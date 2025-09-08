using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrganicStore.Dtos;
using OrganicStore.Service;
using System.Threading.Tasks;

namespace OrganicStore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _service;

        public OrdersController(IOrderService service)
        {
            _service = service;
        }

       
        [HttpPost]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> PlaceOrder([FromBody] OrderCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var res = await _service.PlaceOrderAsync(dto);
            if (!res.Success) return BadRequest(res);

            return Ok(res);
        }

       
        [HttpGet("{id:int}")]
        [Authorize] 
        public async Task<IActionResult> GetOrder(int id)
        {
            var res = await _service.GetOrderAsync(id);
            if (!res.Success) return NotFound(res);

            return Ok(res);
        }

      
        [HttpGet("customer/{customerId:int}")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> GetCustomerOrders(int customerId)
        {
            var res = await _service.GetCustomerOrdersAsync(customerId);
            return Ok(res);
        }

        [HttpGet("store/{storeId:int}")]
        [Authorize(Roles = "Store")]
        public async Task<IActionResult> GetStoreOrders(int storeId)
        {
            var res = await _service.GetStoreOrdersAsync(storeId);
            return Ok(res);
        }

        
        [HttpPut("{id:int}/status")]
        [Authorize(Roles = "Store")]
        public async Task<IActionResult> UpdateOrderStatus(int id, [FromBody] OrderStatusUpdateDto dto)
        {
            if (string.IsNullOrEmpty(dto.Status))
                return BadRequest(new { Message = "Status is required." });

            var res = await _service.UpdateOrderStatusAsync(id, dto.Status);
            if (!res.Success) return BadRequest(res);

            return Ok(res);
        }
    }
}
