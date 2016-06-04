using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Results;
using Api.Controllers;
using Business;
using Business.Dal;
using Business.Entities;
using Business.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StructureMap;
using Test.Dal;

namespace Test.controllers
{
    [TestClass]
    public class CallControllerTest
    {

        private Url GetUrlFromActionResult(IHttpActionResult result)
        {
            return ((OkNegotiatedContentResult<Url>) result).Content;
        }

        [TestMethod]
        public void PostTest_new()
        {
            IRepository<Url> repository = new TestRepository<Url>(Enumerable.Empty<Url>());
            UrlService service = new UrlService(repository, new RandomStringGenerator());
            UrlController ctrl = new UrlController(service);
            var result = ctrl.Post("https://bitly.com/");
            var url = GetUrlFromActionResult(result);
            Assert.IsNotNull(url);
        }

        [TestMethod]
        public void PostTest_alreadyExist()
        {
            const string path = "randomString";
            IRepository<Url> repository = new TestRepository<Url>(new List<Url>
            {
                new Url { Origin = "https://bitly.com", Path = path }
            });

            UrlService service = new UrlService(repository, new RandomStringGenerator());
            UrlController ctrl = new UrlController(service);
            var result = ctrl.Post("https://bitly.com/");
            var url = GetUrlFromActionResult(result);
            Assert.AreEqual(url.Path, path);
        }
    }
}
