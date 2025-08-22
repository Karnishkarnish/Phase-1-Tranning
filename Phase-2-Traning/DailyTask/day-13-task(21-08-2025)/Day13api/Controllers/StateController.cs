using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Day13api.Context;
using Day13api.Models;

namespace Day13api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StateInfoController : ControllerBase
    {
        private readonly MyAppDbContext _context;

        public StateInfoController(MyAppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
       // [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateState(StateInfo model)
        {
            _context.States.Add(model);
            await _context.SaveChangesAsync();
            return Ok(model);
        }

        [HttpGet]
        //[Authorize(Roles = "Officer,Admin")]
        public IActionResult GetStates()
        {
            var states = _context.States.ToList();
            return Ok(states);
        }
    }
}
