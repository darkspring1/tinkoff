using System.Collections.Generic;
using System.Linq;
using Business;
using Business.Dal;
using Business.Entities;
using Business.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Test.Dal;

namespace Test.Tests
{
    [TestClass]
    public class UrlServiceTest
    { 
        [TestMethod]
        public void AddTrafficTest()
        {
            const string shortUrlPart = "http://shortUrl/";
            const string shortUrl = shortUrlPart + "randomString";
            const string origin = "https://bitly.com";
            IRepository<Url> repository = new TestRepository<Url>(new List<Url>
            {
                new Url { OriginUrl = origin, ShortUrl = shortUrl, Traffic = 0 }
            });
            UrlService service = new UrlService(repository, new RandomStringGenerator());
            var url = service.AddTraffic(shortUrl);
            Assert.AreEqual(url.OriginUrl, origin);
            Assert.AreEqual(url.Traffic, 1);
        }


        [TestMethod]
        public void AddTraffic_badPath_Test()
        {
            IRepository<Url> repository = new TestRepository<Url>(Enumerable.Empty<Url>());
            UrlService service = new UrlService(repository, new RandomStringGenerator());
            var url = service.AddTraffic("");
            Assert.IsNull(url);
        }
    }
}
