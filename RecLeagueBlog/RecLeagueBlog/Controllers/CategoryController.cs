using RecLeagueBlog.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RecLeagueBlog.Controllers
{
    public class CategoryController : ApiController
    {
        BlogManager manager = BlogManagerFactory.Create();

        [Route("categories")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetAllCategories()
        {


            return Ok(manager.GetAllCategories());
        }
    }
}
