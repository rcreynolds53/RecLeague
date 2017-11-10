using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecLeagueBlog.Models
{
    public class StaticPage
    {
        public int StaticPageId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        public virtual Status Status { get; set; }

    }
}
