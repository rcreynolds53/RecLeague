﻿using RecLeagueBlog.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecLeagueBlog.Models;

namespace RecLeagueBlog.Data.EFRepositories
{
    public class EFStaticPageRepository : IStaticPageRepository
    {
        RecBlogDBContext context = new RecBlogDBContext();

        public void CreateStaticPage(StaticPage newPage)
        {
            context.StaticPages.Add(newPage);
            context.SaveChanges();
        }

        public void DeleteStaticPage(int staticPageId)
        {
            var post = (from p in context.StaticPages
                        where p.StaticPageId == staticPageId
                        select p).FirstOrDefault();
            context.StaticPages.Remove(post);
            context.SaveChanges();
        }

        public void EditStaticPage(StaticPage updatedPage)
        {
            context.Entry(updatedPage).State = System.Data.Entity.EntityState.Modified;
            context.SaveChanges();
        }

        public List<StaticPage> GetAllPages()
        {
            var pages = (from p in context.StaticPages
                         select p).ToList();

            return pages;
        }

        public List<StaticPage> GetAllPublishedPages()
        {
            var pages = (from p in context.StaticPages
                        where p.Status.StatusId == 1
                        select p).ToList();

            return pages;
        }

        public StaticPage GetPageByID(int staticPageId)
        {
            var page = (from p in context.StaticPages
                        where p.StaticPageId == staticPageId
                        select p).FirstOrDefault();

            return page;
        }
    }
}
