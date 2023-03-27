using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSU.JSonMessage
{
    public class GlcPart
    {
        public int zoneId { get; set; }
        public object? laneNumber { get; set; }
        public object? zoneExtension { get; set; }
        public object? zoneHeading { get; set; }
        public Zone? zone { get; set; }
    }
}
