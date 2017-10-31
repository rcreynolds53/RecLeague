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
            Category category = new Category();
            BlogPost post = new BlogPost();
            MockCategoryRepository categoryRepo = new MockCategoryRepository();
            MockPostRepository postRepo = new MockPostRepository();

            category.CategoryId = 1;
            var categoryToDelete = categoryRepo.GetCategory(category.CategoryId);
            categoryRepo.DeleteCateogry(categoryToDelete.CategoryId);
            post.BlogPostId = 1;
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
    }
}
