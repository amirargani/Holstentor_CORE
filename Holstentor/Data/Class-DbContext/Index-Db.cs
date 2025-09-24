using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Holstentor.Models.Plugin;

// Home
namespace Holstentor.Data.Class_DbContext
{
    public class Index_Db
    {
        // ** Code prop **
        //[RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$", ErrorMessage = Message.RequiredMsgTitel)]
        [Key]
        public int IDIndex { get; set; }
        [MaxLength(100, ErrorMessage = Message.MaxLengthMsgDer)]
        [Required(AllowEmptyStrings = false, ErrorMessage = Message.RequiredMsgDer)]
        [Display(Name ="Titel")]
        public string Title { get; set; }
        [MaxLength(300, ErrorMessage = Message.MaxLengthMsgDer)]
        [Required(AllowEmptyStrings = false, ErrorMessage = Message.RequiredMsgDer)]
        [Display(Name = "Untertitel-1")]
        public string TypedText1 { get; set; }
        [MaxLength(300, ErrorMessage = Message.MaxLengthMsgDer)]
        [Display(Name = "Untertitel-2")]
        public string TypedText2 { get; set; }
        [MaxLength(300, ErrorMessage = Message.MaxLengthMsgDer)]
        [Display(Name = "Untertitel-3")]
        public string TypedText3 { get; set; }
        [MaxLength(300, ErrorMessage = Message.MaxLengthMsgDer)]
        [Display(Name = "Untertitel-4")]
        public string TypedText4 { get; set; }
        [MaxLength(300, ErrorMessage = Message.MaxLengthMsgDer)]
        [Display(Name = "Untertitel-5")]
        public string TypedText5 { get; set; }
    }
}