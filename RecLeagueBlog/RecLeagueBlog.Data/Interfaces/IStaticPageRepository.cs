﻿using RecLeagueBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecLeagueBlog.Data.Interfaces
{
    public interface IStaticPageRepository 
    {
        List<StaticPage> GetAllPages();
        StaticPage GetPageByID(int staticPageId);
        void CreateStaticPage(StaticPage newPage);
        void EditStaticPage(StaticPage updatedPage);
        void DeleteStaticPage(int staticPageId);

    }
}