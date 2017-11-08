using RecLeagueBlog.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecLeagueBlog.Models;

namespace RecLeagueBlog.Data.MockRepositories
{
    public class MockStaticPageRepository : IStaticPageRepository
    {
        private static List<StaticPage> _pages;
        public void CreateStaticPage(StaticPage newPage)
        {
            if (_pages.Any())
            {
                newPage.StaticPageId = _pages.Max(p => p.StaticPageId) + 1;
            }
            else
            {
                newPage.StaticPageId = 1;
            }
            _pages.Add(newPage);
        }

        public void DeleteStaticPage(int staticPageId)
        {
            _pages.RemoveAll(p => p.StaticPageId == staticPageId);
        }

        public void EditStaticPage(StaticPage updatedPage)
        {
            _pages.RemoveAll(p => p.StaticPageId == updatedPage.StaticPageId);
            _pages.Add(updatedPage);
        }

        public List<StaticPage> GetAllPages()
        {
            return _pages.OrderBy(p => p.StaticPageId).ToList();
        }

        public StaticPage GetPageByID(int staticPageId)
        {
            return _pages.FirstOrDefault(p => p.StaticPageId == staticPageId);
        }
    }
}
