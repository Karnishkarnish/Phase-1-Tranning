using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrganicStore.Service;

namespace OrganicStore.Controllers 
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")] 
    public class ReportsController : ControllerBase
    {
        private readonly IReportService _service;
        public ReportsController(IReportService service) => _service = service;

        [HttpGet("sales")]
        public async Task<IActionResult> Sales([FromQuery] DateTime from, [FromQuery] DateTime to, [FromQuery] string groupBy = "month")
            => Ok(await _service.GetSalesAsync(from, to, groupBy));

        [HttpGet("top-products")]
        public async Task<IActionResult> TopProducts([FromQuery] DateTime from, [FromQuery] DateTime to, [FromQuery] int top = 10)
            => Ok(await _service.GetTopProductsAsync(from, to, top));
    }
}

