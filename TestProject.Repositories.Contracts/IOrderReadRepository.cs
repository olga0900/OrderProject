using TestProject.Context.Contracts.Models;

namespace TestProject.Repositories.Contracts
{
    public interface IOrderReadRepository
    {
        /// <summary>
        /// Получить <see cref="Order"/> по номеру
        /// </summary>
        Task<Order?> GetByIdAsync(Guid number, CancellationToken cancellationToken);

        /// <summary>
        /// Получить список всех <see cref="Order"/>
        /// </summary>
        Task<IReadOnlyCollection<Order>> GetAllAsync(CancellationToken cancellationToken);
    }
}
