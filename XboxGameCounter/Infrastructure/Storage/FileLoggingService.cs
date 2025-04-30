using Company.Function.Contracts;
using System.IO;

namespace Company.Function.Infrastructure.Storage
{
    public class FileLoggingService : ILoggingService
    {
        private readonly string _logDirectory;
        private readonly string _logFilePath;

        public FileLoggingService()
        {
            _logDirectory = GetLogDirectory();
            _logFilePath = Path.Combine(_logDirectory, "gamecounter.log");

            EnsureLogDirectoryExists();
        }

        public async Task LogAsync(string message)
        {
            var logLine = FormatLogMessage(message);
            await File.AppendAllTextAsync(_logFilePath, logLine + Environment.NewLine);
        }

        private static string GetLogDirectory()
        {
            var projectRootPath = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"..\.."));
            return Path.Combine(projectRootPath, "Logs");
        }

        private void EnsureLogDirectoryExists()
        {
            if (!Directory.Exists(_logDirectory))
            {
                Directory.CreateDirectory(_logDirectory);
            }
        }

        private static string FormatLogMessage(string message)
        {
            return $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {message}";
        }
    }
}