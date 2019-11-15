using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BudgetMe.ViewModels
{
    public class TransactionGroup
    {
        public string Category { get; set; }
        public decimal Total { get; set; }

        public TransactionGroup(string Category, decimal Total)
        {
            this.Category = Category;
            this.Total = Total;
        }
    }
}