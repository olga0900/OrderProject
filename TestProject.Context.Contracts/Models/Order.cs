namespace TestProject.Context.Contracts.Models
{
    /// <summary>
    /// Сущность заказа
    /// </summary>
    public class Order : BaseAuditEntity
    {
        /// <summary>
        /// Город отправителя
        /// </summary>
        public string SenderCity { get; set; } = string.Empty;

        /// <summary>
        /// Адрес отправителя
        /// </summary>
        public string SenderAddress { get; set;} = string.Empty;

        /// <summary>
        /// Город получателя
        /// </summary>
        public string RecipientCity { get; set; } = string.Empty;

        /// <summary>
        /// Адрес получателя
        /// </summary>
        public string RecipientAddress { get; set; } = string.Empty;

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
