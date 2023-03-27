using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSU.JSonMessage
{
    public class GicPart
    {
        public List<DetectionZoneId>? detectionZoneIds { get; set; }
        public List<RelevanceZoneId>? relevanceZoneIds { get; set; }
        public object? direction { get; set; }
        public object? minimumAwarenessTime { get; set; }
        public int? iviType { get; set; }
        public object? iviPurpose { get; set; }
        public object? laneStatus { get; set; }
        public object? driverCharacteristics { get; set; }
        public object? layoutId { get; set; }
        public object? preStoredlayoutId { get; set; }
        public List<RoadSignCode>? roadSignCodes { get; set; }
        public List<object>? extraText { get; set; }
    }
}
