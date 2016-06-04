using System.Collections.Generic;
using System.Linq;
using Business;
using Business.Dal;
using Business.Entities;
using Business.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Test.Dal;

namespace Test.Services
{
    [TestClass]
    public class UrlServiceTest
    { 
        [TestMethod]
        public void GetByPathTest()
        {
            const string shortUrlPart = "http://shortUrl/";
            const string shortUrl = shortUrlPart + "randomString";
            const string origin = "https://bitly.com";
            IRepository<Url> repository = new TestRepository<Url>(new List<Url>
            {
                new Url { OriginUrl = origin, ShortUrl = shortUrl }
            });
            UrlService service = new UrlService(repository, new RandomStringGenerator());
            var url = service.GetByShortUrl(shortUrl).First();
            Assert.AreEqual(url.OriginUrl, origin);
        }
    }
}
