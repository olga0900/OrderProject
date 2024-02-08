using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Text;
using TestProject.API.Models.CreateRequest;
using TestProject.API.Models.Request;
using TestProject.API.Models.Response;
using TestProject.API.Tests.Infrastructures;
using TestProject.Context.Contracts.Models;
using TestProject.Test.Extensions;
using Xunit;

namespace TestProject.API.Tests.Tests
{
    public class OrderIntergrationTests : BaseIntegrationTest
    {
        public OrderIntergrationTests(TestProjectApiFixture fixture) : base(fixture)
        {
        }

        [Fact]
        public async Task AddShouldWork()
        {
            // Arrange
            var client = factory.CreateClient();
            var order = mapper.Map<CreateOrderRequest>(TestDataGenerator.OrderModel());

            // Act
            string data = JsonConvert.SerializeObject(order);
            var contextdata = new StringContent(data, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("/Order", contextdata);
            var resultString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<OrderResponse>(resultString);

            var orderFirst = await context.Orders.FirstAsync(x => x.Number == result!.Number);

            // Assert          
            orderFirst.Should()
                .BeEquivalentTo(order);
        }

        [Fact]
        public async Task EditShouldWork()
        {
            // Arrange
            var client = factory.CreateClient();
            var order = TestDataGenerator.Order();
            await context.Orders.AddAsync(order);
            await unitOfWork.SaveChangesAsync();

            var orderRequest = mapper.Map<OrderRequest>(TestDataGenerator.OrderModel(x => x.Number = order.Number));

            // Act
            string data = JsonConvert.SerializeObject(orderRequest);
            var contextdata = new StringContent(data, Encoding.UTF8, "application/json");
            await client.PutAsync("/Order", contextdata);

            var orderFirst = await context.Orders.FirstAsync(x => x.Number == orderRequest.Number);

            // Assert           
            orderFirst.Should()
                .BeEquivalentTo(orderRequest);
        }

        [Fact]
        public async Task GetShouldWork()
        {
            // Arrange
            var client = factory.CreateClient();
            var order1 = TestDataGenerator.Order();
            var order2 = TestDataGenerator.Order();

            await context.Orders.AddRangeAsync(order1, order2);
            await unitOfWork.SaveChangesAsync();

            // Act
            var response = await client.GetAsync($"/Order/{order1.Number}");

            // Assert
            response.EnsureSuccessStatusCode();
            var resultString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<OrderResponse>(resultString);

            result.Should()
                .NotBeNull()
                .And
                .BeEquivalentTo(new
                {
                    order1.Number,
                    order1.Weight,
                    order1.PickupDate
                });
        }

        [Fact]
        public async Task GetAllShouldWork()
        {
            // Arrange
            var client = factory.CreateClient();
            var order1 = TestDataGenerator.Order();
            var order2 = TestDataGenerator.Order(x => x.DeletedAt = DateTimeOffset.Now);

            await context.Orders.AddRangeAsync(order1, order2);
            await unitOfWork.SaveChangesAsync();

            // Act
            var response = await client.GetAsync("/Order");

            // Assert
            response.EnsureSuccessStatusCode();
            var resultString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<IEnumerable<OrderResponse>>(resultString);

            result.Should()
                .NotBeNull()
                .And
                .Contain(x => x.Number == order1.Number)
                .And
                .NotContain(x => x.Number == order2.Number);
        }

        [Fact]
        public async Task DeleteShouldWork()
        {
            // Arrange
            var client = factory.CreateClient();
            var order = TestDataGenerator.Order();
            await context.Orders.AddAsync(order);
            await unitOfWork.SaveChangesAsync();

            // Act
            await client.DeleteAsync($"/Order/{order.Number}");

            var orderFirst = await context.Orders.FirstAsync(x => x.Number == order.Number);

            // Assert
            orderFirst.DeletedAt.Should()
                .NotBeNull();

            orderFirst.Should()
            .BeEquivalentTo(new
            {
                order.Number,
                order.Weight,
                order.PickupDate
            });
        }
    }
}
