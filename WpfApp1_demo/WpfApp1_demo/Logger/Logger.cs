using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1_demo.Logger
{
    public enum LogLevel
    {
        Warning = 1,
        Error = 2,
        Info = 0,
        Debug = 3,
    }

    public abstract class LoggerBase
    {
        protected static string GetLevelMessage(LogLevel level)
        {
            return message[level];
        }

        private static Dictionary<LogLevel, string> message = new Dictionary<LogLevel, string>
        {
            { LogLevel.Debug, "Debug" },
            { LogLevel.Error, "ERROR" },
            { LogLevel.Info, "INFO" },
            { LogLevel.Warning, "Warning" },
        };

        protected static string FolderRootPath
        {
            get { return AppDomain.CurrentDomain.BaseDirectory; }
        }
    }

    public sealed class Logger : LoggerBase
    {
        private static readonly string mPath = string.Format("{0}\\tool.log", FolderRootPath);

        static object locker = new object();

        private LogLevel logLevel;

        private Logger()
        {
            try
            {
                this.logLevel = LogLevel.Info;
                this.logLevel = (LogLevel)Enum.Parse(typeof(LogLevel), ConfigurationManager.AppSettings.Get("LogLevel"));
            }
            catch (Exception ex)
            {
                this.logLevel = LogLevel.Info;
                using (StreamWriter writer = new StreamWriter(mPath, true))
                {
                    writer.WriteLine("{2}-{0}:{1} ", GetLevelMessage(LogLevel.Warning), ex.Message, DateTime.Now.ToString("G", DateTimeFormatInfo.InvariantInfo));
                    writer.Flush();
                }
            }
        }

        private static Logger mInstance;
        [SuppressMessage("FxCopCustomRules", "C100007:SpellCheckStringValues", Justification = "Normal behavior")]
        public static Logger CreateInstance()
        {
            try
            {
                lock (locker)
                {
                    string path = string.Format("{0}\\tool.log", AppDomain.CurrentDomain.BaseDirectory);
                    FileInfo fInfo = new FileInfo(path);
                    if (!fInfo.Exists)
                    {
                        File.Create(path);
                    }
                    if (fInfo.Length > 10L * 1024 * 1024)
                    {
                        File.Move(path, string.Format("{0}\\tool[{1}].log", AppDomain.CurrentDomain.BaseDirectory, DateTime.Now.ToString("yyyyMMddHHmmss")));
                    }
                }
            }
            catch (Exception ex)
            {
                Trace.TraceWarning(ex.ToString());
            }

            if (mInstance == null)
            {
                mInstance = new Logger();
            }
            return mInstance;
        }

        public void Log(LogLevel level, string message, params object[] paras)
        {
            lock (locker)
            {
                if (level == LogLevel.Debug && this.logLevel != LogLevel.Debug)
                {
                    return;
                }
                if (paras.Length > 0)
                {
                    message = string.Format(message, paras);
                }
                using (StreamWriter writer = new StreamWriter(mPath, true))
                {
                    writer.WriteLine("{2}-{0}:{1} ", GetLevelMessage(level), message, DateTime.Now.ToString("G", DateTimeFormatInfo.InvariantInfo));
                    writer.Flush();
                }
            }
        }

        [SuppressMessage("FxCopCustomRules", "C100007:SpellCheckStringValues", Justification = "Normal behavior")]
        public static void ChangeLogFile()
        {
            try
            {
                string path = string.Format("{0}\\tool.log", FolderRootPath);
                FileInfo fInfo = new FileInfo(path);
                File.Move(path, string.Format("{0}\\tool[{1}].log", FolderRootPath, DateTime.Now.ToString("yyyyMMddHHmmss")));
            }
            catch (Exception ex)
            {
                Trace.TraceWarning(ex.ToString());
            }
        }

        public void Info(string message, params object[] paras)
        {
            this.Log(LogLevel.Info, message, paras);
        }

        public void Error(string message, params object[] paras)
        {
            this.Log(LogLevel.Error, message, paras);
        }

        public void Warn(string message, params object[] paras)
        {
            this.Log(LogLevel.Warning, message, paras);
        }
    }
}
