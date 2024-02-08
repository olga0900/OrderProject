using TestProject.Services.Contracts.Models;

namespace TestProject.Services.Contracts
{
    public interface IOrderService
    {
        /// <summary>
        /// Получить список всех <see cref="OrderModel"/>
        /// </summary>
        Task<IEnumerable<OrderModel>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="OrderModel"/> по идентификатору
        /// </summary>
        Task<OrderModel?> GetByIdAsync(Guid number, CancellationToken cancellationToken);

        /// <summary>
        /// Добавляет новую <see cref="OrderModel"/>
        /// </summary>
        Task<OrderModel> AddAsync(OrderModel model, CancellationToken cancellationToken);

        /// <summary>
        /// Редактирует существующую <see cref="OrderModel"/>
        /// </summary>
        Task<OrderModel> EditAsync(OrderModel source, CancellationToken cancellationToken);

        /// <summary>
        /// Удаляет существующую <see cref="OrderModel"/>
        /// </summary>
        Task DeleteAsync(Guid number, CancellationToken cancellationToken);
    }
}
