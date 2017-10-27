using RecLeagueBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecLeagueBlog.Data.Interfaces
{
   public interface ITagRepository
    {
        List<Tag> GetAllTags();
        Tag GetTag(int tagId);
        void CreateTag(Tag newTag);
        void UpdateTag(Tag updatedTag);
        void DeleteTag(int tagId);
    }
}
