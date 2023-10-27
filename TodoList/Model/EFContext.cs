using Microsoft.EntityFrameworkCore;
using TodoList.Controller;

namespace TodoList.Model
{
    public class EFContext : DbContext
    {
        public DbSet<TodoTask> TodoTasks { get; set; }

        private const string connectionString = "Server=(localdb)\\mssqllocaldb;Database=EFCore;Trusted_Connection=True;";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}