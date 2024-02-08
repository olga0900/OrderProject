using Microsoft.EntityFrameworkCore;
using TestProject.Common.Entity.InterfaceDB;
using TestProject.Common.Entity.Repositories;
using TestProject.Context.Contracts.Models;
using TestProject.Repositories.Anchors;
using TestProject.Repositories.Contracts;

namespace TestProject.Repositories.ReadRepositories
{
    /// <summary>
    /// Реализация <see cref="IOrderReadRepository"/>
    /// </summary>
    public class OrderReadRepository : IOrderReadRepository, IRepositoryAnchor
    {
        /// <summary>
        /// Reader для связи с бд
        /// </summary>
        private readonly IDbRead reader;

        public OrderReadRepository(IDbRead reader)
        {
            this.reader = reader;
        }

        Task<IReadOnlyCollection<Order>> IOrderReadRepository.GetAllAsync(CancellationToken cancellationToken)
            => reader.Read<Order>()
                .NotDeletedAt()
                .OrderBy(x => x.PickupDate)
                .ToReadOnlyCollectionAsync(cancellationToken);

        Task<Order?> IOrderReadRepository.GetByIdAsync(Guid number, CancellationToken cancellationToken)
            => reader.Read<Order>()
                .ById(number)
                .NotDeletedAt()
                .FirstOrDefaultAsync(cancellationToken);
    }
}
