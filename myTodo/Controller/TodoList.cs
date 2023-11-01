using System.Globalization;
using MyApp.View;
using myTodo.Model;
using myTodo.Model.Service;

namespace myTodo.Controller;

public class TodoList
{
    private DisplayMenu _displayMenu;
    private TodoView _todoView;
    private Crud _crud;
    private EFContext _efContext;
    
    public TodoList()
    {
        _displayMenu = new DisplayMenu();
        _todoView = new TodoView();
        _efContext = new EFContext();
        _crud = new Crud(_efContext);
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
            Commands command = (Commands)Enum.Parse(typeof(Commands), argument[0]); 
            switch (command)
            {
                case Commands.Help:
                    _displayMenu.HelpMenuCommand();
                    break;
                case Commands.Add:
                    string todoTaskDescription = string.Join(" ", argument.Skip(4));
                    _crud.CreateTodoTask(ParsePriority(argument[1]), ParseDate(argument[2]), argument[3], todoTaskDescription, false);
                    break;
                case Commands.Update:
                    _crud.UpdateTodoTask(int.Parse(argument[1]), argument[2]);
                    break;
                case Commands.Remove:
                    _crud.deleteTodoTask(int.Parse(argument[1]));
                    break;
                case Commands.Filter:
                    FilterManager();
                    break;
                case Commands.Show:
                    _crud.ReadTodoTask();
                    break;
                default:
                    _todoView.display("Error, incorrect command or don't exist");
                    break;
            }
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
        Commands command = (Commands)Enum.Parse(typeof(Commands), argument[0]); 
        switch (command)
        {
            case Commands.Completed:
                _efContext.findCompleted(bool.Parse(argument[1]));
                break;
            case Commands.DueDate:
                _efContext.filterDueDate();
                break;
            case Commands.Priority:
                _efContext.filterPriority();
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