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
        [Display(Name="Gasto")]
        EXPENSE,
        [Display(Name = "Ingreso")]
        INCOME
    }

    public enum CategoryType
    {
        [Display(Name = "Salario")]
        SALARY,
        [Display(Name = "Renta")]
        RENT,
        [Display(Name = "Comida")]
        FOOD,
        [Display(Name = "Servicio")]
        SERVICE,
        [Display(Name = "Combustible")]
        GAS,
        [Display(Name = "Factura")]
        BILL,
        [Display(Name = "Otros")]
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
        [DisplayName("Cartera")]
        [Required]
        public int WalletId { get; set; }
        public Wallet Wallet { get; set; }

        public Transaction() {
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
        }
    }
}