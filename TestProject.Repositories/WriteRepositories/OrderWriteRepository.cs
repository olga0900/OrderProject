using TestProject.Common.Entity.InterfaceDB;
using TestProject.Context.Contracts.Models;
using TestProject.Repositories.Anchors;
using TestProject.Repositories.Contracts;

namespace TestProject.Repositories.WriteRepository
{
    public class OrderWriteRepository : BaseWriteRepository<Order>, IOrderWriteRepository, IRepositoryAnchor
    {
        public OrderWriteRepository(IDbWriterContext writerContext) : base(writerContext)
        {
        }
    }
}
