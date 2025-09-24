using Holstentor.Models.Plugin;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Holstentor.Models.ProfileViewModels
{
    public class ChangePasswordViewModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = Message.RequiredMsgDas)]
        [DataType(DataType.Password)]
        [Display(Name = "Aktuelles Passwort")]
        public string OldPassword { get; set; }
        [Required(ErrorMessage = Message.RequiredMsgPassword)]
        // The {0} must be at least {2} and at max {1} characters long.
        [StringLength(100, ErrorMessage = "Das {0} muss mindestens {1} und maximal {2} Buchstaben haben.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Passwort")]
        public string NewPassword { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "Passwort Wiederholung")]
        [Compare("NewPassword", ErrorMessage = "Das Passwort und das Bestätigungspasswort sind nicht gleich.")]
        public string ConfirmPassword { get; set; }
    }
}