using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSU.JSonMessage
{
    public class Code
    {
        public ViennaConvention? viennaConvention { get; set; }
        public Iso14823? iso14823 { get; set; }

        public int? itisCode { get; set; }
    }
}
