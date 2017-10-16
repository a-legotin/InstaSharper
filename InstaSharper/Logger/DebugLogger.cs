using System;
using System.Threading.Tasks;

namespace InstaSharper.Logger
{
    internal class DebugLogger : ILogger
    {
        public void Write(string logMessage)
        {
            Console.Write(logMessage);
        }

        public async Task WriteAsync(string logMessage)
        {
            await Task.Run(() => Console.WriteLine(logMessage));
        }

        public void WriteLine(string logMessage)
        {
            Write(logMessage + Environment.NewLine);
        }
    }
}