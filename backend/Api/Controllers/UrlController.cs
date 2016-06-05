using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web.Http;
using Api.Models;
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

        [HttpPost]
        public IHttpActionResult Post([FromBody]UrlPostModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(_callService.GetOrCreate(model.OriginUrl, Settings.ShortUrlPart));
        }


        [HttpGet]
        public IHttpActionResult Get([FromUri]Guid id)
        {
            return Ok(_callService.GetById(id));
        }
    }
}
