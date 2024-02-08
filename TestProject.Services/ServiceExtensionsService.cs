using Microsoft.Extensions.DependencyInjection;
using TestProject.General;
using TestProject.Services.Anchors;
using TestProject.Services.Contracts;
using TestProject.Services.Services;

namespace TestProject.Services
{
    /// <summary>
    /// Расширения для <see cref="IServiceCollection"/>
    /// </summary>
    public static class ServiceExtensionsService
    {
        /// <summary>
        /// Регистрация всех сервисов и валидатора
        /// </summary>
        public static void RegistrationService(this IServiceCollection service)
        {
            service.RegistrationOnInterface<IServiceAnchor>(ServiceLifetime.Scoped);
            service.AddTransient<IServiceValidator, ServicesValidator>();
        }
    }
}
