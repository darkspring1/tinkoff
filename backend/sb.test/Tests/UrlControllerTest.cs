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

        private IEnumerable<Url> GetUrlFromActionResult(IHttpActionResult result)
        {
            return ((OkNegotiatedContentResult<IEnumerable<Url>>) result).Content;
        }

        [TestMethod]
        public void PostTest_new()
        {
            IRepository<Url> repository = new TestRepository<Url>(Enumerable.Empty<Url>());
            UrlService service = new UrlService(repository, new RandomStringGenerator());
            UrlController ctrl = new UrlController(service);
            var result = ctrl.Post(new UrlPostModel { OriginUrl = "https://bitly.com/" });
            var urls = GetUrlFromActionResult(result);
            Assert.IsTrue(urls.Count() == 1);
        }

        [TestMethod]
        public void PostTest_alreadyExist()
        {
            string shortUrl = Settings.GetShortUrl("randomString");
            IRepository<Url> repository = new TestRepository<Url>(new List<Url>
            {
                new Url { OriginUrl = "https://bitly.com", ShortUrl = shortUrl }
            });

            UrlService service = new UrlService(repository, new RandomStringGenerator());
            UrlController ctrl = new UrlController(service);
            var result = ctrl.Post(new UrlPostModel { OriginUrl = "https://bitly.com/" });
            var urls = GetUrlFromActionResult(result);
            Assert.AreEqual(urls.First().ShortUrl, shortUrl);
        }
    }
}
