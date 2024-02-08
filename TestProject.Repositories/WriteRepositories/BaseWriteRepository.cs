using System.Diagnostics.CodeAnalysis;
using TestProject.Common.Entity.EntityInterface;
using TestProject.Common.Entity.InterfaceDB;
using TestProject.Repositories.Contracts;

namespace TestProject.Repositories.WriteRepository
{
    public abstract class BaseWriteRepository<TEntity> : IBaseWriteRepository<TEntity> where TEntity : class, IEntity
    {
        /// <inheritdoc cref="IDbWriterContext"/>
        private readonly IDbWriterContext writerContext;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="BaseWriteRepository{TEntity}"/>
        /// </summary>
        protected BaseWriteRepository(IDbWriterContext writerContext)
        {
            this.writerContext = writerContext;
        }

        /// <inheritdoc cref="IRepositoryWriter{T}"/>
        public virtual void Add([NotNull] TEntity entity)
        {
            if (entity is IEntityWithNumber entityWithId &&
                entityWithId.Number == Guid.Empty)
            {
                entityWithId.Number = Guid.NewGuid();
            }
            AuditForCreate(entity);
            AuditForUpdate(entity);
            writerContext.Writer.Add(entity);
        }

        /// <inheritdoc cref="IRepositoryWriter{T}"/>
        public void Update([NotNull] TEntity entity)
        {
            AuditForUpdate(entity);
            writerContext.Writer.Update(entity);
        }

        /// <inheritdoc cref="IRepositoryWriter{T}"/>
        public void Delete([NotNull] TEntity entity)
        {
            AuditForUpdate(entity);
            AuditForDelete(entity);
            if (entity is IEntityAuditDeleted)
            {
                writerContext.Writer.Update(entity);
            }
            else
            {
                writerContext.Writer.Delete(entity);
            }
        }

        private void AuditForCreate([NotNull] TEntity entity)
        {
            if (entity is IEntityAuditCreated auditCreated)
            {
                auditCreated.CreatedAt = writerContext.DateTimeProvider.UtcNow;
                auditCreated.CreatedBy = writerContext.UserName;
            }
        }

        private void AuditForUpdate([NotNull] TEntity entity)
        {
            if (entity is IEntityAuditUpdated auditUpdate)
            {
                auditUpdate.UpdatedAt = writerContext.DateTimeProvider.UtcNow;
                auditUpdate.UpdatedBy = writerContext.UserName;
            }
        }

        private void AuditForDelete([NotNull] TEntity entity)
        {
            if (entity is IEntityAuditDeleted auditDeleted)
            {
                auditDeleted.DeletedAt = writerContext.DateTimeProvider.UtcNow;
            }
        }
    }
}
