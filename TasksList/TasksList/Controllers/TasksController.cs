using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TasksList.Data;
using TasksList.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TasksList.Controllers
{
    [Route("api/[controller]")]
    public class TasksController : Controller
    {
        private readonly TasksListContext _context;

        public TasksController(TasksListContext context)
        {
            _context = context;
        }

        // GET: api/Tasks
        [HttpGet]
        public IEnumerable<Todo> Get() => _context.Tasks;

        // GET: api/Tasks/{id}
        [HttpGet("{id}")]
        public Todo Get(int id)
        {
            // Getting single task
            Todo todo = _context.Tasks.FirstOrDefault(l => l.Id == id);

            // Checking if task exists
            if (todo == null) return new Todo();
            
            return todo;
        }

        // POST: api/Tasks
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Todo todo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_context.Tasks.Any(t => t.Id == todo.Id))
            {
                await _context.Tasks.AddAsync(todo);
                await _context.SaveChangesAsync();
            }

            return CreatedAtAction("Get", todo);
        }

        // PUT: api/Tasks/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]Todo todo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            //Get a single task
            var result = _context.Tasks.FirstOrDefault(t => t.Id == id);
            if (result != null)
            {
                result.Description = todo.Description;
                result.Completed = todo.Completed;
                _context.Update(result);
                await _context.SaveChangesAsync();
            }
            else
            {
                await Post(todo);
            }

            return Ok(result);
        }

        // DELETE: api/Tasks/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = _context.Tasks.FirstOrDefault(x => x.Id == id);

            if (result != null)
            {
                _context.Remove(result);
                await _context.SaveChangesAsync();
                return Ok(result);
            }

            return BadRequest(id);
        }
    }
}
