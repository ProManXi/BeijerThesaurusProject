using ThesaurusWebAPI.IApiServices;

namespace ThesaurusWebAPI.ApiServices
{
    public class LoggerService : ILoggerService
    {
        private readonly string _logFilePath;
        private readonly object _lock = new object();

        public LoggerService()
        {
            Directory.CreateDirectory("Logs");
            _logFilePath = Path.Combine("Logs", "app-log.txt");
        }

        public void Log(string message)
        {
            lock (_lock)
            {
                string logEntry = $"[{DateTime.Now:dd-MM-yyyy HH:mm:ss}] {message}";
                File.AppendAllText(_logFilePath, logEntry + Environment.NewLine);
            }
        }

        public void LogError(string message, Exception ex)
        {
            lock (_lock)
            {
                string logEntry = $"[{DateTime.Now:dd-MM-yyyy HH:mm:ss}] ERROR: {message}\nException: {ex.Message}\nStackTrace: {ex.StackTrace}";
                File.AppendAllText(_logFilePath, logEntry + Environment.NewLine);
            }
        }
    }
}
