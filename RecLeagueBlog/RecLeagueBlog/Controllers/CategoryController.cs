using RecLeagueBlog.Data;
using RecLeagueBlog.Models;
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

        [Route("category")]
        [AcceptVerbs("POST")]
        public IHttpActionResult Add(Category newCategory)
        {
            manager.CreateCategory(newCategory);
            return Created($"tag/{newCategory.CategoryId}", newCategory);
        }

        [Route("category/{id}")]
        [AcceptVerbs("DELETE")]
        public void Delete(int id)
        {
            manager.DeleteCategory(id);
        }


        [Route("category/{id}")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetCategory(int id)
        {
            return Ok(manager.GetCategory(id));
        }

        [Route("category/{id}")]
        [AcceptVerbs("PUT")]
        public void EditTag(Category updatedCategory)
        {
            manager.UpdateCategory(updatedCategory);

        }
    }
}
