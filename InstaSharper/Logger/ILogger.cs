using System.Threading.Tasks;

namespace InstaSharper.Logger
{
    public interface ILogger
    {
        void Write(string logMessage);
        Task WriteAsync(string logMessage);
        void WriteLine(string logMessage);
    }
}