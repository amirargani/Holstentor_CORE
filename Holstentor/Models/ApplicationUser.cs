using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Holstentor.Models.Plugin;

namespace Holstentor.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        [MaxLength(256)]
        [Display(Name = "Vorname")]
        public string Name { get; set; }

        [MaxLength(256)]
        [Display(Name = "Familienname")]
        public string NameFamily { get; set; }
        public DateTime Date { get; set; }
    }
}