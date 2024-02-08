using TestProject.Context.Contracts.Models;

namespace TestProject.Repositories.Contracts
{
    /// <summary>
    /// Репозиторий записи <see cref="Order"/>
    /// </summary>
    public interface IOrderWriteRepository : IBaseWriteRepository<Order>
    {      
    }
}
