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
            return context.BlogPosts.ToList();
        }

        public BlogPost GetPostById(int postId)
        {
            return context.BlogPosts.FirstOrDefault(p => p.BlogPostId == postId);
        }

        public void UpdateBlogPost(BlogPost updatedPost)
        {
            context.Entry(updatedPost).State = System.Data.Entity.EntityState.Modified;
            context.SaveChanges();
        }

        public List<BlogPost> GetThreeRecent()
        {
            return context.BlogPosts.OrderByDescending(p=>p.DateCreated).Take(3).ToList();

        }

        public BlogPost UpdatePostModel(AddPostViewModel postModel)
        {
            var tags = context.Tags.ToList();
            var categories = context.Categories.ToList();
            foreach (var t in postModel.TagsToPost)
            {
                if (!tags.Any(tag => tag.TagName == t))
                {
                    Tag tagToAdd = new Tag();
                    tagToAdd.TagName = t;
                    tags.Add(tagToAdd);
                }
            }

            foreach (var c in postModel.Categories)
            {
                if (!categories.Any(cat => cat.CategoryName == c))
                {
                    Category catToAdd = new Category();
                    catToAdd.CategoryName = c;
                    categories.Add(catToAdd);
                }
            }

            BlogPost convertedPost = context.BlogPosts.Single(p => p.BlogPostId == postModel.BlogPostId);
            List<Tag> newTags = new List<Tag>();
            foreach (var t in postModel.TagsToPost)
            {

                var tagToAdd = tags.SingleOrDefault(tag => tag.TagName == t);
                newTags.Add(tagToAdd);


            }
            convertedPost.Tags = newTags;
            List<Category> newCategories = new List<Category>();

            foreach (var c in postModel.Categories)
            {
                var catToAdd = categories.Single(cat => cat.CategoryName == c);
                newCategories.Add(catToAdd);
            }
            convertedPost.Categories = newCategories;
            convertedPost.Title = postModel.Title;
            convertedPost.Content = postModel.Content;
            convertedPost.Status = context.Statuses.First(s => s.StatusName == postModel.StatusName);

            return convertedPost;
        }

        public BlogPost ConvertPostModel(AddPostViewModel postModel)
        {
            var tags = context.Tags.ToList();
            var categories = context.Categories.ToList();

            foreach (var t in postModel.TagsToPost)
            {
                if (!tags.Any(tag => tag.TagName == t))
                {
                    Tag tagToAdd = new Tag();
                    tagToAdd.TagName = t;
                    tags.Add(tagToAdd);
                }
            }

            foreach (var c in postModel.Categories)
            {
                if (!categories.Any(cat => cat.CategoryName == c))
                {
                    Category catToAdd = new Category();
                    catToAdd.CategoryName = c;
                    categories.Add(catToAdd);
                }
            }

            BlogPost convertedPost = new BlogPost();
            var newTags = new List<Tag>();
            foreach (var t in postModel.TagsToPost)
            {

                var tagToAdd = tags.Single(tag => tag.TagName == t);
                newTags.Add(tagToAdd);


            }
            convertedPost.Tags = newTags;
            List<Category> newCategories = new List<Category>();

            foreach (var c in postModel.Categories)
            {
                var catToAdd = categories.Single(cat => cat.CategoryName == c);
                newCategories.Add(catToAdd);
            }
            convertedPost.Content = postModel.Content;
            convertedPost.Categories = newCategories;
            convertedPost.Title = postModel.Title;
            convertedPost.DateCreated = DateTime.Now;
            convertedPost.AppUser = context.Users.Single(u => u.UserName == postModel.UserName);
            convertedPost.Status = context.Statuses.Single(s => s.StatusName == postModel.StatusName);
            return convertedPost;
        }

        public List<BlogPost> GetAllPublishedPosts()
        {
            return context.BlogPosts.Where(p => p.Status.StatusName.ToUpper() == "PUBLISHED").ToList();
        }
    }
}
