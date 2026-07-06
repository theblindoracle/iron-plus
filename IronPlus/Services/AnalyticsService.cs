using System;
using System.Collections.Generic;
using IronPlus.Enums;
using IronPlus.Models;
using Sentry;

namespace IronPlus.Services
{
    public static class AnalyticsService
    {
        public static bool Initialized { get; private set; }

        public static void Init(string key)
        {
            // Sentry is initialized via UseSentry() in MauiProgram.cs — nothing to do here.
            Initialized = true;
        }

        public static string InstallID => SentrySdk.LastEventId.ToString();

        public static void Track_App_Page(AnalyticsRequest request)
        {
            request.EventType = AnalyticsEventType.PageView;
            Track_App_Event(request);
        }

        public static void Track_App_Page(string PageNameToTrack, string PageParameterName = "", string PageParameterValue = "")
        {
            var request = new AnalyticsRequest();
            request.EventType = AnalyticsEventType.PageView;
            request.EventName = PageNameToTrack;

            if (!string.IsNullOrEmpty(PageParameterName))
            {
                request.EventData = new Dictionary<string, string> {
                { PageParameterName, PageParameterValue }};
            }

            Track_App_Event(request);
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
