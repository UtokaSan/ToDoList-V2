using System.Globalization;
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
    private User _user;
    
    //Singleton Pattern
    private static readonly Lazy<EFContext> lazyInstance = new Lazy<EFContext>(() => new EFContext());
    public static EFContext Instance => lazyInstance.Value;
    public EFContext()
    {
        _todoView = new TodoView();
        _user = new User();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TodoTask>()
            .HasOne(t => t.User)
            .WithMany(u => u.TodoTasks)
            .HasForeignKey(t => t.UserId);

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

    public void FilterShowNameUser(int id)
    {
        using (var db = new EFContext())
        {
            var result = db.TodoTasks.Where(x => x.Id == id).ToList();
            foreach (var eResult in result)
            {
                _todoView.display($"{eResult.UserId}");
            }
        }
    }
    public void changeUserIdTodoTask(int idOfTask, int newIdUser)
    {
        using (var db = new EFContext())
        {
            try
            {
                var todoTask = db.TodoTasks.Find(idOfTask);
                if (todoTask != null)
                {
                    todoTask.UserId = newIdUser;
                }
            }
            catch (Exception e)
            {
                _todoView.displayError(e);
                throw;
            }
        }
    }

    public void GiveNameUserTask(int idOfTask)
    {
        using (var db = new EFContext())
        {
            try
            {
                var todoTask = db.TodoTasks.Find(idOfTask);
                if (todoTask != null)
                {
                    var user = db.Users.Find(todoTask.UserId);
                    if (user != null)
                    {
                        _todoView.display(user.Name);   
                    }
                }
            }
            catch (Exception e)
            {
                _todoView.displayError(e);
                throw;
            }
        }
    }

    public void GiveNameUserNotTask()
    {
        using (var db = new EFContext())
        {
            try
            {
                var usersWithoutTasks = db.Users
                    .Where(user => !db.TodoTasks.Any(task => task.UserId == user.Id))
                    .ToList();

                foreach (var user in usersWithoutTasks)
                {
                    _todoView.display($"User without task : {user.Name}");
                }
            }
            catch (Exception e)
            {
                _todoView.displayError(e);
                throw;
            }
        }
    }
}