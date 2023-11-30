using System.Globalization;
using MyApp.View;
using myTodo.Model;
using myTodo.Model.Service;

namespace myTodo.Controller;

public class TodoList
{
    private DisplayMenu _displayMenu;
    private TodoView _todoView;
    private TodoListController _todoListController;
    private UserController _userController;
    private EFContext _efContext;
    public TodoList()
    {
        _displayMenu = new DisplayMenu();
        _todoView = new TodoView();
        _efContext = new EFContext();
        _userController = new UserController();
        _todoListController = new TodoListController(_efContext);
    }
    /// <summary>
    /// Method Menu for manage menu command
    /// </summary>
    public void Menu()
    {
        _displayMenu.LaunchMenu();
        bool exitCommandManager = false;
        while (!exitCommandManager)
        {
            _todoView.display("Enter a command :");
            string input = Console.ReadLine();
            string[] argument = input.Split(" ");
            Commands.Base command = (Commands.Base)Enum.Parse(typeof(Commands.Base), argument[0]);
            ManagerMenuCommands(argument, command);
        }
    }

    
    /// <summary>
    /// Manager Commands Menu
    /// </summary>
    /// <param name="argument">argument of the command</param>
    /// <param name="command">Command input</param>
    private void ManagerMenuCommands(string[] argument,Commands.Base command)
    {
        switch (command)
        {
            case Commands.Base.Help:
                _displayMenu.HelpMenuCommand();
                break;
            case Commands.Base.CreateUser:
                _userController.createUser(argument[1]);
                break;
            case Commands.Base.Add:
                string todoTaskDescription = string.Join(" ", argument.Skip(5));
                _todoListController.CreateTodoTask(int.Parse(argument[1]),ParsePriority(argument[2]), ParseDate(argument[3]), argument[4], todoTaskDescription, false);
                break;
            case Commands.Base.Update:
                _todoListController.UpdateTodoTask(int.Parse(argument[1]), argument[2]);
                break;
            case Commands.Base.Remove:
                _todoListController.deleteTodoTask(int.Parse(argument[1]));
                break;
            case Commands.Base.Filter:
                FilterManager();
                break;
            case Commands.Base.ShowTask:
                _todoListController.ReadTodoTask();
                break;
            case Commands.Base.ShowUser:
                _todoListController.ReadUser();
                break;
            case Commands.Base.ExportCSV:
                _todoListController.ExportCSV();
                break;
            case Commands.Base.ImportCSV:
                _todoListController.ImportCSV();
                break;
            default:
                _todoView.display("Error, incorrect command or don't exist");
                break;
        }
    }
    
    /// <summary>
    /// Method FilterManager for manage filter commands
    /// </summary>
    private void FilterManager()
    {
        _todoView.HelpCommandFilter();
        _todoView.display("Enter commands : ");
        string input = Console.ReadLine();
        string[] argument = input.Split(" ");
        Commands.Filter command = (Commands.Filter)Enum.Parse(typeof(Commands.Filter), argument[0]); 
        switch (command)
        {
            case Commands.Filter.Completed:
                _efContext.findCompleted(bool.Parse(argument[1]));
                break;
            case Commands.Filter.DueDate:
                _efContext.filterDueDate();
                break;
            case Commands.Filter.Priority:
                _efContext.filterPriority();
                break;
            case Commands.Filter.ShowNameUserTask:
                _efContext.GiveNameUserTask(int.Parse(argument[1]));
                break;
            case Commands.Filter.ShowUserWithoutTask:
                _efContext.GiveNameUserNotTask();
                break;
            default:
                _todoView.display("Error, incorrect command or don't exist");
                break;
        }
    }
    /// <summary>
    /// Parse string to date
    /// </summary>
    /// <param name="date">date string for convert</param>
    /// <returns>Parsed date</returns>
    private DateTime ParseDate(string date)
    {
        return DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
    }
    
    /// <summary>
    /// Parse string to priority
    /// </summary>
    /// <param name="priority">priority string for convert</param>
    /// <returns>Parsed priority</returns>
    private PriorityStatus ParsePriority(string priority)
    {
        return Enum.Parse<PriorityStatus>(priority);
    }
}