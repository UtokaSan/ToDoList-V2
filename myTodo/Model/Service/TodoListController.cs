using System.Globalization;
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
            bool userExist = db.Users.Any(p => p.Id == userId);
            if (userExist)
            {
                TodoTask todoTasks = new TodoTask(userId, priority, DateTime.Now, dueDate, name, description, isCompleted);
                db.Add(todoTasks);
                db.SaveChanges();
                _todoView.display("Todo Task Created");   
            }
            else
            {
                _todoView.ColorText(ConsoleColor.Red, "User don't exist");
            }
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
                    _todoView.display($"The id {id} was not found");
                }
            }
            catch (Exception e)
            {
                _todoView.displayError(e);
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
                _todoView.displayError(e);
            }
        }
    }
    public void ExportCSV()
    {
        using (var db = new EFContext())
        {
            var todoTasks = db.TodoTasks.ToList();
            var users = db.Users.ToList();
            CreateCSV(todoTasks);
            string currentDirectory = Directory.GetCurrentDirectory();
            _todoView.ColorText(ConsoleColor.Green,$"CSV exported successfully in : {currentDirectory}");
        }
    }
    
    private void CreateCSV(List<TodoTask> todoTasks)
    {
        using (StreamWriter writer = new StreamWriter(Path.Combine(Directory.GetCurrentDirectory(), "db.csv")))
        using (CsvHelper.CsvWriter csv = new CsvHelper.CsvWriter(writer, CultureInfo.InvariantCulture))
        {
            csv.WriteRecords(todoTasks);
        }
    }
}