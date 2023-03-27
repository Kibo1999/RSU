using PerEncDec.IVI.ITSContainer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSU.JSonMessage
{
    public class VED
    {
        public VehicleHeight? vehicleHeight { get; set; }
        public VehicleWidth? vehicleWidth { get; set; }
        public VehicleLength? vehicleLength { get; set; }
        public VehicleWeight? vehicleWeight { get; set; }

    }

}
