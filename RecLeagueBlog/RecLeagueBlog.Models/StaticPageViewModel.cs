using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace RecLeagueBlog.Models
{
    public class StaticPageViewModel
    {
        public StaticPage StaticPage { get; set; }
        public List<SelectListItem> StatusItems { get; set; }

        public StaticPageViewModel()
        {
            StatusItems = new List<SelectListItem>();
        }

        public void SetStatusItems(IEnumerable<Status> statuses)
        {
            foreach (var status in statuses)
            {
                StatusItems.Add(new SelectListItem()
                {
                    Value = status.StatusId.ToString(),
                    Text = status.StatusName
                });
            }
        }
    }
}
