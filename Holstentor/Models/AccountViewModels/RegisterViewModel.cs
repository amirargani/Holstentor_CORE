using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Holstentor.Models.Plugin;

namespace Holstentor.Models.AccountViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = Message.RequiredMsgEmail)]
        [EmailAddress(ErrorMessage = Message.Email)]
        [Display(Name = "E-Mail-Adresse")]
        public string Email { get; set; }
        [Required(ErrorMessage = Message.RequiredMsgPassword)]
        // The {0} must be at least {2} and at max {1} characters long.
        //[StringLength(100, ErrorMessage = "Das {0} muss mindestens {2} und maximal {1} Buchstaben haben.", MinimumLength = 6)]
        [StringLength(100, ErrorMessage = "Das {0} muss mindestens {2} Buchstaben haben.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Passwort")]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "Passwort Wiederholung")]
        // The password and confirmation password do not match.
        [Compare("Password", ErrorMessage = "Das Passwort und das Bestätigung Passwort sind nicht gleich.")]
        public string ConfirmPassword { get; set; }
    }
}