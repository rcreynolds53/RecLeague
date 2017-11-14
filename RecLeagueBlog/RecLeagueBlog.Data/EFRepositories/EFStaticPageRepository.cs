using RecLeagueBlog.Data.Interfaces;
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

        public StaticPageViewModel ConvertPageToVm(StaticPage staticPage)
        {
            StaticPageViewModel viewModel = new StaticPageViewModel();
            viewModel.StaticPage = staticPage;
            return viewModel;
        }

        public void ConvertVmToPage(StaticPageViewModel viewModel)
        {
            StaticPage convertedPage = context.StaticPages.Single(p => p.StaticPageId == viewModel.StaticPage.StaticPageId);
            convertedPage.Title = viewModel.StaticPage.Title;
            convertedPage.Content = viewModel.StaticPage.Content;
            convertedPage.StaticPageId = viewModel.StaticPage.StaticPageId;
            convertedPage.Status = viewModel.StaticPage.Status;

            context.Entry(convertedPage).State = System.Data.Entity.EntityState.Modified;
            context.SaveChanges();
        }

        public void CreateStaticPage(StaticPage newPage)
        {
            context.StaticPages.Add(newPage);
            context.SaveChanges();
        }

        public void DeleteStaticPage(int staticPageId)
        {
            var post = context.StaticPages.FirstOrDefault(s => s.StaticPageId == staticPageId);
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
            return context.StaticPages.ToList();
        }

        public List<StaticPage> GetAllPublishedPages()
        {
            return context.StaticPages.Where(s => s.Status.StatusId == 1).ToList();
        }

        public List<StaticPage> GetAllPublishStaticPages()
        {
            return context.StaticPages.Where(p => p.Status.StatusId == 1).ToList();
        }

        public IEnumerable<Status> GetAllStatuses()
        {
            return context.Statuses.ToList();
        }

        public StaticPage GetPageByID(int staticPageId)
        {
            return context.StaticPages.FirstOrDefault(s => s.StaticPageId == staticPageId);
        }
    }
}
