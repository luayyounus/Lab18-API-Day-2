using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TasksList.Models;

namespace TasksList.Data
{
    public class TasksListContext : DbContext
    {
        public TasksListContext(DbContextOptions<TasksListContext> options) : base(options) { }

        internal DbSet<Todo> Tasks { get; set; }
        internal DbSet<List> Lists { get; set; }
    }
}
