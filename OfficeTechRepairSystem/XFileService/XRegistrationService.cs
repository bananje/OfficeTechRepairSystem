using ManagementStudent.Api.Utilities.XFileService.XCertainFileService;
using Microsoft.Extensions.DependencyInjection;

namespace ManagementStudent.Api.Utilities.XFileService
{
    public static class XRegistrationService
    {
        /// <summary>
        /// Регистрация файловых сервисов
        /// </summary>
        public static void AddFileService(this IServiceCollection services)
        {
            services.AddSingleton<SpecializationImageFileService>();
        }
    }
}
