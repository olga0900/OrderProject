﻿namespace TestProject.Common.Entity.EntityInterface
{
    /// <summary>
    /// Аудит обновления сущности
    /// </summary>
    public interface IEntityAuditUpdated
    {
        /// <summary>
        /// Дата изменения
        /// </summary>
        public DateTimeOffset UpdatedAt { get; set; }

        /// <summary>
        /// Кто изменил
        /// </summary>
        public string UpdatedBy { get; set; }
    }
}
