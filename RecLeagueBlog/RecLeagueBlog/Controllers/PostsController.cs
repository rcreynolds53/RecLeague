using RecLeagueBlog.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace RecLeagueBlog.Controllers
{
    [EnableCors(origins: "*", methods: "*", headers: "*")]
    public class PostsController : ApiController
    {
        BlogManager manager = BlogManagerFactory.Create();

        [Route("posts")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetAll()
        {
            return Ok(manager.GetAllPosts());
        }

        [Route("recent/posts")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetThreeRecent()
        {
            return Ok(manager.GetThreeRecent());
        }
    }
}
