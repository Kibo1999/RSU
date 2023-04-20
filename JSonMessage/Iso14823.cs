using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSU.JSONIvim
{
    public class Iso14823
    {
        public PictogramCode pictogramCode { get; set; }
        public List<Attribute> attributes { get; set; }
        public object itisCodes { get; set; }
    }
}
