using RecLeagueBlog.Data;
using RecLeagueBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Results;
using Microsoft.AspNet.Identity;

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

        [Route("post")]
        [AcceptVerbs("POST")]
        public IHttpActionResult AddBlogPost(AddPostViewModel newPost)
        {
            newPost.UserName = User.Identity.GetUserName();          
            var blogPost = manager.ConvertPostModel(newPost);
            manager.CreateBlogPost(blogPost);
            return Created($"post/{blogPost.BlogPostId}", blogPost);
        }

        [Route("post/{id}")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetPost(int id)
        {
            return Ok(manager.GetPost(id));
        }

        [Route("post/{id}")]
        [AcceptVerbs("PUT")]
        public void EditPost(AddPostViewModel updatedPost)
        {
            var post = manager.UpdatePostModel(updatedPost);
            manager.UpdatePost(post);
        }

        [Route("post/{id}")]
        [AcceptVerbs("DELETE")]
        public void DeletePost(int id)
        {
            manager.DeletePost(id);
        }
    }
}
