namespace ThesaurusWebAPI.IApiServices
{
    public interface ILoggerService
    {
        void Log(string message);
        void LogError(string message, Exception ex);
    }
}
