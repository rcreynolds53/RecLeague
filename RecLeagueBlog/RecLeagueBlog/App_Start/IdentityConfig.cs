using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using RecLeagueBlog.Data;
using RecLeagueBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RecLeagueBlog.Models.Identity;

namespace RecLeagueBlog.App_Start
{
    public class IdentityConfig
    {
        public void Configuration(IAppBuilder app)
        {

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Home/Login"),
            });


            app.CreatePerOwinContext(() => new RecBlogDBContext());
            app.CreatePerOwinContext<UserManager<IdentityUser>>((options, context) => new UserManager<IdentityUser>(new UserStore<IdentityUser>(context.Get<RecBlogDBContext>())));
            app.CreatePerOwinContext(() => new RecBlogDBContext());
            app.CreatePerOwinContext<RoleManager<IdentityRole>>((options, context) => new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context.Get<RecBlogDBContext>())));
        }
    }

}