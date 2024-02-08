using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using TestProject.API.Exceptions;
using TestProject.API.Models.CreateRequest;
using TestProject.API.Models.Request;
using TestProject.API.Models.Response;
using TestProject.Services.Contracts;
using TestProject.Services.Contracts.Models;

namespace TestProject.API.Controllers
{
    /// <summary>
    /// CRUD контроллер по работе с заказами
    /// </summary>
    [ApiController]
    [Route("[Controller]")]
    [ApiExplorerSettings(GroupName = "Order")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService orderService;
        private readonly IMapper mapper;

        public OrderController(IOrderService orderService, IMapper mapper)
        {
            this.orderService = orderService;
            this.mapper = mapper;
        }

        /// <summary>
        /// Получить список заказов
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(ICollection<OrderResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await orderService.GetAllAsync(cancellationToken);
            return Ok(result.Select(x => mapper.Map<OrderResponse>(x)));
        }

        /// <summary>
        /// Получить заказ по номеру
        /// </summary>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(OrderResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById([Required] Guid id, CancellationToken cancellationToken)
        {
            var item = await orderService.GetByIdAsync(id, cancellationToken);
            return Ok(mapper.Map<OrderResponse>(item));
        }

        /// <summary>
        /// Добавить заказ
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(OrderResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiValidationExceptionDetail), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Add(CreateOrderRequest model, CancellationToken cancellationToken)
        {
            var orderModel = mapper.Map<OrderModel>(model);
            var result = await orderService.AddAsync(orderModel, cancellationToken);
            return Ok(mapper.Map<OrderResponse>(result));
        }

        /// <summary>
        /// Изменить заказ по номеру
        /// </summary>
        [HttpPut]
        [ProducesResponseType(typeof(OrderResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiValidationExceptionDetail), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Edit(OrderRequest request, CancellationToken cancellationToken)
        {
            var model = mapper.Map<OrderModel>(request);
            var result = await orderService.EditAsync(model, cancellationToken);
            return Ok(mapper.Map<OrderResponse>(result));
        }

        /// <summary>
        /// Удалить клиента по номеру
        /// </summary>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status417ExpectationFailed)]
        public async Task<IActionResult> Delete([Required] Guid id, CancellationToken cancellationToken)
        {
            await orderService.DeleteAsync(id, cancellationToken);
            return Ok();
        }
    }
}
