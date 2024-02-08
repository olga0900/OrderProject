using TestProject.API.Models.CreateRequest;

namespace TestProject.API.Models.Request
{
    /// <summary>
    /// Сущность изменения заказа
    /// </summary>
    public class OrderRequest : CreateOrderRequest
    {
        /// <summary>
        /// Номер заказа
        /// </summary>
        public Guid Number { get; set; }
    }
}
