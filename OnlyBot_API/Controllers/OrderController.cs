using Microsoft.AspNetCore.Mvc;
using OnlyBot_Business;
using OnlyBot_Business.IRepository;
using OnlyBot_Models;
using OnlyBot_Models.APIResponse;

namespace OnlyBot_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : Controller
    {
        private readonly IOrderRepository _orderRepository;

        public OrderController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _orderRepository.GetAll());
        }

        [HttpGet("{orderId}")]
        public async Task<IActionResult> Get(Guid orderId)
        {
            if (orderId == Guid.Empty)
            {
                return BadRequest(new ErrorModel()
                {
                    ErrorMessage = "Invalid Id",
                    StatusCode = StatusCodes.Status400BadRequest
                });
            }

            var order = await _orderRepository.Get(orderId);
            if (order == null)
            {
                return NotFound(new ErrorModel()
                {
                    ErrorMessage = "Order not found",
                    StatusCode = StatusCodes.Status404NotFound
                });
            }

            return Ok(order);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Order Order)
        {
            if (Order == null)
            {
                return BadRequest(new ErrorModel()
                {
                    ErrorMessage = "Invalid order data",
                    StatusCode = StatusCodes.Status400BadRequest
                });
            }
            var createdOrder= await _orderRepository.Create(Order);

            return Ok();
        }
    }
}
