using AutoMapper;
using TestProject.Common.Entity.InterfaceDB;
using TestProject.Context.Contracts.Models;
using TestProject.Repositories.Contracts;
using TestProject.Services.Anchors;
using TestProject.Services.Contracts;
using TestProject.Services.Contracts.Exceptions;
using TestProject.Services.Contracts.Models;

namespace TestProject.Services.Services
{
    public class OrderService : IOrderService, IServiceAnchor
    {
        private readonly IOrderReadRepository orderReadRepository;
        private readonly IOrderWriteRepository orderWriteRepository;
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;
        private readonly IServiceValidator validator;

        public OrderService(IOrderReadRepository orderReadRepository, IOrderWriteRepository orderWriteRepository,
            IMapper mapper, IUnitOfWork unitOfWork, IServiceValidator validator)
        {
            this.mapper = mapper;
            this.orderReadRepository = orderReadRepository;
            this.orderWriteRepository = orderWriteRepository;
            this.unitOfWork = unitOfWork;
            this.validator = validator;
        }

        async Task<OrderModel> IOrderService.AddAsync(OrderModel model, CancellationToken cancellationToken)
        {
            model.Number = Guid.NewGuid();

            await validator.ValidateAsync(model, cancellationToken);

            var order = mapper.Map<Order>(model);

            orderWriteRepository.Add(order);
            await unitOfWork.SaveChangesAsync();
            return mapper.Map<OrderModel>(order);
        }

        async Task IOrderService.DeleteAsync(Guid number, CancellationToken cancellationToken)
        {
            var item = await orderReadRepository.GetByIdAsync(number, cancellationToken);

            if (item == null)
            {
                throw new TestProjectEntityNotFoundException<Order>(number);
            }

            orderWriteRepository.Delete(item);
            await unitOfWork.SaveChangesAsync();
        }

        async Task<OrderModel> IOrderService.EditAsync(OrderModel source, CancellationToken cancellationToken)
        {
            await validator.ValidateAsync(source, cancellationToken);

            var order = await orderReadRepository.GetByIdAsync(source.Number, cancellationToken);

            if(order == null)
            {
                throw new TestProjectEntityNotFoundException<Order>(source.Number);
            }

            order = mapper.Map<Order>(source);
            orderWriteRepository.Update(order);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return mapper.Map<OrderModel>(order);
        }

        async Task<IEnumerable<OrderModel>> IOrderService.GetAllAsync(CancellationToken cancellationToken)
        {
            var items = await orderReadRepository.GetAllAsync(cancellationToken);

            return items.Select(x => mapper.Map<OrderModel>(x));
        }

        async Task<OrderModel?> IOrderService.GetByIdAsync(Guid number, CancellationToken cancellationToken)
        {
            var item = await orderReadRepository.GetByIdAsync(number, cancellationToken);

            if (item == null)
            {
                throw new TestProjectEntityNotFoundException<Order>(number);
            }

            return mapper.Map<OrderModel>(item);
        }
    }
}
