using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecLeagueBlog.Models
{
    public class BlogPost
    {
        public int BlogPostId { get; set; }
        public string Title { get; set; }
        public DateTime DateCreated { get; set; }
        public int StatusId { get; set; }
        public int EmployeeId { get; set; }

        public virtual Status Status { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual ICollection<Tag> Tags { get; set; }
        public virtual ICollection<Category> Categories { get; set; }
    }
}
