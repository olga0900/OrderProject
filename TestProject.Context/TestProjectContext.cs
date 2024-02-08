using Microsoft.EntityFrameworkCore;
using TestProject.Common.Entity.InterfaceDB;
using TestProject.Context.Configuration;
using TestProject.Context.Contracts;
using TestProject.Context.Contracts.Models;

namespace TestProject.Context
{
    /// <summary>
    /// Контекст работы с БД
    /// </summary>
    /// <remarks>
    /// 1) dotnet tool install --global dotnet-ef --version 6.0.0
    /// 2) dotnet tool update --global dotnet-ef
    /// 3) dotnet ef migrations add [name] --project TestProject.Context\TestProject.Context.csproj
    /// 4) dotnet ef database update --project TestProject.Context\TestProject.Context.csproj
    /// 5) dotnet ef database update [targetMigrationName] --TestProject.Context\TestProject.Context.csproj
    /// </remarks>
    public class TestProjectContext :
        DbContext,
        ITestProjectContext,
        IDbRead,
        IDbWriter,
        IUnitOfWork
    {
        public DbSet<Order> Orders { get; set; }

        public TestProjectContext(DbContextOptions<TestProjectContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(OrderEntityTypeConfiguration).Assembly);
        }

        /// <summary>
        /// Сохранение изменений в БД
        /// </summary>
        async Task<int> IUnitOfWork.SaveChangesAsync(CancellationToken cancellationToken)
        {
            var count = await base.SaveChangesAsync(cancellationToken);
            foreach (var entry in base.ChangeTracker.Entries().ToArray())
            {
                entry.State = EntityState.Detached;
            }
            return count;
        }

        /// <summary>
        /// Чтение сущностей из БД
        /// </summary>
        IQueryable<TEntity> IDbRead.Read<TEntity>()
            => base.Set<TEntity>()
                .AsNoTracking()
                .AsQueryable();

        /// <summary>
        /// Запись сущности в БД
        /// </summary>
        void IDbWriter.Add<TEntity>(TEntity entity)
            => base.Entry(entity).State = EntityState.Added;

        /// <summary>
        /// Обновление сущностей
        /// </summary>
        void IDbWriter.Update<TEntity>(TEntity entity)
            => base.Entry(entity).State = EntityState.Modified;

        /// <summary>
        /// Удаление сущности из БД
        /// </summary>
        void IDbWriter.Delete<TEntity>(TEntity entity)
            => base.Entry(entity).State = EntityState.Deleted;
    }
}
