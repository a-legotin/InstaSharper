using System;
using System.IO;
using System.Threading.Tasks;
using InstaSharper.Logger;

namespace InstaSharper.Examples
{
    internal class DebugFileLogger : ILogger
    {
        private const string logFile = "log.txt";

        public void Write(string logMessage)
        {
            File.AppendAllText(logFile, logMessage);
        }

        public async Task WriteAsync(string logMessage)
        {
            await Task.Run(() => File.AppendAllText(logFile, logMessage));
        }

        public void WriteLine(string logMessage)
        {
            Write(logMessage + Environment.NewLine);
        }
    }
}
