using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace RecLeagueBlog.Models
{
    public class AddPostViewModel
    {
        public int BlogPostId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string StatusName { get; set; }
        public string UserName { get; set; }

        public string [] TagsToPost { get; set; }
        public string [] Categories { get; set; }
    }
}
