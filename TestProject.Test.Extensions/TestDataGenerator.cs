using TestProject.Context.Contracts.Models;
using TestProject.Services.Contracts.Models;

namespace TestProject.Test.Extensions
{
    /// <summary>
    /// Класс генерации данных для тестов
    /// </summary>
    public static class TestDataGenerator
    {
        static public Order Order(Action<Order>? settings = null)
        {
            var result = new Order
            {
                RecipientAddress = $"{Guid.NewGuid():N}",
                RecipientCity = $"{Guid.NewGuid():N}",
                SenderAddress = $"{Guid.NewGuid():N}",
                SenderCity = $"{Guid.NewGuid():N}",
                Weight = Random.Shared.Next(20, 20000),
                PickupDate = DateTimeOffset.Now.AddDays(2)
            };
            result.BaseAuditSetParamtrs();

            settings?.Invoke(result);
            return result;
        }

        static public OrderModel OrderModel(Action<OrderModel>? settings = null)
        {
            var result = new OrderModel
            {
                Number = Guid.NewGuid(),
                RecipientAddress = $"{Guid.NewGuid():N}",
                RecipientCity = $"{Guid.NewGuid():N}",
                SenderAddress = $"{Guid.NewGuid():N}",
                SenderCity = $"{Guid.NewGuid():N}",
                Weight = Random.Shared.Next(20, 20000),
                PickupDate = DateTimeOffset.Now.AddDays(2)
            };

            settings?.Invoke(result);
            return result;
        }
    }
}
