using IPInfo.Library;
using IPInfo.Library.Configuration;
using IPInfo.Library.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IPInfo.Tests
{
    [TestClass]
    public class IPInfoProviderTests
    {
        private const string _apiKey = "API_KEY";
        private const string _apiRootUrl = "http://api.ipstack.com";

        [TestMethod]
        public void GetInvalidIP()
        {
            var service = new IPInfoProvider(null, IPProviderConfiguration());


            var ex = Xunit.Assert.Throws<IPServiceNotAvailableException>(() => service.GetIPDetails(string.Empty));
            var ex2 = Xunit.Assert.Throws<IPServiceNotAvailableException>(() => service.GetIPDetails("Test"));
        }


        [TestMethod]
        public void GetValidIP()
        {
            var service = new IPInfoProvider(null, IPProviderConfiguration());

            Xunit.Assert.NotNull(service.GetIPDetails("134.201.250.155"));
        }


        private static IPProviderConfiguration IPProviderConfiguration() => new IPProviderConfiguration { APIKey = _apiKey, APIRootUrl = _apiRootUrl };

    }
}
