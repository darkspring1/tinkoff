using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web.Http;
using Business.Services;

namespace Api.Controllers
{
    
    public class UrlController : ApiController
    {
        private readonly UrlService _callService;
        public UrlController(UrlService callService)
        {
            _callService = callService;
        }

        [HttpGet]
        public IHttpActionResult Post(string origin)
        {
            return Ok(_callService.Create(origin));
        }
        
    }
}
