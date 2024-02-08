using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using TestProject.Common.Entity.EntityInterface;

namespace TestProject.Common.Entity.Repositories
{
    /// <summary>
    /// Вспомогательные методы расширения
    /// </summary>
    public static class CommonSpecs
    {
        /// <summary>
        /// По идентификатору 
        /// </summary>
        public static IQueryable<TEntity> ById<TEntity>(this IQueryable<TEntity> query, Guid number) where TEntity : class, IEntityWithNumber
            => query.Where(x => x.Number == number);

        /// <summary>
        /// По идентификаторам
        /// </summary>
        public static IQueryable<TEntity> ByIds<TEntity>(this IQueryable<TEntity> query, IEnumerable<Guid> numbers) where TEntity : class, IEntityWithNumber
        {
            var cnt = numbers.Count();
            switch (cnt)
            {
                case 0:
                    return query.Where(x => false);
                case 1:
                    return query.ById(numbers.First());
                default:
                    return query.Where(x => numbers.Contains(x.Number));
            }
        }

        /// <summary>
        /// Не удаленные записи
        /// </summary>
        public static IQueryable<TEntity> NotDeletedAt<TEntity>(this IQueryable<TEntity> query) where TEntity : class, IEntityAuditDeleted
            => query.Where(x => x.DeletedAt == null);

        /// <summary>
        /// Возвращает <see cref="IReadOnlyCollection{TEntity}"/>
        /// </summary>
        public static Task<IReadOnlyCollection<TEntity>> ToReadOnlyCollectionAsync<TEntity>(this IQueryable<TEntity> query,
            CancellationToken cancellationToken)
            => query.ToListAsync(cancellationToken)
                .ContinueWith(x => new ReadOnlyCollection<TEntity>(x.Result) as IReadOnlyCollection<TEntity>,
                    cancellationToken);
    }
}
