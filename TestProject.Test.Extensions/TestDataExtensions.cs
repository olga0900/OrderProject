using TestProject.Context.Contracts.Models;

namespace TestProject.Test.Extensions
{
    public static class TestDataExtensions
    {
        public static void BaseAuditSetParamtrs<TEntity>(this TEntity entity) where TEntity : BaseAuditEntity
        {
            entity.Number = Guid.NewGuid();
            entity.CreatedAt = DateTimeOffset.UtcNow;
            entity.CreatedBy = $"CreatedBy{Guid.NewGuid():N}";
            entity.UpdatedAt = DateTimeOffset.UtcNow;
            entity.UpdatedBy = $"UpdatedBy{Guid.NewGuid():N}";
        }
    }
}
