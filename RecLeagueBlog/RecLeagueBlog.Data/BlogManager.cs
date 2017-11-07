using Microsoft.AspNet.Identity.EntityFramework;
using RecLeagueBlog.Data.Interfaces;
using RecLeagueBlog.Models;
using RecLeagueBlog.Models.Identity;
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
         private IUserRepo _userRepo;

        public BlogManager(ITagRepository tagRepo, ICategoryRepository categoryRepo, IBlogPostRepository blogPostRepo, IUserRepo userRepo)
        {
            _tagRepo = tagRepo;
            _categoryRepo = categoryRepo;
            _blogPostRepo = blogPostRepo;
            _userRepo = userRepo;
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

        public List<Category> GetAllCategories()
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

        public BlogPost ConvertPostModel(AddPostViewModel postModel)
        {
            return _blogPostRepo.ConvertPostModel(postModel);
        }

        public BlogPost UpdatePostModel(AddPostViewModel postModel)
        {
            return _blogPostRepo.UpdatePostModel(postModel);
        }

        // ***** User Methods ********

        public List<AppUser> GetAllUsers()
        {
            return _userRepo.GetAllUsers();
        }

        public AppUser GetUser(string id)
        {
            return _userRepo.GetUser(id);
        }

        public void UpdateUser(AppUser updatedUser)
        {
            _userRepo.UpdateUser(updatedUser);
        }

        public void DeleteUser(string id)
        {
            _userRepo.DeleteUser(id);
        }

        public void CreateUser(AppUser newUser)
        {
            _userRepo.CreateUser(newUser);
        }

        public IEnumerable<IdentityRole> GetAllRoles()
        {
            return _userRepo.GetAllRoles();
        }
    }
}
