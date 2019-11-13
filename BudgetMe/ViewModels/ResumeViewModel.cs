using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BudgetMe.ViewModels
{
    public class ResumeViewModel
    {
        public decimal balance {get;set;}
        public decimal incomes { get; set; }
        public decimal expenses { get; set; }
    }
}