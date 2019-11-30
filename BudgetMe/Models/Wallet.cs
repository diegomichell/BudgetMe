using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BudgetMe.Models
{
    public class Wallet
    {
        public int Id { get; set; }
        [DisplayName("Titulo")]
        [Required]
        public string Title { get; set; }
        [DisplayName("Descripción")]
        public string Description { get; set; }
        public string UserId { get; set; }
        [NotMapped]
        [DisplayName("Balance")]
        public decimal? Balance { get; set; } = 0;
    }
}