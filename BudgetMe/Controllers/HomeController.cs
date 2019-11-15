using BudgetMe.App_Start;
using BudgetMe.Models;
using BudgetMe.Services;
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
            ViewBag.Title = "Inicio";

            if (User.Identity.IsAuthenticated)
            {
                var transactionsService = new TransactionsService();
                var incomes = transactionsService.GetTotalIncomes();
                var expenses = transactionsService.GetTotalExpenses();
                var resume = new ResumeViewModel
                {
                    balance = incomes - expenses,
                    incomes = incomes,
                    expenses = expenses
                };
                var incomesByCategory = transactionsService.GetIncomesByCategory();
                var expensesByCategory = transactionsService.GetExpensesByCategory();
                var expensesDataPoints = new List<DataPoint>();
                var incomesDataPoints = new List<DataPoint>();

                foreach (var ibc in incomesByCategory)
                {
                    incomesDataPoints.Add(new DataPoint(ibc.Category, ibc.Total));
                }

                foreach (var ebc in expensesByCategory)
                {
                    expensesDataPoints.Add(new DataPoint(ebc.Category, ebc.Total));
                }

                ViewBag.IncomesDataPoints = JsonConvert.SerializeObject(incomesDataPoints);
                ViewBag.ExpensesDataPoints = JsonConvert.SerializeObject(expensesDataPoints);
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

                if (!ident.Succeeded)
                {
                    ModelState.AddModelError("", "Usuario o contraseña incorrecta");
                    return View(register);
                }

                return Redirect(Url.Action("Login", "Home"));
            }

            ModelState.AddModelError("", "Existen campos invalidos");
            return View(register);
        }

    }
}
