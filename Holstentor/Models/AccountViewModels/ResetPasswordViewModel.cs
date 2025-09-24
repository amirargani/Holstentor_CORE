using Holstentor.Models.Plugin;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Holstentor.Models.AccountViewModels
{
    public class ResetPasswordViewModel
    {
        [Required(ErrorMessage = Message.RequiredMsgEmail)]
        [EmailAddress(ErrorMessage = Message.Email)]
        [Display(Name = "E-Mail-Adresse")]
        public string Email { get; set; }
        [Required(ErrorMessage = Message.RequiredMsgPassword)]
        [StringLength(100, ErrorMessage = "Das {0} muss mindestens {2} Buchstaben haben.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Passwort")]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "Passwort Wiederholung")]
        [Compare("Password", ErrorMessage = "Das Passwort und das Bestätigung Passwort sind nicht gleich.")]
        public string ConfirmPassword { get; set; }
        public string Code { get; set; }
    }
}