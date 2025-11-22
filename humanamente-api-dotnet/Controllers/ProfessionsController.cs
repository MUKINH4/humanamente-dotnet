using humanamente.Context;
using humanamente.DTO;
using humanamente.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace humanamente.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ProfessionsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProfessionsController(AppDbContext context)
        {
            _context = context;
        }

        // GET api/v1/professions?page=1&pageSize=10
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Profession>>> GetAll(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10)
        {
            if (page <= 0) page = 1;
            if (pageSize <= 0) pageSize = 10;

            var query = _context.Professions
                .Include(p => p.Tasks)
                .AsQueryable();

            var totalItems = await query.CountAsync();

            var items = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var result = new
            {
                page,
                pageSize,
                totalItems,
                items
            };

            return Ok(result);
        }

        // GET api/v1/professions/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Profession>> GetById(int id)
        {
            var profession = await _context.Professions
                .Include(p => p.Tasks)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (profession == null)
                return NotFound();

            return Ok(profession);
        }

        // POST api/v1/professions
        [HttpPost]
        public async Task<ActionResult<Profession>> Create([FromBody] CreateProfessionDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var profession = new Profession
            {
                Title = dto.Title,
                Description = dto.Description
            };

            _context.Professions.Add(profession);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetById),
                new { id = profession.Id },
                profession
            );
        }

        // PUT api/v1/professions/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] CreateProfessionDTO dto)
        {
            var profession = await _context.Professions.FindAsync(id);

            if (profession == null)
                return NotFound();

            profession.Title = dto.Title;
            profession.Description = dto.Description;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE api/v1/professions/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var profession = await _context.Professions.FindAsync(id);

            if (profession == null)
                return NotFound();

            _context.Professions.Remove(profession);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
