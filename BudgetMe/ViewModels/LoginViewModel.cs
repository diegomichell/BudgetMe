using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BudgetMe.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [DisplayName("Usuario")]
        public string UserName { get; set; }
        [Required]
        [DisplayName("Contraseña")]
        public string Password { get; set; }
        public string ReturnUrl { get; set; }
    }
}