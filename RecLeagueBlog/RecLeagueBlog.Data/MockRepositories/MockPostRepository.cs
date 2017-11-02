using RecLeagueBlog.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecLeagueBlog.Models;

namespace RecLeagueBlog.Data.Repositories
{
    public class MockPostRepository : IBlogPostRepository
    {
        private static List<BlogPost> _posts;
        static MockPostRepository()
        {
            
            List<Status> statuses = new List<Status>()
            {
                new Status {StatusId = 1, StatusName = "Published" },
                new Status {StatusId = 2, StatusName = "Pending"},
                new Status {StatusId =3, StatusName ="Deleted"}
            };

            List<Tag> tag1 = new List<Tag>()
            {
                new Tag {TagId = 1, TagName = "championship"},
                new Tag {TagId = 2, TagName = "Global Gym Cobras"},
                new Tag {TagId = 3, TagName = "Average Joe's"}
            };
            List<Tag> tag2 = new List<Tag>()
            {
                new Tag {TagId = 4, TagName = "sign-up"},
                new Tag {TagId = 5, TagName = "ages 10-14"},
                new Tag {TagId = 6, TagName = "fall2017"}
            };
            List<Tag> tag3 = new List<Tag>()
            {
                new Tag {TagId = 7, TagName = "winter2016"},
                new Tag {TagId = 8, TagName = "canadians"},
                new Tag {TagId = 9, TagName = "cancelled"}
            };
            List<Tag> tag4 = new List<Tag>()
            {
                new Tag {TagId = 10, TagName = "summer2017"},
                new Tag {TagId = 11, TagName = "rankings"},
                new Tag {TagId = 12, TagName = "lumberjacks"}
            };
            List<Category> cat1 = new List<Category>()
           {
                new Category {CategoryId =1, CategoryName = "Football" },
                new Category {CategoryId = 2, CategoryName = "Fall" }
           };

            List<Category> cat2 = new List<Category>()
           {
                new Category {CategoryId =3, CategoryName = "Soccer" },
                new Category {CategoryId = 2, CategoryName = "Fall" }
           };

            List<Category> cat3 = new List<Category>()
           {
                new Category {CategoryId =4, CategoryName = "Curling" },
                new Category {CategoryId = 5, CategoryName = "Winter" }
           };
            List<Category> cat4 = new List<Category>()
           {
                new Category {CategoryId =3, CategoryName = "Soccer" },
                new Category {CategoryId = 6, CategoryName = "Summer" }
           };

            _posts = new List<BlogPost>()
            {

                new BlogPost {BlogPostId = 1, Title = "Football Champs", Categories = cat1, Content = "Music piracy book deals having gay friends t-shirts study abroad standing still at concerts co-ed sports knowing what's best for poor people wrigley field making you feel bad about not going outside. The world cup conan o'brien mad men sea salt ugly sweater parties sweaters having gay friends bad memories of high school graduate school musical comedy threatening to move to canada expensive sandwiches toyota prius kitchen gadgets asian fusion food plays the daily show/colbert report architecture vegan/vegetarianism wrigley field david sedaris wine microbreweries yoga having black friends wes anderson movies coffee. Picking their own fruit banksy hating people who wear ed hardy moleskine notebooks funny or ironic tattoos taking a year off ugly sweater parties san francisco the wire hating corporations the idea of soccer musical comedy oscar parties study abroad divorce expensive sandwiches knowing what's best for poor people toyota prius asian fusion food renovations wrigley field being an expert on your culture awareness wes anderson movies barack obama religions their parents don't belong to. Taking a year off being offended book deals study abroad sushi wrigley field manhattan (now brooklyn too!) david sedaris having two last names writers workshops traveling hating their parents tea wes anderson movies. Banksy pea coats facebook the wire oscar parties toyota prius vintage indie music wine religions their parents don't belong to. Funny or ironic tattoos taking a year off rugby san francisco the wire the idea of soccer difficult breakups divorce recycling natural medicine juno dogs sarah silverman vintage apple products snowboarding wrigley field david sedaris being an expert on your culture wes anderson movies religions their parents don't belong to coffee. Hating people who wear ed hardy taking a year off halloween being offended st. patrick's day juno sarah silverman irony plays snowboarding.", DateCreated = DateTime.Parse("07/15/2017"), Tags = tag1, StatusId = 2, UserName = "Admin@recleague.com"},
                new BlogPost {BlogPostId = 2, Title = "Soccer Sign-Up", Categories = cat2, Content = "Sign up now for our Co-ed Soccer League! Please contact Judy Thao for more information...", DateCreated = DateTime.Parse("08/01/2017"), Tags = tag2, StatusId = 1, UserName = "Manager@recleague.com"},
                new BlogPost {BlogPostId = 3, Title = "Curling Canceled", Categories = cat3, Content = "It is with sadness that we announce that the men's curling league has been canceled due to a lack of interest in the dying sport.", DateCreated = DateTime.Parse("11/11/2016"), Tags = tag3, StatusId = 1, UserName = "Admin@recleague.com"},
                new BlogPost{ BlogPostId =4, Title = "Summer Soccer Final Rankings", Categories = cat4, Tags = tag4, Content = "Music piracy book deals having gay friends t-shirts study abroad standing still at concerts co-ed sports knowing what's best for poor people wrigley field making you feel bad about not going outside. The world cup conan o'brien mad men sea salt ugly sweater parties sweaters having gay friends bad memories of high school graduate school musical comedy threatening to move to canada expensive sandwiches toyota prius kitchen gadgets asian fusion food plays the daily show/colbert report architecture vegan/vegetarianism wrigley field david sedaris wine microbreweries yoga having black friends wes anderson movies coffee. Picking their own fruit banksy hating people who wear ed hardy moleskine notebooks funny or ironic tattoos taking a year off ugly sweater parties san francisco the wire hating corporations the idea of soccer musical comedy oscar parties study abroad divorce expensive sandwiches knowing what's best for poor people toyota prius asian fusion food renovations wrigley field being an expert on your culture awareness wes anderson movies barack obama religions their parents don't belong to. Taking a year off being offended book deals study abroad sushi wrigley field manhattan (now brooklyn too!) david sedaris having two last names writers workshops traveling hating their parents tea wes anderson movies. Banksy pea coats facebook the wire oscar parties toyota prius vintage indie music wine religions their parents don't belong to. Funny or ironic tattoos taking a year off rugby san francisco the wire the idea of soccer difficult breakups divorce recycling natural medicine juno dogs sarah silverman vintage apple products snowboarding wrigley field david sedaris being an expert on your culture wes anderson movies religions their parents don't belong to coffee. Hating people who wear ed hardy taking a year off halloween being offended st. patrick's day juno sarah silverman irony plays snowboarding.", DateCreated = DateTime.Parse("08/28/2017"), StatusId = 1, UserName = "Manager@recleague.com"}
            };

        }
        public void CreateBlogPost(BlogPost newPost)
        {
            if (_posts.Any())
            {
                newPost.BlogPostId = _posts.Max(p => p.BlogPostId) + 1;
            }
            else
            {
                newPost.BlogPostId = 1;
            }
            _posts.Add(newPost);
        }

