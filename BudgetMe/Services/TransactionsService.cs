using BudgetMe.App_Start;
using BudgetMe.Models;
using BudgetMe.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BudgetMe.Services
{
    public class TransactionsService
    {
        BudgetMeDbContext context = new BudgetMeDbContext();

        protected IEnumerable<Transaction> GetUserTransactions()
        {
            var user = AppUserManager.GetUser();
            return context.Transactions.Where(t => t.UserId == user.Id).ToList();
        }

        public decimal GetTotalIncomes()
        {
            var transactions = GetUserTransactions().Where(t => t.TransactionType == TransactionType.INCOME);
            return transactions.Sum(t => t.Amount);
        }

        public decimal GetTotalExpenses()
        {
            var transactions = GetUserTransactions().Where(t => t.TransactionType == TransactionType.EXPENSE);
            return transactions.Sum(t => t.Amount);
        }

        public IEnumerable<TransactionGroup> GetExpensesByCategory()
        {
            var transactions = GetUserTransactions().Where(t => t.TransactionType == TransactionType.EXPENSE);
            return transactions.
                GroupBy(t => t.Category)
                .Select(t => new TransactionGroup(t.Key.ToString(), t.Sum(ts => ts.Amount)));
        }

        public IEnumerable<TransactionGroup> GetIncomesByCategory()
        {
            var transactions = GetUserTransactions().Where(t => t.TransactionType == TransactionType.INCOME);
            return transactions.
                GroupBy(t => t.Category)
                .Select(t => new TransactionGroup(t.Key.ToString(), t.Sum(ts => ts.Amount)));
        }
    }
}