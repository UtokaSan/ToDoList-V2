using Microsoft.EntityFrameworkCore.Metadata.Conventions.Internal;
using MyApp.View;
using myTodo.Controller;

namespace myTodo.Model.Service;

public class Crud
{
    private TodoView _todoView;
    private EFContext _efContext;
    private User _user;
    public Crud(EFContext efContext)
    {
        _todoView = new TodoView();
        _efContext = efContext;
    }
    public void CreateTodoTask(PriorityStatus priority, DateTime dueDate, string name,
        string? description, bool isCompleted)
    {
        using (var db = new EFContext())
        {
            TodoTask todoTasks = new TodoTask(priority, DateTime.Now, dueDate, name, description, isCompleted);
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
}