using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ManagementStudent.Api.Utilities.XFileService
{
    /// <summary>
    /// Супер класс сервиса управления файлами на сервере
    /// </summary>
    public abstract class XFileService
    {
        /// <summary>
        /// Окружение приложения
        /// </summary>
        private readonly IWebHostEnvironment _environment;

        /// <summary>
        /// Полный путь
        /// </summary>
        protected abstract string FolderPath { get; set; }

        /// <summary>
        /// Путь который записывается в базу
        /// </summary>
        protected abstract string AbstractPath { get; set; }

        /// <summary>
        /// Доступные для сохранения файлы
        /// </summary>
        protected abstract string[] SupportType { get; set; }

        /// <summary>
        /// Максимальный размер файла
        /// </summary>
        protected abstract long MaxSize { get; set; }

        /// <summary>
        /// Корневая папка
        /// </summary>
        protected string RootFolderPath { get; private set; }

        /// <summary>
        /// Абстрактная корневая папка
        /// </summary>
        protected string AbstractRootFolterPath { get; private set; } = "static";

        /// <summary>
        /// Констурктор супер класса
        /// </summary>
        /// <param name="environment">Окружение приложения</param>
        public XFileService(IWebHostEnvironment environment)
        {
            _environment = environment;
            RootFolderPath = Path.Combine(_environment.WebRootPath, AbstractRootFolterPath);
        }

        /// <summary>
        /// Загрузка на сервер
        /// </summary>
        /// <param name="file">Файл</param>
        /// <returns></returns>
        public virtual async Task<XFileInfoDTO> Upload(IFormFile file)
        {
            //if (!CheckFileExtention(file))
            //    throw new XFileException();

            if (!CheckFileSize(file))
            {
                var exception = new XFileException(file.Length, MaxSize);

                var baseException = new XFileException($"Размер файла {exception.FileSize} мб превышает {exception.MaxSize} мб.");

                throw baseException;
            }

            var path = Path.Combine(RootFolderPath, FolderPath);

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            var guid = Guid.NewGuid();

            var fileName = $"{guid}-{file.FileName}";

            var pathToSave = Path.Combine(path, fileName);

            using var fileStream = new FileStream(pathToSave, FileMode.Create);
            await file.CopyToAsync(fileStream);

            return new XFileInfoDTO
            {
                FileName = fileName,
                FilePath = Path.Combine(AbstractRootFolterPath, AbstractPath, fileName)
            };
        }

        /// <summary>
        /// Проверка на доступ к сохранению
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        private bool CheckFileExtention(IFormFile file)
        {
            var fileExtention = Path.GetExtension(file.FileName);

            return SupportType.Contains(fileExtention);
        }

        /// <summary>
        /// Проверить размер файла
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        private bool CheckFileSize(IFormFile file)
        {
            return file.Length <= MaxSize;
        }

        /// <summary>
        /// Удалить множество файлов
        /// </summary>
        /// <param name="filePaths">Пути, которые надо удалить</param>
        public void DeleteFileRange(IEnumerable<string> filePaths)
        {
            foreach (var filePath in filePaths)
            {
                DeleteFile(filePath);
            }
        }

        /// <summary>
        /// Удалить фотографию
        /// </summary>
        /// <param name="filePath">Путь, который храниться в базе, к файлу</param>
        public void DeleteFile(string filePath)
        {
            var file = new FileInfo(Path.Combine(_environment.WebRootPath, filePath));
            if (file.Exists)
            {
                file.Delete();
            }
        }
    }
}
