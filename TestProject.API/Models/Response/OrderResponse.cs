namespace TestProject.API.Models.Response
{
    /// <summary>
    /// Сущность ответа заказа
    /// </summary>
    public class OrderResponse
    {
        /// <summary>
        /// Номер заказа
        /// </summary>
        public Guid Number { get; set; }

        /// <summary>
        /// Данные отправителя
        /// </summary>
        public string SenderData { get; set; } = string.Empty;

        /// <summary>
        /// Данные получателя
        /// </summary>
        public string RecipientData { get; set; } = string.Empty;

        /// <summary>
        /// Вес
        /// </summary>
        public decimal Weight { get; set; }

        /// <summary>
        /// Дата забора груза
        /// </summary>
        public DateTimeOffset PickupDate { get; set; }
    }
}
