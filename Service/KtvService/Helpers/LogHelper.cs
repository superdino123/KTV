using NLog;
using NLog.Config;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helpers
{
    class Logger
    {
        private static ILogger _log;

        public static ILogger Log
        {
            get
            {
                if (_log == null)
                    _log = LogManager.GetCurrentClassLogger();
                return _log;
            }
        }
    }

    public class LogHelper
    {
        public static void LogInfo(String message, bool isRecordLog = true)
        {
            if (isRecordLog)
                Logger.Log.Info(message);
        }

        public static void LogError(String message, Exception ex, bool isRecordLog = true)
        {
            if (isRecordLog)
            {
                Logger.Log.Error(ex, message);
            }

        }

        public static void LogError(String message, bool isRecordLog = true)
        {
            if (isRecordLog)
                Logger.Log.Error(message);
        }

        public static void LogWarn(String message, bool isRecordLog = true)
        {
            if (isRecordLog)
                Logger.Log.Warn(message);
        }

        public static void LogWarn(String message, Exception ex, bool isRecordLog = true)
        {
            if (isRecordLog)
            {
                Logger.Log.Warn(ex, message);
            }

        }

        public static void LogTrace(string message)
        {
            Logger.Log.Trace(message);
        }

        public static void LogTrace(long time, string message)
        {
            if (time > 1000)
            {
                Logger.Log.Trace(message + ",花费时间:" + time);
            }
        }

        public static void SetConfig(FileInfo configFile)
        {
            NLog.LogManager.Configuration = new XmlLoggingConfiguration(configFile.FullName);
        }
    }
}
