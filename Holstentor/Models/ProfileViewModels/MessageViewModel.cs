using Holstentor.Models.Plugin;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Holstentor.Models.ProfileViewModels
{
    public class MessageViewModel
    {
        public int ID { get; set; }
        public string UserIdSend { get; set; }
        public string UserIdRecive { get; set; }
        public string UsernameSend { get; set; }
        public string Text { get; set; }
        public string TextAdmin { get; set; }
        public DateTime Date { get; set; }
        public Nullable<DateTime> DateUpdate { get; set; }
        public bool Confirm { get; set; }
    }
}