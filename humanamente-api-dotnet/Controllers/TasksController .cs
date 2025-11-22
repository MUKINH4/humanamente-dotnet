using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using humanamente.Context;
using humanamente.Models;
using humanamente.DTO;
using humanamente.Services;

namespace humanamente.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly AIService _aiService;

        public TasksController(AppDbContext context, AIService aiService)
        {
            _context = context;
            _aiService = aiService;
        }

        // GET api/v1/tasks?professionId=1
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskItem>>> GetAll(
            [FromQuery] int? professionId = null)
        {
            var query = _context.Tasks.AsQueryable();

            if (professionId.HasValue)
            {
                query = query.Where(t => t.ProfessionId == professionId.Value);
            }

            var tasks = await query.ToListAsync();
            return Ok(tasks);
        }

        // GET api/v1/tasks/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<TaskItem>> GetById(int id)
        {
            var task = await _context.Tasks.FindAsync(id);

            if (task == null)
                return NotFound();

            return Ok(task);
        }

        // POST api/v1/tasks/profession/1
        [HttpPost("profession/{professionId:int}")]
        public async Task<ActionResult<TaskItem>> Create(
    int professionId,
    [FromBody] CreateTaskDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var professionExists = await _context.Professions
                .AnyAsync(p => p.Id == professionId);

            if (!professionExists)
                return NotFound(new { message = "Profession not found." });

            var classification = await _aiService.ClassifyTaskAsync(dto.Description);

            var task = new TaskItem
            {
                Name = dto.Name,
                Description = dto.Description,
                ProfessionId = professionId,
                Classification = classification
            };

            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetById),
                new { id = task.Id },
                task
            );
        }

        // PUT api/v1/tasks/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] CreateTaskDTO dto)
        {
            var task = await _context.Tasks.FindAsync(id);

            if (task == null)
                return NotFound();

            task.Name = dto.Name;
            task.Description = dto.Description;

            // Reclassifica com IA quando a descrição muda
            task.Classification = await _aiService.ClassifyTaskAsync(dto.Description);

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE api/v1/tasks/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var task = await _context.Tasks.FindAsync(id);

            if (task == null)
                return NotFound();

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
