using NUnit.Framework;
using RecLeagueBlog.Data.Repositories;
using RecLeagueBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecLeagueBlog.Tests
{
    [TestFixture]
   public class MockRepositoryTests
    {
        [Test]
        public void CanDeleteCategoryFromAllPosts()
        {
 
            MockCategoryRepository categoryRepo = new MockCategoryRepository();
            MockPostRepository postRepo = new MockPostRepository();

            var categoryToDelete = categoryRepo.GetCategory(1);
            categoryRepo.DeleteCateogry(categoryToDelete.CategoryId);
            var post = postRepo.GetPostById(1);
            var postToDisplay = postRepo.GetPostById(post.BlogPostId);

            Assert.AreEqual(1, postToDisplay.Categories.Count());
        }

        [Test]
        public void CanUpdateCategoryInPosts()
        {
            Category category = new Category();
            BlogPost post = new BlogPost();
            MockCategoryRepository categoryRepo = new MockCategoryRepository();
            MockPostRepository postRepo = new MockPostRepository();

            post.BlogPostId = 1;
            var postToDisplay = postRepo.GetPostById(post.BlogPostId);
            category.CategoryId = 1;
            var categoryToUpdate = categoryRepo.GetCategory(category.CategoryId);
            categoryToUpdate.CategoryName = "Sports";       
            categoryRepo.UpdateCategory(categoryToUpdate);
            var categoryToShow = postToDisplay.Categories.SingleOrDefault(c => c.CategoryName == categoryToUpdate.CategoryName);


            Assert.AreEqual("Sports", categoryToShow.CategoryName.ToString());
        }
        [Test]
        public void CanAddBlogPost()
        {
            BlogPost post = new BlogPost();
            MockPostRepository postRepo = new MockPostRepository();

            post.Content = "This";
            post.DateCreated = DateTime.Now;

            postRepo.CreateBlogPost(post);

            var postGet = postRepo.GetPostById(5);

            Assert.AreEqual(5, postRepo.GetAllPosts().Count());
        }

        [Test]
        public void CanDeleteTag()
        {          

            MockTagRepository tagRepo = new MockTagRepository();

            var tag = tagRepo.GetTag(1);

            tagRepo.DeleteTag(tag.TagId);

            Assert.AreEqual(11, tagRepo.GetAllTags().Count());

        }
    }
}
