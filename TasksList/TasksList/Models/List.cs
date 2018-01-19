using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TasksList.Models
{
    public class List
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Todo> Todos { get; set; }
    }
}
