﻿using RecLeagueBlog.Data.Interfaces;
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
        private static List<Status> _statuses;

        static MockStaticPageRepository()
        {
            _statuses = new List<Status>()
            {
                new Status {StatusId = 1, StatusName = "Published" },
                new Status {StatusId = 2, StatusName = "Pending"},
                new Status {StatusId =3, StatusName ="Deleted"}
            };

            _pages = new List<StaticPage>()
            {
                new StaticPage
                { StaticPageId = 1, Title = "Football Champs", Content = "Music piracy book deals having gay friends t-shirts study abroad standing " +
                "still at concerts co-ed sports knowing what's best for poor people wrigley field making you feel bad about not going outside. " +
                "The world cup conan o'brien mad men sea salt ugly sweater parties sweaters having gay friends bad memories of high school graduate school musical comedy threatening to move to canada expensive sandwiches " +
                "toyota prius kitchen gadgets asian fusion food plays the daily show/colbert report architecture vegan/vegetarianism wrigley field david sedaris wine microbreweries yoga having black friends wes anderson movies coffee. " +
                "Picking their own fruit banksy hating people who wear ed hardy moleskine notebooks funny or ironic tattoos taking a year off ugly sweater parties san francisco the wire hating corporations the idea of soccer musical " +
                "comedy oscar parties study abroad divorce expensive sandwiches knowing what's best for poor people toyota prius asian fusion food renovations wrigley field being an expert on your culture awareness wes anderson movies " +
                "barack obama religions their parents don't belong to. Taking a year off being offended book deals study abroad sushi wrigley field manhattan (now brooklyn too!) david sedaris having two last names writers workshops " +
                "traveling hating their parents tea wes anderson movies. Banksy pea coats facebook the wire oscar parties toyota prius vintage indie music wine religions their parents don't belong to. Funny or ironic tattoos taking a" +
                " year off rugby san francisco the wire the idea of soccer difficult breakups divorce recycling natural medicine juno dogs sarah silverman vintage apple products snowboarding wrigley field david sedaris being an expert on " +
                "your culture wes anderson movies religions their parents don't belong to coffee. Hating people who wear ed hardy taking a year off halloween being offended st. patrick's day juno sarah silverman irony plays snowboarding.",
                Status = _statuses[0]
                },

                new StaticPage
                 { StaticPageId = 2, Title = "Curling Canceled", Content = "It is with sadness that we announce that the men's curling league has been canceled due to a lack of interest in the dying sport.", Status = _statuses[0]},

                new StaticPage
                 { StaticPageId = 3, Title = "Pending Status Page", Content = "It is with sadness that we announce that the men's curling league has been canceled due to a lack of interest in the dying sport.", Status = _statuses[1]},

                new StaticPage
                 { StaticPageId = 4, Title = "Delete Status Page", Content = "It is with sadness that we announce that the men's curling league has been canceled due to a lack of interest in the dying sport.", Status = _statuses[2]},
            };
        }

        public StaticPageViewModel ConvertPageToVm(StaticPage staticPage)
        {
            StaticPageViewModel viewModel = new StaticPageViewModel();
            viewModel.StaticPage = staticPage;
            return viewModel;
        }

        public void ConvertVmToPage(StaticPageViewModel viewModel)
        {
            StaticPage convertedPage = _pages.Single(p => p.StaticPageId == viewModel.StaticPage.StaticPageId);
            convertedPage.Title = viewModel.StaticPage.Title;
            convertedPage.Content = viewModel.StaticPage.Content;
            convertedPage.StaticPageId = viewModel.StaticPage.StaticPageId;
            convertedPage.Status = viewModel.StaticPage.Status;

            _pages.RemoveAll(p => p.StaticPageId == convertedPage.StaticPageId);
            _pages.Add(convertedPage);
        }

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

        public List<StaticPage> GetAllPublishedPages()
        {
            return _pages.Where(p => p.Status.StatusId == 1).ToList();
        }

        public List<StaticPage> GetAllPublishStaticPages()
        {
            return _pages.Where(p => p.Status.StatusId == 1).ToList();
        }

        public IEnumerable<Status> GetAllStatuses()
        {
            return _statuses;
        }

        public StaticPage GetPageByID(int staticPageId)
        {
            return _pages.FirstOrDefault(p => p.StaticPageId == staticPageId);
        }
    }
}
