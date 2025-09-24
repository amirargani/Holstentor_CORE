using Holstentor.Models.Plugin;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Holstentor.Data.Class_DbContext
{
    // Subcategory
    public class Unterkategorie_Db
    {
        [Key]
        public int ID { get; set; }
        [MaxLength(50, ErrorMessage = Message.MaxLengthMsgDer)]
        [Required(AllowEmptyStrings = false, ErrorMessage = Message.RequiredMsgDer)]
        [Display(Name = "Unterkategoriename")]
        public string TitleSubCategory { get; set; }
        [MaxLength(150, ErrorMessage = Message.MinLengthMsgDie)]
        [Display(Name = "Beschreibung")]
        public string Description { get; set; }
        public bool ActivePassive { get; set; }
        [MaxLength(30, ErrorMessage = Message.MinLengthMsgDie)]
        [Display(Name = "Schriftart")]
        public string FontName { get; set; }
        [MaxLength(50, ErrorMessage = Message.MinLengthMsgDer)]
        [Display(Name = "Untertitel-1")]
        public string Subtitle1 { get; set; }
        [MaxLength(50, ErrorMessage = Message.MinLengthMsgDer)]
        [Display(Name = "Untertitel-2")]
        public string Subtitle2 { get; set; }
        [MaxLength(50, ErrorMessage = Message.MinLengthMsgDer)]
        [Display(Name = "Untertitel-3")]
        public string Subtitle3 { get; set; }
        public Nullable<int> CategoryID { get; set; }

        [ForeignKey(nameof(CategoryID))]
        public virtual Kategorie_Db Tbl_Kategorie { get; set; }
    }
}