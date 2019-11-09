using BudgetMe.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BudgetMe.App_Start
{
    public class IdentityConfig
    {
        public void Configuration(IAppBuilder app)
        {
            app.CreatePerOwinContext(() => new BudgetMeDbContext());
            app.CreatePerOwinContext<AppUserManager>(AppUserManager.Create);
            /*app.CreatePerOwinContext<RoleManager<Role>>((options, context) =>
                new RoleManager<Role>(
                    new RoleStore<Role>(context.Get<BudgetMeDbContext>("KEY"))));*/

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Home/Login"),
            });
        }
    }
}