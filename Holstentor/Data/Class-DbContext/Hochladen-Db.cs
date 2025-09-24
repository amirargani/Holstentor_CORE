using Holstentor.Models.Plugin;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Holstentor.Data.Class_DbContext
{
    // Upload
    public class Hochladen_Db
    {
        [Key]
        public int ID { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = Message.RequiredMsgDas)]
        [Display(Name = "Video")]
        public string Video { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = Message.RequiredMsgDas)]
        [Display(Name = "Logo")]
        public string Logo { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = Message.RequiredMsgDas)]
        [Display(Name = "Foto")]
        public string ImageAbout { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = Message.RequiredMsgDas)]
        [Display(Name = "Foto")]
        public string ImageGallery { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = Message.RequiredMsgDie)]
        [Display(Name = "Foto")]
        public string ImageFooter { get; set; }
    }
}
