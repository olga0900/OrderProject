using FluentAssertions;
using TestProject.Context.Tests;
using TestProject.Repositories.Contracts;
using TestProject.Repositories.ReadRepositories;
using TestProject.Test.Extensions;
using Xunit;

namespace TestProject.Repositories.Tests
{
    public class OrderReadTests : TestProjectContextInMemory
    {
        private readonly IOrderReadRepository orderReadRepository;

        public OrderReadTests()
        {
            orderReadRepository = new OrderReadRepository(Reader);
        }

        /// <summary>
        /// Возвращает пустой список заказов
        /// </summary>
        [Fact]
        public async Task GetAllShouldReturnEmpty()
        {
            // Act
            var result = await orderReadRepository.GetAllAsync(CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.BeEmpty();
        }

        /// <summary>
        /// Возвращает список заказов
        /// </summary>
        [Fact]
        public async Task GetAllShouldReturnValues()
        {
            //Arrange
            var target = TestDataGenerator.Order();
            var target2 = TestDataGenerator.Order(x => x.DeletedAt = DateTimeOffset.UtcNow);

            await Context.Orders.AddRangeAsync(target,
                target2);
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            var result = await orderReadRepository.GetAllAsync(CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.HaveCount(1)
                .And.ContainSingle(x => x.Number == target.Number);
        }

        /// <summary>
        /// Получение заказа по идентификатору возвращает null
        /// </summary>
        [Fact]
        public async Task GetByIdShouldReturnNull()
        {
            //Arrange
            var number = Guid.NewGuid();

            // Act
            var result = await orderReadRepository.GetByIdAsync(number, CancellationToken);

            // Assert
            result.Should().BeNull();
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
            var result = await orderReadRepository.GetByIdAsync(target.Number, CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.BeEquivalentTo(target);
        }     
    }
}
