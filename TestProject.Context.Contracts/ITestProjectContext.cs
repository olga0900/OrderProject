using Microsoft.EntityFrameworkCore;
using TestProject.Context.Contracts.Models;

namespace TestProject.Context.Contracts
{
    public interface ITestProjectContext
    {
        /// <summary>Список <inheritdoc cref="Order"/></summary>
        DbSet<Order> Orders { get; }
    }
}
