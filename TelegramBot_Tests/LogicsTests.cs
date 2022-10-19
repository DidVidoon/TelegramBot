using Moq;
using TelegramBot.Interfaces;
using TelegramBot;

namespace TelegramBot_Tests
{
    [TestClass]
    public class LogicsTests
    {
        [TestMethod]
        [DataRow(data1: new string[] { "USD", "01.01.2021", " " }, moreData: "Incorect format. Enter the currency and date in the format: 'currency code dd.mm.yyyy'\nexample: USD 01.01.2021")]
        [DataRow(data1: new string[] { "USDD", "01.01.12021" }, moreData: "Incorect format. Enter the currency and date in the format: 'currency code dd.mm.yyyy'\nexample: USD 01.01.2021")]
        [DataRow(data1: new string[] { "USDD", "01.01.2021" }, moreData: "Incorect currency code. Enter the currency and date in the format: 'currency code dd.mm.yyyy'\n(example: USD 01.01.2021)")]
        [DataRow(data1: new string[] { "USD", "01.01.12021" }, moreData: "Incorect data format. Enter the currency and date in the format: 'currency code dd.mm.yyyy'\n(example: USD 01.01.2021)")]
        [DataRow(data1: new string[] { "BBB", "01.01.2021" }, moreData: "Did not find this currency on this day")]
        [DataRow(data1: new string[] { "USD", "01.01.2021" }, moreData: "USD:\nSale Rate: 28,2746 UAH\nPurchase Rate: 28,2746 UAH")]
        [DataRow(data1: new string[] { "USd", "01.01.2021" }, moreData: "USD:\nSale Rate: 28,2746 UAH\nPurchase Rate: 28,2746 UAH")]
        [DataRow(data1: new string[] { "usd", "01.01.2021" }, moreData: "USD:\nSale Rate: 28,2746 UAH\nPurchase Rate: 28,2746 UAH")]
        public void Run_TakeUserRequest_GetUnswer(string[] parsingUserText, string expected)
        {
            long chatId = 1;
            string userName = "User";

            var mock = new Mock<IGetRequestAPI>();
            mock.Setup(a => a.GetExchangeRate(parsingUserText[1])).Returns("{\"date\":\"01.01.2021\",\"bank\":\"PB\",\"baseCurrency\":980,\"baseCurrencyLit\":\"UAH\",\"exchangeRate\":[{\"baseCurrency\":\"UAH\",\"saleRateNB\":21.6852000,\"purchaseRateNB\":21.6852000},{\"baseCurrency\":\"UAH\",\"currency\":\"AZN\",\"saleRateNB\":16.6439000,\"purchaseRateNB\":16.6439000},{\"baseCurrency\":\"UAH\",\"currency\":\"BYN\",\"saleRateNB\":10.9477000,\"purchaseRateNB\":10.9477000},{\"baseCurrency\":\"UAH\",\"currency\":\"CAD\",\"saleRateNB\":22.1154000,\"purchaseRateNB\":22.1154000},{\"baseCurrency\":\"UAH\",\"currency\":\"CHF\",\"saleRateNB\":32.0156000,\"purchaseRateNB\":32.0156000,\"saleRate\":33.1500000,\"purchaseRate\":31.1500000},{\"baseCurrency\":\"UAH\",\"currency\":\"CNY\",\"saleRateNB\":4.3333000,\"purchaseRateNB\":4.3333000},{\"baseCurrency\":\"UAH\",\"currency\":\"CZK\",\"saleRateNB\":1.3238000,\"purchaseRateNB\":1.3238000,\"saleRate\":1.3300000,\"purchaseRate\":1.1100000},{\"baseCurrency\":\"UAH\",\"currency\":\"DKK\",\"saleRateNB\":4.6700000,\"purchaseRateNB\":4.6700000},{\"baseCurrency\":\"UAH\",\"currency\":\"EUR\",\"saleRateNB\":34.7396000,\"purchaseRateNB\":34.7396000,\"saleRate\":34.9700000,\"purchaseRate\":34.2000000},{\"baseCurrency\":\"UAH\",\"currency\":\"GBP\",\"saleRateNB\":38.4393000,\"purchaseRateNB\":38.4393000,\"saleRate\":38.9500000,\"purchaseRate\":36.9500000},{\"baseCurrency\":\"UAH\",\"currency\":\"HUF\",\"saleRateNB\":0.0951810,\"purchaseRateNB\":0.0951810},{\"baseCurrency\":\"UAH\",\"currency\":\"ILS\",\"saleRateNB\":8.8113000,\"purchaseRateNB\":8.8113000},{\"baseCurrency\":\"UAH\",\"currency\":\"JPY\",\"saleRateNB\":0.2744400,\"purchaseRateNB\":0.2744400},{\"baseCurrency\":\"UAH\",\"currency\":\"KZT\",\"saleRateNB\":0.0671240,\"purchaseRateNB\":0.0671240},{\"baseCurrency\":\"UAH\",\"currency\":\"MDL\",\"saleRateNB\":1.6413000,\"purchaseRateNB\":1.6413000},{\"baseCurrency\":\"UAH\",\"currency\":\"NOK\",\"saleRateNB\":3.2957000,\"purchaseRateNB\":3.2957000},{\"baseCurrency\":\"UAH\",\"currency\":\"PLN\",\"saleRateNB\":7.6348000,\"purchaseRateNB\":7.6348000,\"saleRate\":7.7000000,\"purchaseRate\":7.2000000},{\"baseCurrency\":\"UAH\",\"currency\":\"RUB\",\"saleRateNB\":0.3782300,\"purchaseRateNB\":0.3782300,\"saleRate\":0.4000000,\"purchaseRate\":0.3600000},{\"baseCurrency\":\"UAH\",\"currency\":\"SEK\",\"saleRateNB\":3.4530000,\"purchaseRateNB\":3.4530000},{\"baseCurrency\":\"UAH\",\"currency\":\"SGD\",\"saleRateNB\":21.3651000,\"purchaseRateNB\":21.3651000},{\"baseCurrency\":\"UAH\",\"currency\":\"TMT\",\"saleRateNB\":8.1418000,\"purchaseRateNB\":8.1418000},{\"baseCurrency\":\"UAH\",\"currency\":\"TRY\",\"saleRateNB\":3.8448000,\"purchaseRateNB\":3.8448000},{\"baseCurrency\":\"UAH\",\"currency\":\"UAH\",\"saleRateNB\":1.0000000,\"purchaseRateNB\":1.0000000},{\"baseCurrency\":\"UAH\",\"currency\":\"USD\",\"saleRateNB\":28.2746000,\"purchaseRateNB\":28.2746000,\"saleRate\":28.5000000,\"purchaseRate\":28.0000000},{\"baseCurrency\":\"UAH\",\"currency\":\"UZS\",\"saleRateNB\":0.0027328,\"purchaseRateNB\":0.0027328},{\"baseCurrency\":\"UAH\",\"currency\":\"GEL\",\"saleRateNB\":8.5837000,\"purchaseRateNB\":8.5837000}]}");

            Logics logics = new(parsingUserText, chatId, userName, mock.Object);
            string actual = logics.Run();

            Assert.AreEqual(actual, expected);
        }

