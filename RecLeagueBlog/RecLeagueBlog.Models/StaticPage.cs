using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace RecLeagueBlog.Models
{
    public class StaticPage
    {
        public int StaticPageId { get; set; }
        public string Title { get; set; }

        [UIHint("tinymce_jquery_full"), AllowHtml]
        public string Content { get; set; }

        public virtual Status Status { get; set; }

    }
}
