namespace TestProject.Services.Contracts.Exceptions
{
    public class TestProjectNotFoundException : TestProjectException
    {
        /// <summary>
        /// Инициализирует новый экземпляр <see cref="TestProjectNotFoundException"/> с указанием
        /// сообщения об ошибке
        /// </summary>
        public TestProjectNotFoundException(string message)
            : base(message)
        { }
    }
}
