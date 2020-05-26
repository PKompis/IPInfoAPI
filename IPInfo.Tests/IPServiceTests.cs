using IPInfo.Core.Exceptions;
using IPInfo.Library;
using IPInfo.Library.Configuration;
using IPInfo.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Runtime.Caching;

namespace IPInfo.Tests
{
    [TestClass]
    public class IPServiceTests
    {
        private const string _apiKey = "";
        private const string _apiRootUrl = "";

        [TestMethod]
        public void GetInvalidIP()
        {
            var ipservice = new IPService(GetIPInfoProvider(), GetCachingService());


            var ex = Xunit.Assert.ThrowsAsync<BadRequestException>(() => ipservice.GetIPDetails(string.Empty));
            var ex2 = Xunit.Assert.ThrowsAsync<BadRequestException>(() => ipservice.GetIPDetails("Test"));
        }


        [TestMethod]
        public void GetValidIP()
        {
            var ipservice = new IPService(GetIPInfoProvider(), GetCachingService());

            Xunit.Assert.NotNull(ipservice.GetIPDetails("134.201.250.155"));
        }


        private static CachingService GetCachingService() => new CachingService(new CacheItemPolicy { AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(1) });

        private static IPInfoProvider GetIPInfoProvider() => new IPInfoProvider(null, new IPProviderConfiguration { APIKey = _apiKey, APIRootUrl = _apiRootUrl });
    }
}
