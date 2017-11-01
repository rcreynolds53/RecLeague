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

        // TAGS BLL

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

        public void UpdateTag(Tag updatedTag)
        {
            _tagRepo.UpdateTag(updatedTag);
        }

        public Tag GetTag(int tagId)
        {
            return _tagRepo.GetTag(tagId);
        }

        // CATEGORY BLL

        public List<Category> GetAll()
        {
            return _categoryRepo.GetAllCategories();
        }
        public void DeleteCategory(int categoryId)
        {
            _categoryRepo.DeleteCateogry(categoryId);
        }

        public Category GetCategory(int categoryId)
        {
           return _categoryRepo.GetCategory(categoryId);
        }

        public void CreateCategory(Category newCategory)
        {
            _categoryRepo.CreateCategory(newCategory);
        }
        public void UpdateCategory(Tag updatedCategory)
        {
            _tagRepo.UpdateTag(updatedCategory);
        }

        // POSTS BLL

        public BlogPost GetPost(int postId)
        {
            return _blogPostRepo.GetPostById(postId);
        }

        public List<BlogPost> GetAllPosts()
        {
            return _blogPostRepo.GetAllPosts();
        }

        public List<BlogPost> GetThreeRecent()
        {
            return _blogPostRepo.GetThreeRecent();
        }

        public void CreateBlogPost(BlogPost newPost)
        {
            if(_blogPostRepo.GetAllPosts().Any())
            {
                newPost.BlogPostId = _blogPostRepo.GetAllPosts().Max(p => p.BlogPostId) + 1;
            }

            newPost.BlogPostId = 1;
            _blogPostRepo.CreateBlogPost(newPost);
        }

        public void UpdatePost(BlogPost updatedPost)
        {
            _blogPostRepo.UpdateBlogPost(updatedPost);
        }

        public void DeletePost(int postId)
        {
            _blogPostRepo.DeletePost(postId);
        }

    }
}
