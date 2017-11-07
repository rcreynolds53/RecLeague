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
        public void CreateStaticPage(StaticPage newPage)
        {
            throw new NotImplementedException();
        }

        public void DeleteStaticPage(int staticPageId)
        {
            throw new NotImplementedException();
        }

        public void EditStaticPage(StaticPage updatedPage)
        {
            throw new NotImplementedException();
        }

        public List<StaticPage> GetAllPages()
        {
            throw new NotImplementedException();
        }

        public StaticPage GetPageByID(int staticPageId)
        {
            throw new NotImplementedException();
        }
    }
}
