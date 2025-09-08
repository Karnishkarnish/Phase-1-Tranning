using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrganicStore.Model;
using OrganicStore.Service;

[ApiController]
[Route("api/[controller]")]
public class StoresController : ControllerBase
{
    private readonly IStoreService _service;
    public StoresController(IStoreService service) => _service = service;

    
    [Authorize(Roles = "Admin,Store")]
    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id) => Ok(await _service.GetStoreAsync(id));

   
   [Authorize(Roles = "Admin")]
    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await _service.GetAllStoresAsync());

   
    [Authorize(Roles = "Store")]
    [HttpPost("{storeId:int}/products")]
    public async Task<IActionResult> UpsertProduct(int storeId, [FromBody] Product product)
        => Ok(await _service.AddOrUpdateProductAsync(storeId, product));

   
    [Authorize(Roles = "Store")]
    [HttpGet("{storeId:int}/orders")]
    public async Task<IActionResult> IncomingOrders(int storeId)
        => Ok(await _service.GetIncomingOrdersAsync(storeId));

    [Authorize(Roles = "Store")]
    [HttpPut("{storeId:int}/orders/{orderId:int}/status")]
    public async Task<IActionResult> UpdateOrderStatus(int storeId, int orderId, [FromBody] dynamic body)
        => Ok(await _service.UpdateOrderStatusAsync(storeId, orderId, (string)body.status));
}
