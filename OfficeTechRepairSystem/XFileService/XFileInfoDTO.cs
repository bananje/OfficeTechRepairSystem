namespace ManagementStudent.Api.Utilities.XFileService
{
    /// <summary>
    /// Информация о файле на сервере
    /// </summary>
    public class XFileInfoDTO
    {
        /// <summary>
        /// Наименование файла
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// Путь к файлу на сервере
        /// </summary>
        public string FilePath { get; set; }
    }
}
