using RecLeagueBlog.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecLeagueBlog.Models;

namespace RecLeagueBlog.Data.EFRepositories
{
    public class EFPostRepository : IBlogPostRepository
    {
        RecBlogDBContext context = new RecBlogDBContext();
        public void CreateBlogPost(BlogPost newPost)
        {
            context.BlogPosts.Add(newPost);
            context.SaveChanges();
        }

        public void DeletePost(int postId)
        {
            var post = (from p in context.BlogPosts
                        where p.BlogPostId == postId
                        select p).FirstOrDefault();
            context.BlogPosts.Remove(post);
            context.SaveChanges();
        }

        public List<BlogPost> GetAllPosts()
        {
            var posts = (from p in context.BlogPosts
                         select p).ToList();

            return posts;
        }

        public BlogPost GetPostById(int postId)
        {
            var post = (from p in context.BlogPosts
                        where p.BlogPostId == postId
                        select p).FirstOrDefault();

            return post;
        }

        public void UpdateBlogPost(BlogPost updatedPost)
        {
            context.Entry(updatedPost).State = System.Data.Entity.EntityState.Modified;
            context.SaveChanges();
        }

        public List<BlogPost> GetThreeRecent()
        {
            var posts = (from p in context.BlogPosts
                         orderby p.DateCreated descending
                         select p).Take(3).ToList();
            return posts;
        }

        public void ConvertPostModel(AddPostViewModel postModel)
        {
            throw new NotImplementedException();
        }
    }
}
