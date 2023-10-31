using Microsoft.EntityFrameworkCore;
using MyApp.View;
using myTodo.Controller;

namespace myTodo.Model;

public class EFContext : DbContext
{
    public DbSet<TodoTask> TodoTasks { get; set; }
    private const string connectionString = "Server=(localdb)\\mssqllocaldb;Database=EFCore;Trusted_Connection=True;";
    private TodoView _todoView;

    public EFContext()
    {
        _todoView = new TodoView();
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(connectionString);
    }

    public void findCompleted(bool completed)
    {
        using (var db = new EFContext())
        {
                var result = db.TodoTasks.Where(x => x.IsCompleted == completed).ToList();
                foreach (var eResult in result)
                {
                    _todoView.display(eResult.Id + " " +eResult.Name + " " + eResult.IsCompleted);
                }
        }
    }
}