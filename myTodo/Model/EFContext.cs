using Microsoft.EntityFrameworkCore;
using MyApp.View;
using myTodo.Controller;

namespace myTodo.Model;

public class EFContext : DbContext
{
    public DbSet<TodoTask> TodoTasks { get; set; }
    public DbSet<User> Users { get; set; }
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
                    _todoView.display($"{eResult.Id} {eResult.Name} {eResult.IsCompleted}");
                }
        }
    }

    public void filterDueDate()
    {
        using (var db = new EFContext())
        {
            var list = db.TodoTasks.ToList();
            list.Sort((a, b) => a.DueDate.CompareTo(b.DueDate));
            foreach (var eList in list)
            {
                _todoView.display($"{eList.Id} {eList.Name} {eList.DueDate}");
            }
        }
    }

    public void filterPriority()
    {
        using (var db = new EFContext())
        {
            var list = db.TodoTasks.ToList();
            list.Sort((a, b) => a.Priority.CompareTo(b.Priority));
            foreach (var eList in list)
            {
                _todoView.display($"{eList.Id} {eList.Name} {eList.Priority}");
            }
        }
    }
}