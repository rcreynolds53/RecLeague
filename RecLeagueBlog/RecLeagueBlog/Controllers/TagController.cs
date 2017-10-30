using RecLeagueBlog.Data;
using RecLeagueBlog.Data.Interfaces;
using RecLeagueBlog.Data.Repositories;
using RecLeagueBlog.Models;
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
    public class TagController : ApiController
    {
        BlogManager manager = BlogManagerFactory.Create();

        [Route("tags")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetAll()
        {
            return Ok(manager.GetAllTags());
        }
        [Route("tag")]
        [AcceptVerbs("POST")]
        public IHttpActionResult Add(Tag newTag)
        {
            manager.CreateTag(newTag);
            return Created($"tag/{newTag.TagId}", newTag);
        }
    }
}