        public void DeletePost(int postId)
        {
            _posts.RemoveAll(p => p.BlogPostId == postId);
        }

        public List<BlogPost> GetAllPosts()
        {
            return _posts;
        }

        public BlogPost GetPostById(int postId)
        {
            return _posts.FirstOrDefault(p => p.BlogPostId == postId);
        }

        public void UpdateBlogPost(BlogPost updatedPost)
        {
            _posts.RemoveAll(p => p.BlogPostId == updatedPost.BlogPostId);
            _posts.Add(updatedPost);
        }

        public List<BlogPost> GetThreeRecent()
        {
            return _posts.OrderByDescending(p => p.DateCreated).Take(3).ToList();
        }

        public void ConvertPostModel(AddPostViewModel postModel)
        {
            List<Tag> tags = new MockTagRepository().GetAllTags();
            List<Category> categories = new MockCategoryRepository().GetAllCategories();

            foreach (var t in postModel.TagsToPost)
            {
                if (!tags.Any(tag => tag.TagName == t))
                {
                    Tag tagToAdd = new Tag();
                    tagToAdd.TagId = tags.Max(tag => tag.TagId) + 1;
                    tagToAdd.TagName = t;
                    tags.Add(tagToAdd);
                }
            }

            foreach(var c in postModel.Categories)
            {
                if(!categories.Any(cat=>cat.CategoryName ==c))
                {
                    Category catToAdd = new Category();
                    catToAdd.CategoryId = categories.Max(cat => cat.CategoryId) + 1;
                    catToAdd.CategoryName = c;
                    categories.Add(catToAdd);
                }
            }
                 
            BlogPost convertedPost = new BlogPost();
            convertedPost.BlogPostId = _posts.Max(p => p.BlogPostId) + 1;
            List<Tag> newTags = new List<Tag>();
            foreach(var t in postModel.TagsToPost)
            {
                
                var tagToAdd = tags.SingleOrDefault(tag => tag.TagName == t);
                newTags.Add(tagToAdd);

                
            }
            convertedPost.Tags = newTags;
            List<Category> newCategories = new List<Category>();

            foreach (var c in postModel.Categories)
            {
                var catToAdd = categories.SingleOrDefault(cat => cat.CategoryName == c);
                newCategories.Add(catToAdd);
            }
            convertedPost.Categories = newCategories;
            convertedPost.Title = postModel.Title;
            convertedPost.DateCreated = DateTime.Now;
            convertedPost.UserName = "Mark";

            _posts.Add(convertedPost); 
        }
    }
}
