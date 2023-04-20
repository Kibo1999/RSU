using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSU.JSONIvim
{
    public class TcPart
    {
        public List<DetectionZone>? DetectionZone { get; set; }
        public List<RelevanceZone>? RelevanceZone { get; set; }
        public object? Direction { get; set; }
        public object? minimumAwarenessZoneIds { get; set; }
        public object? layoutId { get; set; }
        public object? preStoredlayoutId { get; set; }
        public List<object>? text { get; set; }
        public DateTime? data { get; set; }
    }
}
