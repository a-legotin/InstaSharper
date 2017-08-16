using System;
using System.IO;
using System.Threading.Tasks;
using InstaSharper.Logger;

namespace InstaSharper.Tests.Utils
{
    internal class TestLogger : ILogger
    {
        private const string testLogFile = "tests.txt";
        public void Write(string logMessage)
        {
            File.AppendAllText(testLogFile, logMessage);
        }

        public async Task WriteAsync(string logMessage)
        {
            await Task.Run(()=> {
                File.AppendAllText(testLogFile, logMessage);
            });
        }

        public void WriteLine(string logMessage)
        {
            Write(logMessage + Environment.NewLine);
        }
    }
}