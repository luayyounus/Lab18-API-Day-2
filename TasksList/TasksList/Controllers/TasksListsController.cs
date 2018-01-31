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
    public class TasksListsController : Controller
    {
        private readonly TasksListContext _context;

        public TasksListsController(TasksListContext context)
        {
            _context = context;
        }

        // GET: api/taskslist
        [HttpGet]
        public IEnumerable<List> Get() => _context.Lists;

        // GET: api/taskslist/{id}
        [HttpGet("{id}")]
        public List Get(int id)
        {
            // Getting single playlist
            List tasksList = _context.Lists.FirstOrDefault(l => l.Id == id);

            // Getting a list of todos associated with tasks list
            if (tasksList != null)
            {
                //tasksList.Todos = _context.Tasks.Where(t => t.ListId == id).ToList();

                return tasksList;
            }
            return new List{};
        }

        // POST: api/taskslist
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]List list)
        {
            if (!ModelState.IsValid) return BadRequest();
            if (!_context.Lists.Any(x => x.Id == list.Id))
            {
                await _context.Lists.AddAsync(list);
                await _context.SaveChangesAsync();
            }
            return CreatedAtAction("Get", list);
        }

        // PUT: api/taskslist/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]List newList)
        {
            if (!ModelState.IsValid) return BadRequest();
            List taskList = _context.Lists.FirstOrDefault(l => l.Id == id);
            if (taskList != null)
            {
                taskList = newList;
                await _context.Lists.AddAsync(taskList);
                await _context.SaveChangesAsync();
            }
            return Ok();
        }

        [HttpDelete("{id}")]
        // DELETE: api/taskslist/{id}
        public async Task<IActionResult> Delete(int id)
        {
            List deleteItem = _context.Lists.FirstOrDefault(l => l.Id == id);
            if(deleteItem != null) _context.Lists.Remove(deleteItem);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
