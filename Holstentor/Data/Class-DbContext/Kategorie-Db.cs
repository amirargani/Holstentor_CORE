using Holstentor.Models.Plugin;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Holstentor.Data.Class_DbContext
{
    // Category
    public class Kategorie_Db
    {
        [Key]
        public int ID { get; set; }
        [MaxLength(50, ErrorMessage = Message.MaxLengthMsgDer)]
        [Required(AllowEmptyStrings = false, ErrorMessage = Message.RequiredMsgDer)]
        [Display(Name = "Kategoriename")]
        public string TitleCategory { get; set; }
        [MaxLength(150, ErrorMessage = Message.MaxLengthMsgDie)]
        [Display(Name = "Beschreibung")]
        public string Description { get; set; }
        public bool ActivePassive { get; set; }
        [MaxLength(30, ErrorMessage = Message.MaxLengthMsgDie)]
        [Display(Name = "Schriftart")]
        public string FontName { get; set; }

        public virtual ICollection<Unterkategorie_Db> Tbl_Unterkategorie { get; set; }

    }
}