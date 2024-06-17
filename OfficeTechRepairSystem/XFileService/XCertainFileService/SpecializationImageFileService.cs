using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace ManagementStudent.Api.Utilities.XFileService.XCertainFileService
{
    /// <summary>
    /// Сервис для загрузки фотографий
    /// </summary>
    public class SpecializationImageFileService : XFileService
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="environment">Окружение приложения</param>
        public SpecializationImageFileService(IWebHostEnvironment environment) : base(environment)
        {
            AbstractPath = Path.Combine("SpecializationImageStorage");
            FolderPath = Path.Combine(RootFolderPath, AbstractPath);
        }

        /// <summary>
        /// Путь хранения фотографий
        /// </summary>
        protected override string FolderPath { get; set; }

        /// <summary>
        /// Доступные расширения
        /// </summary>
        protected override string[] SupportType { get; set; } = new string[] { ".jpg", ".png" }; //Добавить ".jpeg"

        /// <summary>
        /// Максимальный размер изображения
        /// </summary>
        protected override long MaxSize { get; set; } = 5_242_880;

        /// <summary>
        /// Путь для обращения к фотографии через браузер
        /// </summary>
        protected override string AbstractPath { get; set; }
    }
}
