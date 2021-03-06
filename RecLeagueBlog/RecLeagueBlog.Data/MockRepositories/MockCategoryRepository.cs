﻿using RecLeagueBlog.Data.Interfaces;
using RecLeagueBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecLeagueBlog.Data.Repositories
{
   public  class MockCategoryRepository : ICategoryRepository
    {
        private static List<Category> _categories;

        static MockCategoryRepository()
        {
            _categories = new List<Category>()
            {
                new Category {CategoryId = 1, CategoryName = "Football"},
                new Category {CategoryId = 2, CategoryName = "Fall" },
                new Category {CategoryId = 3, CategoryName = "Soccer" },
                new Category {CategoryId = 4, CategoryName = "Curling" },
                new Category {CategoryId = 5, CategoryName = "Winter" },
                new Category {CategoryId = 6, CategoryName = "Summer" }
            };
        }

        public void CreateCategory(Category newCategory)
        {
            if (_categories.Any())
            {
                newCategory.CategoryId = _categories.Max(c => c.CategoryId) + 1;
            }
            else
            {
                newCategory.CategoryId = 1;
            }

            _categories.Add(newCategory);
        }

        public void DeleteCateogry(int categoryId)
        {
            List<BlogPost> posts = new MockPostRepository().GetAllPosts();
            _categories.RemoveAll(c => c.CategoryId == categoryId);

            foreach(var p in posts)
            {
                var category = p.Categories.SingleOrDefault(c => c.CategoryId == categoryId);
                if (category != null)
                {
                    p.Categories.Remove(category);
                }                
            }
        }

        public List<Category> GetAllCategories()
        {
            return _categories;
        }

        public Category GetCategory(int categoryId)
        {
            return _categories.FirstOrDefault(c => c.CategoryId == categoryId);
        }

        public void UpdateCategory(Category updatedCategory)
        {
            _categories.RemoveAll(c => c.CategoryId == updatedCategory.CategoryId);
            _categories.Add(updatedCategory);
            List<BlogPost> posts = new MockPostRepository().GetAllPosts();

            foreach (var p in posts)
            {
                var category = p.Categories.SingleOrDefault(c => c.CategoryId == updatedCategory.CategoryId);
                if (category != null)
                {
                    p.Categories.Remove(category);
                    p.Categories.Add(updatedCategory);
                }
            }                       
        }
    }
}
