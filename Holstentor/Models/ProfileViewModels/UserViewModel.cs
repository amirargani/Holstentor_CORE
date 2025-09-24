using Holstentor.Models.Plugin;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Holstentor.Models.ProfileViewModels
{
    public class UserViewModel
    {
        public string Email { get; set; }
        public string UserName { get; set; }

        // https://sensibledev.com/phone-number-validation-in-asp-net/
        [MaxLength(12, ErrorMessage = "?")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^(\+\s?)?((?<!\+.*)\(\+?\d+([\s\-\.]?\d+)?\)|\d+)([\s\-\.]?(\(\d+([\s\-\.]?\d+)?\)|\d+))*(\s?(x|ext\.?)\s?\d+)?$", ErrorMessage = "Das Feld {0} ist keine gültige Telefonnummer.")]
        [StringLength(12, ErrorMessage = "Das {0} muss mindestens {1} und maximal {2} Buchstaben haben.", MinimumLength = 12)]
        [Required(AllowEmptyStrings = false, ErrorMessage = Message.RequiredMsgDie)]
        [Display(Name = "Telefonnummer")]
        public string PhoneNumber { get; set; }
        //[RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$", ErrorMessage = Message.RequiredMsgDer)]
        //[Required(ErrorMessage = Message.RequiredMsgPassword)]
        [StringLength(50, ErrorMessage = "Das {0} muss mindestens {1} und maximal {2} Buchstaben haben.", MinimumLength = 3)]
        [Required(AllowEmptyStrings = false, ErrorMessage = Message.RequiredMsgDer)]
        [Display(Name = "Vorname")]
        public string Name { get; set; }
        //[RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$", ErrorMessage = Message.RequiredMsgDer)]
        //[Required(ErrorMessage = Message.RequiredMsgPassword)]
        [StringLength(50, ErrorMessage = "Das {0} muss mindestens {1} und maximal {2} Buchstaben haben.", MinimumLength = 3)]
        [Required(AllowEmptyStrings = false, ErrorMessage = Message.RequiredMsgDer)]
        [Display(Name = "Familienname")]
        public string NameFamily { get; set; }
    }
}