using Holstentor.Models.Plugin;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Holstentor.Data.Class_DbContext
{
    // Message
    public class Nachricht_Db
    {
        [Key]
        public int ID { get; set; }
        public string UserIdSend { get; set; }
        public string UserIdRecive { get; set; }
        [MaxLength(250, ErrorMessage = Message.MaxLengthMsgDie)]
        [Required(AllowEmptyStrings = false, ErrorMessage = Message.RequiredMsgDie)]
        [Display(Name = "Nachricht")]
        public string Text { get; set; }
        [MaxLength(250, ErrorMessage = Message.MaxLengthMsgDie)]
        [Required(AllowEmptyStrings = false, ErrorMessage = Message.RequiredMsgDie)]
        [Display(Name = "Nachricht")]
        public string TextAdmin { get; set; }
        public DateTime Date { get; set; }
        public Nullable<DateTime> DateUpdate { get; set; }
        public bool Confirm { get; set; }
    }
}