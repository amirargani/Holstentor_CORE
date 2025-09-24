using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Holstentor.Models.HomeViewModels
{
    public class IndexEXViewModel
    {
        public int IDIndex { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string NameSite { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
        public string PostCode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string EmbedLinkGoogleMap { get; set; }
        public string Description { get; set; }
    }
}
