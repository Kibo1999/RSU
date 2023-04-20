using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSU.JSONIvim
{
    public class Mandatory
    {
        public ServiceProviderId? serviceProviderId { get; set; }
        public int iviIdentificationNumber { get; set; }
        public long? timeStamp { get; set; }
        public long? validFrom { get; set; }
        public long? validTo { get; set; }
        public int? connectedIviStructures { get; set; }
        public int? iviStatus { get; set; }
    }
}
