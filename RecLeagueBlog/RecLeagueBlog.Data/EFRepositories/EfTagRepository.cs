using RecLeagueBlog.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecLeagueBlog.Models;

namespace RecLeagueBlog.Data.EFRepositories
{
    public class EFTagRepository : ITagRepository
    {
        RecBlogDBContext context = new RecBlogDBContext();
        public void CreateTag(Tag newTag)
        {
            context.Tags.Add(newTag);
            context.SaveChanges();
        }

        public void DeleteTag(int tagId)
        {
            var tag = (from t in context.Tags
                       where t.TagId == tagId
                       select t).FirstOrDefault();
            context.Tags.Remove(tag);
            context.SaveChanges();
        }

        public List<Tag> GetAllTags()
        {
            return context.Tags.ToList();
        }

        public Tag GetTag(int tagId)
        {
            return context.Tags.FirstOrDefault(t => t.TagId == tagId);
        }

        public void UpdateTag(Tag updatedTag)
        {
            context.Entry(updatedTag).State = System.Data.Entity.EntityState.Modified;
            context.SaveChanges();
        }
    }
}
