using System;
using System.Collections.Generic;
using IronPlus.Enums;
using IronPlus.Models;
using Sentry;

namespace IronPlus.Services
{
    public static class AnalyticsService
    {
        public static void Track_App_Page(AnalyticsRequest request)
        {
            SentrySdk.Metrics.EmitCounter("app.page_view", 1,
                new Dictionary<string, object> { ["page"] = request.EventName });
        }

        public static void Track_App_Page(string pageNameToTrack, string pageParameterName = "", string pageParameterValue = "")
        {
            var attrs = new Dictionary<string, object> { ["page"] = pageNameToTrack };

            if (!string.IsNullOrEmpty(pageParameterName))
                attrs[pageParameterName] = pageParameterValue;

            SentrySdk.Metrics.EmitCounter("app.page_view", 1, attrs);
        }

        public static void Track_App_Event(AnalyticsRequest request)
        {
            if (request.EventData == null)
                request.EventData = new Dictionary<string, string>();

            request.EventData.Add("Category", request.EventType.ToString());

            var eventName = request.EventName;
            var eventType = request.EventType.ToString();
            var data = request.EventData;

            SentrySdk.Logger.LogInfo(log =>
            {
                log.SetAttribute("event.type", eventType);
                foreach (var kvp in data)
                    log.SetAttribute(kvp.Key, kvp.Value);
            }, "{0}", eventName);
        }

        public static void Track_App_Event(AnalyticsEventType EventType, string EventToTrack, string EventParameter = "", string EventValue = "")
        {
            var request = new AnalyticsRequest();
            request.EventType = EventType;
            request.EventName = EventToTrack;

            if (!string.IsNullOrEmpty(EventParameter))
            {
                request.EventData = new Dictionary<string, string> {
                { EventParameter, EventValue }};
            }

            Track_App_Event(request);
        }

        public static void Track_App_Exception(string ExceptionMessage, object caller = null, Dictionary<string, string> data = null)
        {
            Track_App_Exception(new Exception(ExceptionMessage), caller, data);
        }

        public static void Track_App_Exception(Exception exception, object caller = null, Dictionary<string, string> data = null)
        {
            SentrySdk.CaptureException(exception, scope =>
            {
                if (caller != null)
                    scope.SetTag("Caller", caller.GetType().Name);

                if (data != null)
                    foreach (var kvp in data)
                        scope.SetExtra(kvp.Key, kvp.Value);
            });
        }
    }
}
