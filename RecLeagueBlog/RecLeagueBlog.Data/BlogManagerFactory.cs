using RecLeagueBlog.Data.Interfaces;
using RecLeagueBlog.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecLeagueBlog.Data
{
   public static class BlogManagerFactory
    {
        

        public static BlogManager Create()
        {
            string mode = ConfigurationManager.AppSettings["Mode"].ToString();

            switch (mode)
            {
                case "Mock":
                    return new BlogManager(new MockTagRepository(), new MockCategoryRepository(), new MockPostRepository());
                default:
                    throw new Exception("Mode value in app config is not valid.");
            }
        }
    }
}
