using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetApi.Data;
using PetApi.Models;

namespace PetApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PetsController : ControllerBase
{
    private readonly PetContext _context;

    public PetsController(PetContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Pet>>> GetPets()
    {
        return await _context.Pets.ToListAsync();
    }
}
