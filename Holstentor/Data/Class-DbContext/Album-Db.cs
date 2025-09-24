using Holstentor.Models.Plugin;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

// Album
namespace Holstentor.Data.Class_DbContext
{
    public class Album_Db
    {
        [Key]
        public int ID { get; set; }
        [MaxLength(50, ErrorMessage = Message.MaxLengthMsgDer)]
        [Required(AllowEmptyStrings = false, ErrorMessage = Message.RequiredMsgDer)]
        [Display(Name = "Fotoname")]
        public string NamePic { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = Message.RequiredMsgDas)]
        [Display(Name = "Foto")]
        public string Image { get; set; }
        public DateTime Date { get; set; }
    }
}