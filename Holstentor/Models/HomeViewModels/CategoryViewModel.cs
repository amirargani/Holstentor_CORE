using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Holstentor.Models.HomeViewModels
{
    public class CategoryViewModel
    {
        public int ID { get; set; }
        public string TitleCategory { get; set; }
        public string Description { get; set; }
        public bool ActivePassive { get; set; }
        public string FontName { get; set; }
        public string Subtitle1 { get; set; }
        public string Subtitle2 { get; set; }
        public string Subtitle3 { get; set; }
        public string Link { get; set; }
    }
}