using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramBot.Interfaces;

namespace TelegramBot
{
    public class Logics : ILogics
    {
        readonly string[] parsingUserText;
        readonly long chatId;
        readonly string user;
        private readonly IGetRequestAPI getRequestAPI;

        public Logics(string[] _parsingUserText, long _chatId, string _user, IGetRequestAPI _getRequestAPI)
        {
            parsingUserText = _parsingUserText;
            chatId = _chatId;
            user = _user;
            getRequestAPI = _getRequestAPI;
        }

        public string Run()
        {
            string checkResult = FormatCheck();

            if (checkResult == "Ok")
            {
                return FindCurrency();
            }

            return checkResult;
        }

        private string FormatCheck()
        {
            if (parsingUserText[0] == "/start")
            {
                Logs.Writer($"Chat {chatId} started bot\n");
                return "Enter the currency and date in the format (only last 4 years): 'currency code dd.mm.yyyy'\n(example: USD 01.01.2021)";
            }
            else
            {
                if (parsingUserText.Length != 2 || parsingUserText[0].Length != 3 && !DateTime.TryParseExact(parsingUserText[1], format: "dd.mm.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out _))
                {
                    Logs.Writer($"In chat {chatId}, {user} entered the incorrect message\n");
                    return "Incorect format. Enter the currency and date in the format: 'currency code dd.mm.yyyy'\nexample: USD 01.01.2021";
                }
                else if (parsingUserText[0].Length != 3)
                {
                    Logs.Writer($"In chat {chatId}, {user} entered the incorrect curency code\n");
                    return "Incorect currency code. Enter the currency and date in the format: 'currency code dd.mm.yyyy'\n(example: USD 01.01.2021)";
                }
                else if (!DateTime.TryParseExact(parsingUserText[1], format: "dd.mm.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out _))
                {
                    Logs.Writer($"In chat {chatId}, {user} entered the incorrect data format\n");
                    return "Incorect data format. Enter the currency and date in the format: 'currency code dd.mm.yyyy'\n(example: USD 01.01.2021)";
                }
                else
                {
                    Logs.Writer($"In chat {chatId}, {user} entered good exchange rate request\n");
                    return "Ok";
                }
            }
        }

        private string FindCurrency()
        {
            string currencyCode = parsingUserText[0].ToUpper();
            string date = parsingUserText[1];

            string ResponseGetExchangeRate = getRequestAPI.GetExchangeRate(date);

            if (ResponseGetExchangeRate == "Error" || ResponseGetExchangeRate == "ResponseError")
                return "Service is unvailable";

            try
            {
                var json = JObject.Parse(ResponseGetExchangeRate);
                var exchangeRate = json["exchangeRate"];


                foreach (var currancyInfo in exchangeRate)
                {
                    string currency = (currancyInfo["currency"] ?? string.Empty).ToString();
                    string saleRateNB = currancyInfo["saleRateNB"].ToString();
                    string purchaseRateNB = currancyInfo["purchaseRateNB"].ToString();

                    if (currency == currencyCode)
                    {
                        Logs.Writer($"Bot response to {user} in chat {chatId}:\n                    {currency}:\n                    Sale Rate NB: {saleRateNB} UAH\n                    Purchase Rate NB: {purchaseRateNB} UAH\n");
                        return $"{currency}:\nSale Rate: {saleRateNB} UAH\nPurchase Rate: {purchaseRateNB} UAH";
                    }
                }
            }
            catch (Exception ex)
            {
                Logs.Writer(ex.Message);
                return "Service is unavailable";
            }

            Logs.Writer($"Bot did not find this currency on this day\n");
            return "Did not find this currency on this day";
        }
    }
}
