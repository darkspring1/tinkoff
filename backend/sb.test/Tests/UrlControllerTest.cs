using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Results;
using Api;
using Api.Controllers;
using Api.Models;
using Business;
using Business.Dal;
using Business.Entities;
using Business.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Test.Dal;

namespace Test.Tests
{
    [TestClass]
    public class UrlControllerTest
    {

        private IEnumerable<Url> GetUrlsFromActionResult(IHttpActionResult result)
        {
            return ((OkNegotiatedContentResult<IEnumerable<Url>>) result).Content;
        }

        private Url GetUrlFromActionResult(IHttpActionResult result)
        {
            return ((OkNegotiatedContentResult<Url>)result).Content;
        }


        private UrlController CreateController(IEnumerable<Url> urls)
        {
            IRepository<Url> repository = new TestRepository<Url>(Enumerable.Empty<Url>());
            UrlService service = new UrlService(repository, new RandomStringGenerator());
            return new UrlController(service);
        }

        [TestMethod]
        public void PostTest_new()
        {
            UrlController ctrl = CreateController(Enumerable.Empty<Url>());
            var result = ctrl.Post(new UrlPostModel { OriginUrl = "https://bitly.com/" });
            var urls = GetUrlsFromActionResult(result);
            Assert.IsTrue(urls.Count() == 1);
        }

        [TestMethod]
        public void PostTest_alreadyExist()
        {
            string shortUrl = Settings.GetShortUrl("randomString");
            UrlController ctrl = CreateController(new [] { new Url { OriginUrl = "https://bitly.com", ShortUrl = shortUrl } });
            var result = ctrl.Post(new UrlPostModel { OriginUrl = "https://bitly.com/" });
            var urls = GetUrlsFromActionResult(result);
            Assert.AreEqual(urls.First().ShortUrl, shortUrl);
        }

        [TestMethod]
        public void GetTest()
        {
            UrlController ctrl = CreateController(new[] { new Url {Id = Guid.Empty} });
            var result = ctrl.Get(Guid.Empty);
            var url = GetUrlFromActionResult(result);
            Assert.AreEqual(url.Id, Guid.Empty);
        }
    }
}
