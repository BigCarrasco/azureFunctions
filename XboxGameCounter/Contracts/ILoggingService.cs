namespace Company.Function.Contracts;
    public interface ILoggingService
    {
        Task LogAsync(string message);
    }
