using AutoMapper;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using TestProject.Context.Contracts.Models;
using TestProject.Context.Tests;
using TestProject.Repositories.Contracts;
using TestProject.Repositories.ReadRepositories;
using TestProject.Repositories.WriteRepository;
using TestProject.Services.Contracts;
using TestProject.Services.Contracts.Exceptions;
using TestProject.Services.Mappers;
using TestProject.Services.Services;
using TestProject.Test.Extensions;
using Xunit;

namespace TestProject.Services.Tests
{
    public class OrderServiceTest : TestProjectContextInMemory
    {
        private readonly IOrderService orderService;
        private readonly IOrderReadRepository orderReadRepository;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="OrderServiceTest"/>
        /// </summary>
        public OrderServiceTest()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ServiceMapper());
            });

            orderReadRepository = new OrderReadRepository(Reader);

            orderService = new OrderService(orderReadRepository, new OrderWriteRepository(WriterContext), config.CreateMapper(), 
                UnitOfWork,  new ServicesValidator());
        }

        /// <summary>
        /// Получение заказа по идентификатору возвращает null
        /// </summary>
        [Fact]
        public async Task GetByIdShouldReturnNull()
        {
            //Arrange
            var id = Guid.NewGuid();

            // Act
            Func<Task> result = () => orderService.GetByIdAsync(id, CancellationToken);

            // Assert
            await result.Should().ThrowAsync<TestProjectEntityNotFoundException<Order>>()
               .WithMessage($"*{id}*");
        }

        /// <summary>
        /// Получение заказа по идентификатору возвращает данные
        /// </summary>
        [Fact]
        public async Task GetByIdShouldReturnValue()
        {
            //Arrange
            var target = TestDataGenerator.Order();
            await Context.Orders.AddAsync(target);
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            var result = await orderService.GetByIdAsync(target.Number, CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.BeEquivalentTo(new
                {
                    target.Number,
                    target.SenderCity,
                    target.SenderAddress,
                    target.RecipientCity,
                    target.RecipientAddress,
                    target.Weight,
                    target.PickupDate
                });
        }

        /// <summary>
        /// Получение заказов по идентификаторам возвращает пустую коллекцию
        /// </summary>
        [Fact]
        public async Task GetAllShouldReturnEmpty()
        {
            // Act
            var result = await orderService.GetAllAsync(CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.BeEmpty();
        }

        /// <summary>
        /// Получение списка заказов по идентификаторам возвращает данные
        /// </summary>
        [Fact]
        public async Task GetAllShouldReturnValues()
        {
            //Arrange
            var target = TestDataGenerator.Order();

            await Context.Orders.AddRangeAsync(target,
                TestDataGenerator.Order(x => x.DeletedAt = DateTimeOffset.UtcNow));
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            var result = await orderService.GetAllAsync(CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.HaveCount(1)
                .And.ContainSingle(x => x.Number == target.Number);
        }

        /// <summary>
        /// Удаление несуществуюущего заказа
        /// </summary>
        [Fact]
        public async Task DeletingNonExistentCinemaReturnExсeption()
        {
            //Arrange
            var id = Guid.NewGuid();

            // Act
            Func<Task> result = () => orderService.DeleteAsync(id, CancellationToken);

            // Assert
            await result.Should().ThrowAsync<TestProjectEntityNotFoundException<Order>>()
               .WithMessage($"*{id}*");
        }

        /// <summary>
        /// Удаление удаленного заказа
        /// </summary>
        [Fact]
        public async Task DeletingDeletedCinemaReturnExсeption()
        {
            //Arrange
            var model = TestDataGenerator.Order(x => x.DeletedAt = DateTime.UtcNow);
            await Context.Orders.AddAsync(model);
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            Func<Task> result = () => orderService.DeleteAsync(model.Number, CancellationToken);

            // Assert
            await result.Should().ThrowAsync<TestProjectEntityNotFoundException<Order>>()
                .WithMessage($"*{model.Number}*");
        }

        /// <summary>
        /// Удаление заказа
        /// </summary>
        [Fact]
        public async Task DeleteShouldWork()
        {
            //Arrange
            var model = TestDataGenerator.Order();
            await Context.Orders.AddAsync(model);
            await UnitOfWork.SaveChangesAsync(CancellationToken);

            //Act
            Func<Task> act = () => orderService.DeleteAsync(model.Number, CancellationToken);

            // Assert
            await act.Should().NotThrowAsync();
            var entity = Context.Orders.Single(x => x.Number == model.Number);
            entity.Should().NotBeNull();
            entity.DeletedAt.Should().NotBeNull();
        }

        /// <summary>
        /// Добавление заказа
        /// </summary>
        [Fact]
        public async Task AddShouldWork()
        {
            //Arrange
            var model = TestDataGenerator.OrderModel();

            //Act
            Func<Task> act = () => orderService.AddAsync(model, CancellationToken);

            // Assert
            await act.Should().NotThrowAsync();
            var entity = Context.Orders.Single(x => x.Number == model.Number);
            entity.Should().NotBeNull();
            entity.DeletedAt.Should().BeNull();
        }

        /// <summary>
        /// Добавление невалидируемого заказа
        /// </summary>
        [Fact]
        public async Task AddShouldValidationException()
        {
            //Arrange
            var model = TestDataGenerator.OrderModel(x => x.SenderAddress = "T");

            //Act
            Func<Task> act = () => orderService.AddAsync(model, CancellationToken);

            // Assert
            await act.Should().ThrowAsync<TestProjectValidationException>();
        }

        /// <summary>
        /// Изменение несуществующего заказа
        /// </summary>
        [Fact]
        public async Task EditShouldNotFoundException()
        {
            //Arrange
            var model = TestDataGenerator.OrderModel();

            //Act
            Func<Task> act = () => orderService.EditAsync(model, CancellationToken);

            // Assert
            await act.Should().ThrowAsync<TestProjectEntityNotFoundException<Order>>()
                .WithMessage($"*{model.Number}*");
        }

        /// <summary>
        /// Изменение невалидируемого заказа
        /// </summary>
        [Fact]
        public async Task EditShouldValidationException()
        {
            //Arrange
            var model = TestDataGenerator.OrderModel(x => x.RecipientCity = "T");

            //Act
            Func<Task> act = () => orderService.EditAsync(model, CancellationToken);

            // Assert
            await act.Should().ThrowAsync<TestProjectValidationException>();
        }

        /// <summary>
        /// Изменение заказа
        /// </summary>
        [Fact]
        public async Task EditShouldWork()
        {
            //Arrange
            var model = TestDataGenerator.OrderModel();
            var order = TestDataGenerator.Order(x => x.Number = model.Number);
            await Context.Orders.AddAsync(order);
            await UnitOfWork.SaveChangesAsync(CancellationToken);

            //Act
            Func<Task> act = () => orderService.EditAsync(model, CancellationToken);

            // Assert
            await act.Should().NotThrowAsync();
            var entity = Context.Orders.Single(x => x.Number == order.Number);
            entity.Should().NotBeNull()
                .And
                .BeEquivalentTo(new
                {
                    model.Number,
                    model.SenderCity,
                    model.SenderAddress,
                    model.RecipientCity,
                    model.RecipientAddress,
                    model.Weight,
                    model.PickupDate
                });
        }
    }
}
