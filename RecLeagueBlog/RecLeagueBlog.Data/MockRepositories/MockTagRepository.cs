using RecLeagueBlog.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecLeagueBlog.Models;

namespace RecLeagueBlog.Data.Repositories
{
    public class MockTagRepository : ITagRepository
    {
        private static List<Tag> _tags;

        static MockTagRepository()
        {
            _tags = new List<Tag>()
            {
                new Tag {TagId = 1, TagName = "championship"},
                new Tag {TagId = 2, TagName = "Global Gym Cobras"},
                new Tag {TagId = 3, TagName = "Average Joe's"},
                new Tag {TagId = 4, TagName = "sign-up"},
                new Tag {TagId = 5, TagName = "ages 10-14"},
                new Tag {TagId = 6, TagName = "fall2017"},
                new Tag {TagId = 7, TagName = "winter2016"},
                new Tag {TagId = 8, TagName = "canadians"},
                new Tag {TagId = 9, TagName = "cancelled"},
                new Tag {TagId = 10, TagName = "summer2017"},
                new Tag {TagId = 11, TagName = "rankings"},
                new Tag {TagId = 12, TagName = "lumberjacks"}
            };
        }        
        public void CreateTag(Tag newTag)
        {
            if (_tags.Any())
            {
                newTag.TagId = _tags.Max(t => t.TagId) + 1;
            }
            else
            {
                newTag.TagId = 1;
            }
            _tags.Add(newTag);
        }

        public void DeleteTag(int tagId)
        {
            List<BlogPost> posts = new MockPostRepository().GetAllPosts();
            _tags.RemoveAll(t => t.TagId == tagId);

            foreach(var p in posts)
            {
                var tag = p.Tags.SingleOrDefault(t => t.TagId == tagId);

                if(tag != null)
                {
                    p.Tags.Remove(tag);
                }
            }
        }

        public List<Tag> GetAllTags()
        {
            return _tags;
        }

        public Tag GetTag(int tagId)
        {
            return _tags.FirstOrDefault(t => t.TagId == tagId);
        }

        public void UpdateTag(Tag updatedTag)
        {
            _tags.RemoveAll(t => t.TagId == updatedTag.TagId);
            _tags.Add(updatedTag);
            List<BlogPost> posts = new MockPostRepository().GetAllPosts();

            foreach(var p in posts)
            {
                var tag = p.Tags.SingleOrDefault(t => t.TagId == updatedTag.TagId);

                if(tag != null)
                {
                    p.Tags.Remove(updatedTag);
                    p.Tags.Add(updatedTag);
                }
            }
        }
    }
}
