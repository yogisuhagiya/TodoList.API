using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoList.Api.Data; 
using TodoList.Models; 
using System; 
using System.Collections.Generic; 
using System.Linq; 
using System.Threading.Tasks; 

namespace TodoList.Api.Controllers
{
    [Route("api/[controller]")] 
    [ApiController]
    public class ToDoItemsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ToDoItemsController(ApplicationDbContext context)
        {
            _context = context; // Dependency Injection of the DbContext
        }

        // GET (uncompleted): api/ToDoItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ToDoItem>>> GetUncompletedToDoItems()
        {
            return await _context.ToDoItems
                                 .Where(item => item.CompletedDate == null)
                                 .ToListAsync();
        }

        // GET (by ID): api/ToDoItems/5
        
        [HttpGet("{id}")]
        public async Task<ActionResult<ToDoItem>> GetToDoItemById(int id)
        {
            var toDoItem = await _context.ToDoItems.FindAsync(id);

            if (toDoItem == null)
            {
                return NotFound(); // HTTP 404 if item not found
            }

            return toDoItem; // HTTP 200 with the item
        }

        // POST: api/ToDoItems
        [HttpPost]
        public async Task<ActionResult<ToDoItem>> CreateToDoItem(ToDoItem toDoItem)
        {
            _context.ToDoItems.Add(toDoItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetToDoItemById), new { id = toDoItem.Id }, toDoItem);
        }

        // PUT: api/ToDoItems/5/complete
        [HttpPut("{id}/complete")]
        public async Task<IActionResult> MarkToDoItemComplete(int id)
        {
            var toDoItem = await _context.ToDoItems.FindAsync(id);

            if (toDoItem == null)
            {
                return NotFound(); // HTTP 404 if item not found
            }

            toDoItem.CompletedDate = DateTime.UtcNow; // Set completion date to current UTC time

            await _context.SaveChangesAsync();

            return NoContent(); // HTTP 204 No Content 
        }
    }
}
