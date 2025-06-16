using System;
using System.Collections.Generic;
using IronPlus.Enums;
using IronPlus.Models;
// using Microsoft.AppCenter;
// using Microsoft.AppCenter.Analytics;
// using Microsoft.AppCenter.Crashes;

namespace IronPlus.Services
{
    public static class AppCenterService
    {
        public static bool Initialized { get; private set; }

        public static void Init(string key)
        {

            if (Initialized)
            {
                return;
            }


            // if (!AppCenter.Configured)
            // {
                //#if DEBUG
                //                //set APPCENTER SDK  log level
                //                Microsoft.AppCenter.AppCenter.LogLevel = LogLevel.Verbose;
                //#endif
                //crashes are processed as soon as you call Start , 
                //this call also changes AppCenter.Configured to true
                // AppCenter.Start(key, typeof(Analytics), typeof(Crashes));
            // }

            Initialized = true;

        }

        private static void CheckInitialized()
        {
            if (!Initialized)
            {
                throw new Exception("Analytics Service not Initialized");
            }

        }

        public static string InstallID => ""; //AppCenter.GetInstallIdAsync().ToString();


        /// <summary>
        /// Track a Page View
        /// </summary>
        /// <param name="request"></param>
        public static void Track_App_Page(AnalyticsRequest request)
        {
            request.EventType = AnalyticsEventType.PageView;
            Track_App_Event(request);

        }


        /// <summary>
        /// Track a Page View
        /// </summary>
        /// <param name="PageNameToTrack"></param>
        /// <param name="PageParameterName"></param>
        /// <param name="PageParameterValue"></param>
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



        /// <summary>
        /// Track an Event
        /// </summary>
        /// <param name="request"></param>
        public static void Track_App_Event(AnalyticsRequest request)
        {
            if (request.EventData == null)
                request.EventData = new Dictionary<string, string>();

            request.EventData.Add("Category", request.EventType.ToString());


            // CheckInitialized();

            // Analytics.TrackEvent(request.EventName, request.EventData);
        }


        /// <summary>
        /// Track an Event
        /// </summary>
        /// <param name="EventType"></param>
        /// <param name="EventToTrack"></param>
        /// <param name="EventParameter"></param>
        /// <param name="EventValue"></param>
        public static void Track_App_Event(AnalyticsEventType EventType, string EventToTrack, string EventParameter = "", string EventValue = "")
        {

            var request = new AnalyticsRequest();

            request.EventType = EventType;
            request.EventName = EventToTrack;

            // Add Event
            if (!string.IsNullOrEmpty(EventParameter))
            {
                request.EventData = new Dictionary<string, string> {
                { EventParameter, EventValue }};
            }

            Track_App_Event(request);

        }


        /// <summary>
        /// Log exception to Analytics
        /// </summary>
        /// <param name="ExceptionMessage"></param>
        /// <param name="callerName"></param>
        public static void Track_App_Exception(string ExceptionMessage, object caller = null, Dictionary<string, string> data = null)
        {
            Track_App_Exception(new Exception(ExceptionMessage), caller, data);
        }

        /// <summary>
        /// Log Exception to Analytics
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="caller"></param>
        /// <param name="data"></param>
        public static void Track_App_Exception(Exception exception, object caller, Dictionary<string, string> data = null)
        {
            var request = new AnalyticsRequest();
            request.EventType = AnalyticsEventType.Exception;
            request.EventName = exception.Message;

            if (data != null)
                request.EventData = data;

            if (caller != null)
            {
                if (request.EventData == null)
                    request.EventData = new Dictionary<string, string>();

                request.EventData.Add("Caller", caller.GetType().Name);
            }

            // Don't write to analytics. Just Exceptions
            // Track_App_Event(request);

            CheckInitialized(); // Not really needed since above line would throw exception anyway

            // Crashes.TrackError(exception, request.EventData);

        }


    }
}

