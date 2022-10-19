using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TelegramBot.Interfaces;

namespace TelegramBot
{
    public class GetRequestAPI : IGetRequestAPI
    {
        public string GetExchangeRate(string date)
        {
            var _request = (HttpWebRequest)WebRequest.Create($"https://api.privatbank.ua/p24api/exchange_rates?json&date={date}");
            _request.Method = "Get";

            try
            {
                HttpWebResponse response = (HttpWebResponse)_request.GetResponse();
                var stream = response.GetResponseStream();
                if (stream != null)
                    return new StreamReader(stream).ReadToEnd();

                Logs.Writer("Response API is null");
                return "Error";
            }
            catch (Exception ex)
            {
                Logs.Writer(ex.Message);
                return "ResponseError";
            }
        }
    }
}
