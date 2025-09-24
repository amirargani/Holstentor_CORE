using Holstentor.Models;
using Holstentor.Models.Plugin;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Holstentor.Data.Class_DbContext
{
    // Products
    public class Produkte_Db
    {
        [Key]
        public int ID { get; set; }
        [MaxLength(50, ErrorMessage = Message.MaxLengthMsgDer)]
        [Required(AllowEmptyStrings = false, ErrorMessage = Message.RequiredMsgDer)]
        [Display(Name = "Produkttitel")]
        public string Title { get; set; }
        [MaxLength(500, ErrorMessage = Message.MaxLengthMsgDie)]
        [Display(Name = "Beschreibung")]
        public string Description { get; set; }
        public Nullable<int> CategoryID { get; set; }
        public Nullable<int> SubCategoryID { get; set; }
        [MaxLength(50, ErrorMessage = Message.MaxLengthMsgDer)]
        [Required(AllowEmptyStrings = false, ErrorMessage = Message.RequiredMsgDer)]
        [Display(Name = "Preis-1")]
        public string Price1 { get; set; }
        [MaxLength(50, ErrorMessage = Message.MaxLengthMsgDer)]
        [Display(Name = "Preis-2")]
        public string Price2 { get; set; }
        [MaxLength(50, ErrorMessage = Message.MaxLengthMsgDer)]
        [Display(Name = "Preis-3")]
        public string Price3 { get; set; }
        public bool ActiveInactive { get; set; }
        public DateTime Date { get; set; }
        public string UserID { get; set; }

        [ForeignKey(nameof(UserID))]
        public virtual ApplicationUser Tbl_User { get; set; }

        [ForeignKey(nameof(CategoryID))]
        public virtual Kategorie_Db Tbl_Kategorie { get; set; }

        [ForeignKey(nameof(SubCategoryID))]
        public virtual Unterkategorie_Db Tbl_Unterkategorie { get; set; }
        public virtual ICollection<Galerie_Db> Tbl_Galerie { get; set; }
    }
}