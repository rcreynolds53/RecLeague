using RecLeagueBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecLeagueBlog.Data.Interfaces
{
    public interface IBlogPostRepository
    {
        List<BlogPost> GetAllPosts();
        BlogPost GetPostById(int postId);
        void CreateBlogPost(BlogPost newPost);
        void UpdateBlogPost(BlogPost updatedPost);
        void DeletePost(int postId);
        List<BlogPost> GetThreeRecent();
        BlogPost ConvertPostModel(AddPostViewModel postModel);
        BlogPost UpdatePostModel(AddPostViewModel postModel);
        List<BlogPost> GetAllPublishedPosts();


    }
}
