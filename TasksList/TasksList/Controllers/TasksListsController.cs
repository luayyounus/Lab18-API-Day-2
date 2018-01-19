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

        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            // Getting single playlist
            List tasksList = _context.Lists.FirstOrDefault(l => l.Id == id);

            // Getting a list of todos associated with tasks list
            if (tasksList != null)
            {
                tasksList.Todos = _context.Tasks.Where(t => t.ListId == id).ToList();

                return Ok(tasksList);
            }
            return NotFound(id);
        }

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
