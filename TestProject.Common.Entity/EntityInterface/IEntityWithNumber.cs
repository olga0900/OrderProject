namespace TestProject.Common.Entity.EntityInterface
{
    /// <summary>
    /// Аудит сущности с номером заказа
    /// </summary>
    public interface IEntityWithNumber
    {
        public Guid Number { get; set; }
    }
}
