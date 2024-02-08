using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using TestProject.Common.Entity.InterfaceDB;
using TestProject.Context.Contracts;

namespace TestProject.Context
{
    /// <summary>
    /// Методы пасширения для <see cref="IServiceCollection"/>
    /// </summary>
    public static class ServiceExtensionsContext
    {
        /// <summary>
        /// Регистрирует все что связано с контекстом
        /// </summary>
        /// <param name="service"></param>
        public static void RegistrationContext(this IServiceCollection service)
        {
            service.TryAddScoped<ITestProjectContext>(provider => provider.GetRequiredService<TestProjectContext>());
            service.TryAddScoped<IDbRead>(provider => provider.GetRequiredService<TestProjectContext>());
            service.TryAddScoped<IDbWriter>(provider => provider.GetRequiredService<TestProjectContext>());
            service.TryAddScoped<IUnitOfWork>(provider => provider.GetRequiredService<TestProjectContext>());
        }
    }
}
