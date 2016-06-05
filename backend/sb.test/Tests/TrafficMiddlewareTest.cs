using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Results;
using System.Web.Http.Validation;
using Api;
using Api.Middlewares;
using Business;
using Business.Dal;
using Business.Entities;
using Business.Services;
using Microsoft.Owin;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using StructureMap;
using Test.Dal;
using Test.Ioc;

namespace Test.Tests
{
    [TestClass]
    public class TrafficMiddlewareTest
    {
        private IOwinResponse MiddlewareInvoke(string pathValue, Action<string> redirectCallback = null)
        {
            var owinMwMoc = new Mock<OwinMiddleware>(null);
            var contextMoc = new Mock<IOwinContext>();
            var requestMoc = new Mock<IOwinRequest>();

            if (redirectCallback != null)
            {
                var responseMoc = new Mock<IOwinResponse>();
                responseMoc
                    .Setup(mr => mr.Redirect(It.IsAny<string>()))
                    .Callback(redirectCallback);

                contextMoc.Setup(c => c.Response).Returns(responseMoc.Object);
            }

            PathString path = new PathString(pathValue);
            requestMoc.Setup(r => r.Path).Returns(path);
            contextMoc.Setup(c => c.Request).Returns(requestMoc.Object);
            
            var context = contextMoc.Object;
            using (var container = new Container(new TestRegistry()))
            {
                var mw = new TrafficMiddleware(owinMwMoc.Object, container);    
                mw.Invoke(context);
            }
            
            return context.Response;
        }

        [TestMethod]
        public void WrongPath_Test()
        {
            var response = MiddlewareInvoke(null);
            Assert.IsNull(response);

            response = MiddlewareInvoke("/");
            Assert.IsNull(response);

            response = MiddlewareInvoke("/api/url");
            Assert.IsNull(response);

            response = MiddlewareInvoke("/sdsffsff");
            Assert.IsNull(response);
        }


        [TestMethod]
        public void RightPath_Test()
        {
            const string path = "/randomString";
            const string originUrl = "https://bitly.com";
            var urls = new[]
            {
                new Url {OriginUrl = originUrl, ShortUrl = Settings.GetShortUrl(path)}
            };

            DataContext.AddData(urls);

            string redirectUrl = null;
            MiddlewareInvoke(path, s => { redirectUrl = s; });
            Assert.AreEqual(redirectUrl, originUrl);

            DataContext.Clear();
        }
    }



}
