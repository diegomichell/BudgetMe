using BudgetMe.App_Start;
using BudgetMe.Models;
using BudgetMe.Util;
using BudgetMe.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BudgetMe.Services
{
    public class TransactionsService
    {
        BudgetMeDbContext context = new BudgetMeDbContext();
        public IEnumerable<Transaction> GetUserTransactions()
        {
            var user = AppUserManager.GetUser();
            return from t in context.Transactions where t.UserId == user.Id select t;
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
                .Select(t => new TransactionGroup(t.Key.GetDisplayName(), t.Sum(ts => ts.Amount)));
        }

        public IEnumerable<TransactionGroup> GetIncomesByCategory()
        { 
            var transactions = GetUserTransactions().Where(t => t.TransactionType == TransactionType.INCOME);
            return transactions.
                GroupBy(t => t.Category)
                .Select(t => new TransactionGroup(t.Key.GetDisplayName(), t.Sum(ts => ts.Amount)));
        }

        public decimal GetBalanceForWallet(int walletId)
        {
            var walletTransactions = context.Transactions.Where(t => t.WalletId == walletId).ToList();
            var incomes = walletTransactions.Where(t => t.TransactionType == TransactionType.INCOME).Sum(t => t.Amount);
            var expenses = walletTransactions.Where(t => t.TransactionType == TransactionType.EXPENSE).Sum(t => t.Amount);
            return Math.Round(incomes - expenses);
        }
    }
}