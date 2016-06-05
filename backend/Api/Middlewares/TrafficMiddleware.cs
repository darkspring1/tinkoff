using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Business.Services;
using Microsoft.Owin;
using NLog;
using StructureMap;
namespace Api.Middlewares
{
    public class TrafficMiddleware : OwinMiddleware
    {
        //private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private readonly IContainer _container;

        public TrafficMiddleware(OwinMiddleware next, IContainer container)
            : base(next)
        {
            _container = container;
        }
        
        public override Task Invoke(IOwinContext context)
        {
            var path = context.Request.Path;
            if (path.HasValue &&
                path.Value.Length > 2 &&
                Regex.IsMatch(path.Value, @"^\/[A-Za-z0-9]*$"))
            {
                using (var nested = _container.GetNestedContainer())
                {
                    var urlService = nested.GetInstance<UrlService>();
                    var url = urlService
                        .AddTraffic(Settings.ShortUrlPart + path.Value);
                    if (url != null)
                    {
                        context.Response.Redirect(url.OriginUrl);
                        return Task.FromResult(0);
                    }
                }
            }
            return Next.Invoke(context);
        }
    }
}
