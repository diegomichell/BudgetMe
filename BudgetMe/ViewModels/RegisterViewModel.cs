using System.ComponentModel;

namespace BudgetMe.ViewModels
{
    public class RegisterViewModel
    {
        [DisplayName("Usuario")]
        public string UserName { get; set; }
        [DisplayName("Contraseña")]
        public string Password { get; set; }
        [DisplayName("Nombre")]
        public string FirstName { get; set; }
        [DisplayName("Apellido")]
        public string LastName { get; set; }
        [DisplayName("Correo")]
        public string Email { get; set; }
    }
}