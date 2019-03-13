using NLog;
using NLog.Layouts;
using NLog.Targets;
using System;

namespace Infrastructure
{
    public class LogHelper
    {
        private static readonly Logger log = LogManager.GetLogger("UserLog");
        private static string assName = AppDomain.CurrentDomain.FriendlyName;

        private static string FormatMsg(string title, object msg)
        {
            return "Assembly：" + assName + "\r\nTitle : " + title + "\r\nMessage : " + msg + "\r\n";
        }

        public static void Error(string title, object msg = null)
        {
            log?.Error(FormatMsg(title, msg));
        }

        public static void Debug(string title, object msg = null)
        {
            log?.Debug(FormatMsg(title, msg));
        }

        public static void Info(string title, object msg = null)
        {
            log?.Info(FormatMsg(title, msg));
        }

        public static void Warn(string title, object msg = null)
        {
            log?.Warn(FormatMsg(title, msg));
        }

        public static void Trace(string title, object msg = null)
        {
            log?.Trace(FormatMsg(title, msg));
            Console.WriteLine(FormatMsg(title, msg));
        }

        public static void Fatal(string title, object msg = null)
        {
            log?.Fatal(FormatMsg(title, msg));
        }
    }

    [Layout("Log4JXmlEventLayoutEx")]
    public class Log4JXmlEventLayoutEx : Log4JXmlEventLayout
    {
        protected override string GetFormattedMessage(LogEventInfo logEvent)
        {
            string msg = logEvent.Message + " ${exception:format=Message,Type,ToString,StackTrace}";
            msg = SimpleLayout.Evaluate(msg, logEvent);
            LogEventInfo updatedInfo;
            if (msg == logEvent.Message)
            {
                updatedInfo = logEvent;
            }
            else
            {
                updatedInfo = new LogEventInfo(
                    logEvent.Level, logEvent.LoggerName,
                    logEvent.FormatProvider, msg,
                    logEvent.Parameters, logEvent.Exception);
            }

            return base.GetFormattedMessage(updatedInfo);
        }
    }

    [Target("NLogViewerEx")]
    public class NLogViewerTargetEx : NLogViewerTarget
    {
        private readonly Log4JXmlEventLayoutEx layout = new Log4JXmlEventLayoutEx();

        public override Layout Layout
        {
            get { return layout; }
            set { }
        }
    }
}
