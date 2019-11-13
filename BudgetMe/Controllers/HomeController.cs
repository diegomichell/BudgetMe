using BudgetMe.App_Start;
using BudgetMe.Models;
using BudgetMe.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BudgetMe.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            if(User.Identity.IsAuthenticated)
            {
                var context = new BudgetMeDbContext();
                var user = AppUserManager.GetUser();
                var transactions = context.Transactions.Where(t => t.UserId == user.Id).ToList();
                var incomeTransactions = transactions.Where(t => t.TransactionType == TransactionType.INCOME);
                var expenseTransactions = transactions.Where(t => t.TransactionType == TransactionType.EXPENSE);

                var incomesResult = incomeTransactions.GroupBy(t => t.UserId).Select(t => new { incomes = t.Sum(ts => ts.Amount) }).First();
                var expensesResult = expenseTransactions.GroupBy(t => t.UserId).Select(t => new { expenses = t.Sum(ts => ts.Amount) }).First();

                var resume = new ResumeViewModel
                {
                    balance = incomesResult.incomes - expensesResult.expenses,
                    incomes = incomesResult.incomes,
                    expenses = expensesResult.expenses
                };

                var transactionsByCategory = transactions.GroupBy(t => t.Category).Select(t => new { category = t.Key, total = t.Sum(ts => ts.Amount) });

                List<DataPoint> dataPoints = new List<DataPoint>();

               foreach(var tbc in transactionsByCategory)
               {
                    dataPoints.Add(new DataPoint(tbc.category.ToString(), tbc.total));
               }

                ViewBag.DataPoints = JsonConvert.SerializeObject(dataPoints);
                ViewBag.resume = resume;
            }

            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(LoginViewModel login)
        {
            if (ModelState.IsValid)
            {
                var userManager = HttpContext.GetOwinContext().GetUserManager<AppUserManager>();
                var authManager = HttpContext.GetOwinContext().Authentication;

                User user = userManager.Find(login.UserName, login.Password);
                if (user != null)
                {
                    var ident = userManager.CreateIdentity(user,
                        DefaultAuthenticationTypes.ApplicationCookie);
                    //use the instance that has been created. 
                    authManager.SignIn(
                        new AuthenticationProperties { IsPersistent = false }, ident);
                    return Redirect(login.ReturnUrl ?? Url.Action("Index", "Home"));
                }
            }
            ModelState.AddModelError("", "Invalid username or password");
            return View(login);
        }

        public ActionResult LogOut()
        {
            Request.GetOwinContext().Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Register(RegisterViewModel register)
        {
            if (ModelState.IsValid)
            {
                var userManager = HttpContext.GetOwinContext().GetUserManager<AppUserManager>();
                User user = new User { UserName = register.UserName, Email = register.Email, FirstName = register.FirstName, LastName = register.LastName };
                var ident = userManager.Create(user, register.Password);

                if(!ident.Succeeded)
                {
                    ModelState.AddModelError("", "Usuario o contraseña incorrecta");
                    return View(register);
                }

                return Redirect(Url.Action("Login", "Home"));
            }

            ModelState.AddModelError("","Existen campos invalidos");
            return View(register);
        }

    }
}
