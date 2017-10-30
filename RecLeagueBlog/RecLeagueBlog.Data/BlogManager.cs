using RecLeagueBlog.Data.Interfaces;
using RecLeagueBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecLeagueBlog.Data
{
    public class BlogManager
    {
         private ITagRepository _tagRepo;
         private ICategoryRepository _categoryRepo;
         private IBlogPostRepository _blogPostRepo;

        public BlogManager(ITagRepository tagRepo, ICategoryRepository categoryRepo, IBlogPostRepository blogPostRepo)
        {
            _tagRepo = tagRepo;
            _categoryRepo = categoryRepo;
            _blogPostRepo = blogPostRepo;
        }

        public List<Tag> GetAllTags()
        {
            return _tagRepo.GetAllTags();
        }

        public void CreateTag(Tag newTag)
        {
            _tagRepo.CreateTag(newTag);
        }

        public void DeleteTag(int tagId)
        {
            _tagRepo.DeleteTag(tagId);
        }

        public void DeleteCategory(int categoryId)
        {
            _categoryRepo.DeleteCateogry(categoryId);
        }

        public Category GetCategory(int categoryId)
        {
           return _categoryRepo.GetCategory(categoryId);
        }

        public BlogPost GetPost(int postId)
        {
            return _blogPostRepo.GetPostById(postId);
        }
    }
}
