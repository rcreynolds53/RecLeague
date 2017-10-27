using RecLeagueBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecLeagueBlog.Data.Interfaces
{
    public interface ICategoryRepository
    {
        List<Category> GetAllCategories();
        Category GetCategory(int categoryId);
        void DeleteCateogry(int categoryId);
        void UpdateCategory(Category updatedCategory);
        void CreateCategory(Category newCategory);
    }
}
