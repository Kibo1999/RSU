using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSU.JSonMessage
{
    public class Ivi
    {
        public Mandatory? mandatory { get; set; }
        public List<Optional>? optional { get; set; }
    }
}
