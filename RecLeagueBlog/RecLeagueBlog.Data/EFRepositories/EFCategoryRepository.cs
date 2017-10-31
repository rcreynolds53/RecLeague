using RecLeagueBlog.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecLeagueBlog.Models;

namespace RecLeagueBlog.Data.EFRepositories
{
    public class EFCategoryRepository : ICategoryRepository
    {
        RecBlogDBContext context = new RecBlogDBContext();

        public void CreateCategory(Category newCategory)
        {
            context.Categories.Add(newCategory);
            context.SaveChanges();
        }

        public void DeleteCateogry(int categoryId)
        {
            var category = (from c in context.Categories
                            where c.CategoryId == categoryId
                            select c).FirstOrDefault();
            context.Categories.Remove(category);
            context.SaveChanges();
        }

        public List<Category> GetAllCategories()
        {
            var categories = (from c in context.Categories
                              select c).ToList();

            return categories;
        }

        public Category GetCategory(int categoryId)
        {
            var category = (from c in context.Categories
                            where c.CategoryId == categoryId
                            select c).FirstOrDefault();

            return category;
        }

        public void UpdateCategory(Category updatedCategory)
        {
            context.Entry(updatedCategory).State = System.Data.Entity.EntityState.Modified;
            context.SaveChanges();
        }
    }
}
