using RecLeagueBlog.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RecLeagueBlog.Controllers
{
    public class StaticPagesController : ApiController
    {
        BlogManager manager = BlogManagerFactory.Create();
        [Route("api/StaticPages")]
        [AcceptVerbs("GET")]
        public IHttpActionResult Get()
        {
            return Ok(manager.GetAllPublishStaticPages());
        }

        // GET: api/StaticPages/5
        public string Get(int id)
        {
            return "value";
        }
    }
}
