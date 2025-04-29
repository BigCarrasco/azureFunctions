using Company.Function.Contracts;
using System.IO;

namespace Company.Function.Infrastructure.Storage
{
    public class FileLoggingService : ILoggingService
    {
        public async Task LogAsync(string message)
        {
            // subimos 4 carpetas desde /bin/Debug/net8.0
            var projectRootPath = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"..\.."));

            // Crear si no existe
            var logDirectory = Path.Combine(projectRootPath, "Logs");
            if (!Directory.Exists(logDirectory))
            {
                Directory.CreateDirectory(logDirectory);
            }

            // Define archivo de log
            var logFilePath = Path.Combine(logDirectory, "gamecounter.log");

            // Construir la línea de log
            var logLine = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {message}";

            // Guardar en el archivo
            await File.AppendAllTextAsync(logFilePath, logLine + Environment.NewLine);
        }
    }
}
