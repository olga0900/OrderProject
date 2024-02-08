namespace TestProject.Services.Contracts.Exceptions
{
    public abstract class TestProjectException : Exception
    {
        /// <summary>
        /// Инициализирует новый экземпляр <see cref="TestProjectException"/> без параметров
        /// </summary>
        protected TestProjectException() { }

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="TestProjectException"/> с указанием
        /// сообщения об ошибке
        /// </summary>
        protected TestProjectException(string message)
            : base(message) { }
    }
}
