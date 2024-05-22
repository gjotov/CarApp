using System;
using System.IO;

namespace LoggerLibrary
{
    /// <summary>
    /// Класс для записи логов в файлы.
    /// </summary>
    public static class Logger
    {
        private static readonly string logDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs");

        static Logger()
        {
            if (!Directory.Exists(logDirectory))
            {
                Directory.CreateDirectory(logDirectory);
            }
        }

        /// <summary>
        /// Записывает сообщение в лог-файл.
        /// </summary>
        /// <param name="message">Сообщение для записи в лог.</param>
        public static void Log(string message)
        {
            string logFilePath = Path.Combine(logDirectory, $"{DateTime.Now:yyyy-MM-dd}.log");
            string logMessage = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {message}";
            File.AppendAllText(logFilePath, logMessage + Environment.NewLine);
        }

        /// <summary>
        /// Записывает сообщение об ошибке в лог-файл.
        /// </summary>
        /// <param name="message">Сообщение об ошибке для записи в лог.</param>
        public static void LogError(string message)
        {
            Log($"ОШИБКА: {message}");
        }

        /// <summary>
        /// Записывает информационное сообщение в лог-файл.
        /// </summary>
        /// <param name="message">Информационное сообщение для записи в лог.</param>
        public static void LogInfo(string message)
        {
            Log($"ИНФО: {message}");
        }

        /// <summary>
        /// Записывает предупреждающее сообщение в лог-файл.
        /// </summary>
        /// <param name="message">Предупреждающее сообщение для записи в лог.</param>
        public static void LogWarning(string message)
        {
            Log($"ПРЕДУПРЕЖДЕНИЕ: {message}");
        }
    }
}
