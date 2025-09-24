using Holstentor.Models.Plugin;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Holstentor.Data.Class_DbContext
{
    public class IndexET_Db
    {
        [Key]
        public int IDIndex { get; set; }
        [Required(ErrorMessage = Message.RequiredMsgEmail)]
        [MaxLength(256, ErrorMessage = Message.MaxLengthMsgDie)]
        [EmailAddress(ErrorMessage = Message.Email)]
        [Display(Name = "E-Mail-Adresse")]
        public string Email { get; set; }
        [DataType(DataType.PhoneNumber)]
        [Required(AllowEmptyStrings = false, ErrorMessage = Message.RequiredMsgDie)]
        [Display(Name = "Telefonnummer")]
        public string PhoneNumber { get; set; }
        [Required]
        public string NameSite { get; set; }
        [Required]
        public string Street { get; set; }
        [Required]
        public string Number { get; set; }
        [Required]
        public string PostCode { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public string EmbedLinkGoogleMap { get; set; }
        [Required]
        public string Description { get; set; }
    }
}