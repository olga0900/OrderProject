using Microsoft.Extensions.DependencyInjection;
using TestProject.General;
using TestProject.Repositories.Anchors;

namespace TestProject.Repositories
{
    public static class ServiceExtensionsRepository
    {
        // <summary>
        /// Регистрирует маркерный интерфейс репозитория
        /// </summary>
        public static void RegistrationRepository(this IServiceCollection service)
        {
            service.RegistrationOnInterface<IRepositoryAnchor>(ServiceLifetime.Scoped);
        }
    }
}
