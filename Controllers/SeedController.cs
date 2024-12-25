using Microsoft.AspNetCore.Mvc;
using TreasuryManagementAPI.Data;
using TreasuryManagementAPI.Models;

[ApiController]
[Route("api/[controller]")]
public class SeedController : ControllerBase
{
    private readonly TreasuryDbContext _context;

    public SeedController(TreasuryDbContext context)
    {
        _context = context;
    }

    [HttpPost("seed-products")]
    public IActionResult SeedProducts()
    {
        if (!_context.Products.Any())
        {
            _context.Products.Add(new Product { Name = "Laptop", Price = 1000000.99M, Stock = 99 });
            _context.Products.Add(new Product { Name = "Mouse", Price = 10000000.99M, Stock = 100 });
            _context.SaveChanges();
            return Ok("Products seeded successfully.");
        }

        return BadRequest("Products table is already seeded.");
    }
}

