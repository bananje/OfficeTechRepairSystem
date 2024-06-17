using System;

namespace ManagementStudent.Api.Utilities.XFileService
{
    /// <summary>
    /// Исключение для файлового сервиса
    /// </summary>
    public class XFileException : Exception
    {
        /// <summary>
        /// Размер входящего файла
        /// </summary>
        public int FileSize { get; set; }

        /// <summary>
        /// Максимальный размер
        /// </summary>
        public int MaxSize { get; set; }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="message">Сообщение</param>
        public XFileException(string message) : base(message) { }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="fileSize">Размер входящего файла</param>
        /// <param name="maxSize">Максимальный размер</param>
        public XFileException(long fileSize, long maxSize)
        {
            FileSize = (int)(fileSize / 1_048_576);
            MaxSize = (int)(maxSize / 1_048_576);
        }
    }
}
