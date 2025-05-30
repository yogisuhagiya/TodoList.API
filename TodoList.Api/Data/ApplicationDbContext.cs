using Microsoft.EntityFrameworkCore; 
using TodoList.Models; 
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