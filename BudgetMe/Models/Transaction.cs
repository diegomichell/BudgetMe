using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BudgetMe.Models
{
    public enum TransactionType
    {
        EXPENSE,
        INCOME
    }

    public enum CategoryType
    {
        SALARY,
        RENT,
        FOOD,
        SERVICE,
        GAS,
        BILL,
        OTHER
    }

    public class Transaction
    {
        public int Id { get; set; }
        [Required]
        [DisplayName("Monto")]
        public decimal Amount { get; set; }
        [Required]
        [DisplayName("Descripción")]
        public string Description { get; set; }
        [Required]
        [DisplayName("Tipo de transacción")]
        public TransactionType TransactionType { get; set; }
        [Required]
        [DisplayName("Categoria")]
        public CategoryType Category { get; set; }
        [DisplayName("Creada")]
        public DateTime CreatedAt { get; set; }
        [DisplayName("Actualizada")]
        public DateTime UpdatedAt { get; set; }
        public string UserId { get; set; }

        public Transaction() {
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
        }
    }
}