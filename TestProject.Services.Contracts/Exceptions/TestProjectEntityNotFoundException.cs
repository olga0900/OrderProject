namespace TestProject.Services.Contracts.Exceptions
{
    public class TestProjectEntityNotFoundException<TEntity> : TestProjectNotFoundException
    {
        /// <summary>
        /// Инициализирует новый экземпляр <see cref="TestProjectEntityNotFoundException{TEntity}"/>
        /// </summary>
        public TestProjectEntityNotFoundException(Guid id)
            : base($"Сущность {typeof(TEntity)} c id = {id} не найдена.")
        {
        }
    }
}