        [TestMethod]
        [DataRow(data1: new string[] { "/start" }, moreData: "Enter the currency and date in the format (only last 4 years): 'currency code dd.mm.yyyy'\n(example: USD 01.01.2021)")]
        public void Run_TakeUserMessageStart_GetUnswer(string[] parsingUserText, string expected)
        {
            long chatId = 1;
            string userName = "User";

            var mock = new Mock<IGetRequestAPI>();
            mock.Setup(a => a.GetExchangeRate(parsingUserText[0])).Returns("");

            Logics logics = new(parsingUserText, chatId, userName, mock.Object);
            string actual = logics.Run();

            Assert.AreEqual(actual, expected);
        }

        [TestMethod]
        [DataRow(data1: new string[] { "usd", "01.01.2021" }, moreData: null)]
        [DataRow(data1: new string[] { "usd", "01.01.2021" }, moreData: "")]
        [DataRow(data1: new string[] { "usd", "01.01.2021" }, moreData: "Error")]
        [DataRow(data1: new string[] { "usd", "01.01.2021" }, moreData: "ResponseError")]
        public void Run_TakeApiEmptyOrError_GetUnswer(string[] parsingUserText, string ApiResponse)
        {
            long chatId = 1;
            string userName = "User";
            string expected = "Service is unavailable";

            var mock = new Mock<IGetRequestAPI>();
            mock.Setup(a => a.GetExchangeRate(parsingUserText[0])).Returns(ApiResponse);

            Logics logics = new(parsingUserText, chatId, userName, mock.Object);
            string actual = logics.Run();

            Assert.AreEqual(actual, expected);
        }
    }
}