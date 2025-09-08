using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrganicStore.Model;
using OrganicStore.Service;
using OrganicStoreApplication.Context;

[ApiController]
[Route("api/[controller]")]
public class AdminController : ControllerBase
{
    private readonly IReportService _reports;
    private readonly DataContext _ctx;
    public AdminController(IReportService reports, DataContext ctx)
    {
        _reports = reports;
        _ctx = ctx;
    }

    [HttpGet("dashboard")]
    public async Task<IActionResult> Dashboard() => Ok(await _reports.GetDashboardAsync());

    [HttpGet("users")]
    public async Task<IActionResult> Users() => Ok(await _ctx.Users.ToListAsync());
   
    [HttpGet("stores")]
    public async Task<IActionResult> Stores() => Ok(await _ctx.Set<Store>().ToListAsync());

    [HttpGet("orders")]
    public async Task<IActionResult> Orders() => Ok(await _ctx.Orders
        .Include(o => o.OrderItems).ThenInclude(oi => oi.Product)
        .ToListAsync());
}
