using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TestProject.Common.Entity.InterfaceDB;
using TestProject.Context;
using TestProject.Context.Contracts;
using Xunit;

namespace TestProject.API.Tests.Infrastructures
{
    public class TestProjectApiFixture : IAsyncLifetime
    {
        private readonly CustomWebApplicationFactory factory;
        private TestProjectContext? testProjectContext;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="TestProjectApiFixture"/>
        /// </summary>
        public TestProjectApiFixture()
        {
            factory = new CustomWebApplicationFactory();
        }

        Task IAsyncLifetime.InitializeAsync() => TicketSellingContext.Database.MigrateAsync();

        async Task IAsyncLifetime.DisposeAsync()
        {
            await TicketSellingContext.Database.EnsureDeletedAsync();
            await TicketSellingContext.Database.CloseConnectionAsync();
            await TicketSellingContext.DisposeAsync();
            await factory.DisposeAsync();
        }

        public CustomWebApplicationFactory Factory => factory;

        public ITestProjectContext Context => TicketSellingContext;

        public IUnitOfWork UnitOfWork => TicketSellingContext;

        internal TestProjectContext TicketSellingContext
        {
            get
            {
                if (testProjectContext != null)
                {
                    return testProjectContext;
                }

                var scope = factory.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
                testProjectContext = scope.ServiceProvider.GetRequiredService<TestProjectContext>();
                return testProjectContext;
            }
        }
    }
}
