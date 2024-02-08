namespace TestProject.Services.Contracts.Exceptions
{
    public class TestProjectInvalidOperationException : TestProjectException
    {
        /// <summary>
        /// Инициализирует новый экземпляр <see cref="TestProjectInvalidOperationException"/>
        /// с указанием сообщения об ошибке
        /// </summary>
        public TestProjectInvalidOperationException(string message)
            : base(message)
        {

        }
    }
}
