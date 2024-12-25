using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TreasuryManagementAPI.Data;
using TreasuryManagementAPI.Models;

namespace TreasuryManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CollateralsController : ControllerBase
    {
        private readonly TreasuryDbContext _context;

        public CollateralsController(TreasuryDbContext context)
        {
            _context = context;
        }

        // GET: api/collaterals
        [Authorize]
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<IEnumerable<Collateral>>> GetCollaterals()
        {
            var collaterals = await _context.Collaterals.ToListAsync();

            if (collaterals == null || !collaterals.Any())
            {
                return NotFound("No collaterals found.");
            }

            return Ok(collaterals);
        }

        [Authorize]
        [HttpGet]
        [Route("filter")]
        public async Task<ActionResult<IEnumerable<Collateral>>> GetCollaterals([FromQuery] string filter, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var query = _context.Collaterals.AsQueryable();

            if (!string.IsNullOrEmpty(filter))
                query = query.Where(c => c.Name.Contains(filter) || c.AssetType.Contains(filter));

            var collaterals = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return Ok(collaterals);
        }


        // GET: api/collaterals/{id}
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<Collateral>> GetCollateralById(int id)
        {
            var collateral = await _context.Collaterals.FindAsync(id);

            if (collateral == null)
            {
                return NotFound($"Collateral with ID {id} not found.");
            }

            return Ok(collateral);
        }

        // POST: api/collaterals
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Collateral>> PostCollateral([FromBody] Collateral collateral)
        {
            if (collateral == null)
            {
                return BadRequest("Collateral data is required.");
            }

            _context.Collaterals.Add(collateral);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCollateralById), new { id = collateral.Id }, collateral);
        }

        // DELETE: api/collaterals/{id}
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCollateral(int id)
        {
            var collateral = await _context.Collaterals.FindAsync(id);

            if (collateral == null)
            {
                return NotFound($"Collateral with ID {id} not found.");
            }

            _context.Collaterals.Remove(collateral);
            await _context.SaveChangesAsync();

            return Ok($"Collateral with ID {id} deleted.");
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCollateral(int id, [FromBody] Collateral updatedCollateral)
        {
            if (id != updatedCollateral.Id)
                return BadRequest("ID mismatch.");

            var existingCollateral = await _context.Collaterals.FindAsync(id);
            if (existingCollateral == null)
                return NotFound($"Collateral with ID {id} not found.");

            existingCollateral.Name = updatedCollateral.Name;
            existingCollateral.AssetType = updatedCollateral.AssetType;
            existingCollateral.Value = updatedCollateral.Value;

            await _context.SaveChangesAsync();
            return Ok(existingCollateral);
        }

        [Authorize]
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchCollateral(int id, [FromBody] JsonPatchDocument<Collateral> patchDoc)
        {
            if (patchDoc == null)
                return BadRequest("Invalid patch document.");

            var collateral = await _context.Collaterals.FindAsync(id);
            if (collateral == null)
                return NotFound($"Collateral with ID {id} not found.");

            patchDoc.ApplyTo(collateral, (Microsoft.AspNetCore.JsonPatch.Adapters.IObjectAdapter)ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _context.SaveChangesAsync();
            return Ok(collateral);
        }



    }
}
