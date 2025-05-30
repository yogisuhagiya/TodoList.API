using Microsoft.EntityFrameworkCore; // Required for DbContext and DbSet
using TodoList.Models; // Required to reference ToDoItem
namespace TodoList.Api.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // This registers the ToDoItem class with the DataContext
        public DbSet<ToDoItem> ToDoItems { get; set; }
    }
}