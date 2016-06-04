using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Owin;
using Newtonsoft.Json;
using NLog;
namespace Api.Middlewares
{
    public class TrafficMiddleware : OwinMiddleware
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();


        public TrafficMiddleware(OwinMiddleware next)
            : base(next)
        {
        }

        public override Task Invoke(IOwinContext context)
        {
            var path = context.Request.Path;
            if (path.HasValue && Regex.IsMatch(path.Value, "^[A-Za-z0-9]*$"))
            {
                
            }

            return Next.Invoke(context);
        }
    }
}
