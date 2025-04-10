using System;
using System.Collections.Generic;
using IronPlus.Enums;

namespace IronPlus.Models
{
    public class AnalyticsRequest
    {
        public AnalyticsEventType EventType { get; set; }
        public string EventName { get; set; }
        public Dictionary<string, string> EventData { get; set; }
    }
}
