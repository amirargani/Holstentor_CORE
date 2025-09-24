using Holstentor.Models.Plugin;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Holstentor.Models.HomeViewModels
{
    public class MessageUserViewModel
    {
        [MaxLength(20, ErrorMessage = Message.MaxLengthMsgDer)]
        [Required(AllowEmptyStrings = false, ErrorMessage = Message.RequiredMsgDer)]
        [Display(Name = "Name")]
        public string name { get; set; }
        [Required(ErrorMessage = Message.RequiredMsgEmail)]
        [EmailAddress(ErrorMessage = Message.Email)]
        [Display(Name = "E-Mail-Adresse")]
        public string email { get; set; }
        [MaxLength(250, ErrorMessage = Message.MaxLengthMsgDie)]
        [Required(AllowEmptyStrings = false, ErrorMessage = Message.RequiredMsgDie)]
        [Display(Name = "Nachricht")]
        public string message { get; set; }
    }
}