using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSU.JSONIvim
{
    public class PictogramCode
    {
        public object countryCode { get; set; }
        public ServiceCategoryCode? serviceCategoryCode { get; set; }
        public PictogramCategoryCode? pictogramCategoryCode { get; set; }
    }
}
