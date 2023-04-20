using PerEncDec.IVI.GDD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSU.JSONIvim
{
    public class EDT
    {
        public Year year { get; set; }
        public MonthDay month_day { get; set; }
        public object repeatingPeriodDayTypes { get; set; }
        public HourMinutes hourMinutes { get; set; }
        public object dateRangeOfWeek { get; set; }
        public DurationHourMinute durationHourMinute { get; set; }
    }
}
