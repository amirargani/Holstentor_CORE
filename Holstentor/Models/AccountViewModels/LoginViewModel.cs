using Holstentor.Models.Plugin;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Holstentor.Models.AccountViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = Message.RequiredMsgEmail)]
        [EmailAddress(ErrorMessage = Message.Email)]
        [Display(Name = "E-Mail-Adresse")]
        public string Email { get; set; }
        [Required(ErrorMessage = Message.RequiredMsgPassword)]
        [Display(Name = "Passwort")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}