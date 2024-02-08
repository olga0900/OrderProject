using AutoMapper;
using TestProject.API.Mappers;
using TestProject.API.Tests.Infrastructures;
using TestProject.Common.Entity.InterfaceDB;
using TestProject.Context.Contracts;
using TestProject.Services.Mappers;
using Xunit;

namespace TestProject.API.Tests
{
    /// <summary>
    /// Базовый класс для тестов
    /// </summary>
    [Collection(nameof(TestProjectApiTestCollection))]
    public class BaseIntegrationTest
    {
        protected readonly CustomWebApplicationFactory factory;
        protected readonly ITestProjectContext context;
        protected readonly IUnitOfWork unitOfWork;
        protected readonly IMapper mapper;

        public BaseIntegrationTest(TestProjectApiFixture fixture)
        {
            factory = fixture.Factory;
            context = fixture.Context;
            unitOfWork = fixture.UnitOfWork;

            Profile[] profiles = { new APIMapper(), new ServiceMapper() };

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfiles(profiles);
            });

            mapper = config.CreateMapper();
        }
    }
}
