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
    }
}
