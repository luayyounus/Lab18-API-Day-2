using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using TasksList;
using TasksList.Controllers;
using TasksList.Data;
using TasksList.Models;
using Xunit;

namespace XUnitTestTasksList
{
    public class UnitTest1
    {
        [Fact]
        public void TestGettingAllTodosInDb()
        {
            TasksListContext _context;
            DbContextOptions<TasksListContext> options = new DbContextOptionsBuilder<TasksListContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            using (_context = new TasksListContext(options))
            {
                List<Todo> testTodos = GetTodosObjects();

                // Adding two objects into the context to check if both are stored in database
                foreach (Todo x in testTodos)
                {
                    _context.Tasks.AddAsync(x);
                    _context.SaveChangesAsync();
                }

                // Arrange
                TasksController tasksController = new TasksController(_context);

                // Act
                IEnumerable<Todo> result = tasksController.Get();
                List<Todo> resultList = result.ToList();

                // Assert
                Assert.Equal(testTodos.Count, resultList.Count);
            }
        }

        [Fact]
        public void TestGettingTwoItemsFromOneList()
        { 
            TasksListContext _context;
            DbContextOptions<TasksListContext> options = new DbContextOptionsBuilder<TasksListContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            using (_context = new TasksListContext(options))
            {
                List oneList = new List {Name = "MyList"};

                _context.Lists.Add(oneList);
                _context.SaveChanges();

                List<Todo> testTodos = GetTodosObjects();
                foreach (Todo todo in testTodos) _context.Tasks.Add(todo);
                _context.SaveChanges();

                // Arrange
                TasksController tasksController = new TasksController(_context);

                // Act

                IEnumerable<Todo> allTodos = tasksController.Get();
                List<Todo> result = allTodos.ToList();

                // Assert
                Assert.Equal(2, result.Count);
            }
        }

        private List<Todo> GetTodosObjects()
        {
            return new List<Todo>
            {
                new Todo
                {
                    Id = 1,
                    Description = "Go to work!",
                    Completed = true,
                    List = new List(){Id = 1, Name = "MyList"}
                },
                new Todo
                {
                    Id = 2,
                    Description = "Eat lunch!",
                    Completed = false,
                    List = new List(){Id = 1, Name = "MyList"}
                }
            };
        }
    }
}
