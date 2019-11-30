using BudgetMe.Models;
using BudgetMe.Services;
using BudgetMe.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BudgetMe.Controllers
{
    public class StateController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Estado de situación";
            var transactionsService = new TransactionsService();

            var generalState = new GeneralStateViewModel
            {
                ExpensesList = transactionsService.GetExpensesByCategory().ToList(),
                IncomesList = transactionsService.GetIncomesByCategory().ToList(),
                TotalExpenses = transactionsService.GetTotalExpenses(),
                TotalIncomes = transactionsService.GetTotalIncomes(),
                Balance = transactionsService.GetBalance()
            };

            return View(generalState);
        }
    }
}