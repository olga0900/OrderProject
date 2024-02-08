using System.Diagnostics.CodeAnalysis;
using TestProject.Common.Entity.EntityInterface;

namespace TestProject.Repositories.Contracts
{
    public interface IBaseWriteRepository<in TEntity> where TEntity : class, IEntity
    {
        /// <summary>
        /// Добавить новую запись
        /// </summary>
        void Add([NotNull] TEntity entity);

        /// <summary>
        /// Изменить запись
        /// </summary>
        void Update([NotNull] TEntity entity);

        /// <summary>
        /// Удалить запись
        /// </summary>
        void Delete([NotNull] TEntity entity);
    }
}
