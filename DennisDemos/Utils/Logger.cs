using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DennisDemos.Utils
{
    public class Logger
    {
        private static readonly Logger Instance = new Logger();
        private static TraceSource _ts;

        private Logger()
        {
            _ts = new TraceSource("Trace");
        }

        public static Logger GetInstance()
        {
            return Instance;
        }

        public void LogInformation(string information)
        {
            information = $"[{DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss")}] {information}";
            _ts.TraceInformation(information);
        }

        public void LogError(string error)
        {
            error = $"[{DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss")}] {error}";
            _ts.TraceEvent(TraceEventType.Error, 1, error);
        }
    }
}
