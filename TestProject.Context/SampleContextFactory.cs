using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace TestProject.Context
{
    public class SampleContextFactory : IDesignTimeDbContextFactory<TestProjectContext>
    {
        public TestProjectContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            var options = new DbContextOptionsBuilder<TestProjectContext>()
                .UseSqlServer(connectionString)
                .Options;

            return new TestProjectContext(options);
        }
    }
}
