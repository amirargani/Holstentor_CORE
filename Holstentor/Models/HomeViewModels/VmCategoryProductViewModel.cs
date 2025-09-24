using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Holstentor.Models.HomeViewModels
{
    public class VmCategoryProductViewModel
    {
        public int IDProduct { get; set; }
        public string TitleProduct { get; set; }
        public string DescriptionProduct { get; set; }
        public string PriceProduct1 { get; set; }
        public string PriceProduct2 { get; set; }
        public string PriceProduct3 { get; set; }
        public DateTime DateProduct { get; set; }
        public int? IDCategory { get; set; }
        public string TitleCategory { get; set; }
        public string DescriptionCategory { get; set; }
        public int? IDSubCategory { get; set; }
        public string TitleSubCategory { get; set; }
        public string DescriptionSubCategory { get; set; }
        public string SubtitleSubCategory1 { get; set; }
        public string SubtitleSubCategory2 { get; set; }
        public string SubtitleSubCategory3 { get; set; }
        public string NamePicGallery { get; set; }
        public string ImageGallery { get; set; }
    }
}