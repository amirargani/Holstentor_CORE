using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Holstentor.Models.Plugin;

// Settings
namespace Holstentor.Data.Class_DbContext
{
    public class Einstellungen_Db
    {
        [Key]
        public int IDSetting { get; set; }
        [Display(Name = "Website Description")]
        [MaxLength(150, ErrorMessage = "")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "")]
        public string Description { get; set; }
        [MaxLength(75, ErrorMessage = "")]
        [Display(Name = "Website Title")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "")]
        public string Title { get; set; }
        [Display(Name = "Page Number")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "")]
        public int PageNumber { get; set; }
        [Display(Name = " Smtp")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "")]
        public string Smtp { get; set; }
        [Display(Name = "Email")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "")]
        public string Email { get; set; }
        [Display(Name = "Password Email")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "")]
        public string PasswordEmail { get; set; }
    }
}