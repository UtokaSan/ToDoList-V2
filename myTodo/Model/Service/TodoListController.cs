using Microsoft.EntityFrameworkCore.Metadata.Conventions.Internal;
using MyApp.View;
using myTodo.Controller;

namespace myTodo.Model.Service;

public class TodoListController
{
    private TodoView _todoView;
    private EFContext _efContext;
    public TodoListController(EFContext efContext)
    {
        _todoView = new TodoView();
        _efContext = efContext;
    }
    public void CreateTodoTask(int userId, PriorityStatus priority, DateTime dueDate, string name,
        string? description, bool isCompleted)
    {
        using (var db = new EFContext())
        {
            TodoTask todoTasks = new TodoTask(userId, priority, DateTime.Now, dueDate, name, description, isCompleted);
            db.Add(todoTasks);
            db.SaveChanges();
            _todoView.display("Todo Task Created");
        }
    }
    public void ReadTodoTask()
    {
        using (var db = new EFContext())
        {
            var todoTasks = db.TodoTasks.ToList();
            foreach (var eTodoTask in todoTasks)
            {
                _todoView.display($"TodoTask : Id : {eTodoTask.Id} {eTodoTask.Name}, date time created : {eTodoTask.CreationDate}, due time : {eTodoTask.DueDate}");
            }
        }
    }

    public void ReadUser()
    {
        using (var db = new EFContext())
        {
            var users = db.Users.ToList();
            foreach (var eUser in users)
            {
                _todoView.display($"Users : Id : {eUser.Id}, Name {eUser.Name}");
            }
        }
    }
    public void UpdateTodoTask(int id, string description)
    {
        using (var db = new EFContext())
        {
            try
            {
                var todoTask = db.TodoTasks.Find(id);
                if (todoTask != null)
                {
                    todoTask.Description = description;
                    db.SaveChanges();
                }
                else
                {
                    Console.WriteLine($"The id {id} was not found");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }

    public void deleteTodoTask(int id)
    {
        using (var db = new EFContext())
        {
            try
            {
                var todoTask = db.TodoTasks.Find(id);
                if (todoTask != null)
                {
                    db.TodoTasks.Remove(todoTask);
                    db.SaveChanges();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
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
                Console.WriteLine(e);
                throw;
            }
        }
    }
}