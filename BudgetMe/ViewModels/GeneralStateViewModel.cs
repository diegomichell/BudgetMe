using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace BudgetMe.ViewModels
{
    public class GeneralStateViewModel
    {
        [DisplayName("Ingresos")]
        public List<TransactionGroup> IncomesList { get; set; }
        [DisplayName("Gastos")]
        public List<TransactionGroup> ExpensesList { get; set; }
        [DisplayName("Total Ingresos")]
        public decimal TotalIncomes { get; set; }
        [DisplayName("Total Gastos")]
        public decimal TotalExpenses { get; set; }
        [DisplayName("Disponible")]
        public decimal Balance { get; set; }
    }
}