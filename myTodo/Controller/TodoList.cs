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
    /// Method Menu for manage command
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
            switch (argument[0])
            {
                case "Help":
                    _displayMenu.HelpMenuCommand();
                    break;
                case "Add":
                    string todoTaskDescription = string.Join(" ", argument.Skip(4));
                    _crud.CreateTodoTask(ParsePriority(argument[1]), ParseDate(argument[2]), argument[3], todoTaskDescription, false);
                    break;
                case "Update":
                    _crud.UpdateTodoTask(int.Parse(argument[1]), argument[2]);
                    break;
                case "Remove":
                    _crud.deleteTodoTask(int.Parse(argument[1]));
                    break;
                case "Filter":
                    FilterManager();
                    break;
                case "Show":
                    _crud.ReadTodoTask();
                    break;
                default:
                    _todoView.display("Error, incorrect command or don't exist");
                    break;
            }
        }
    }
    private void FilterManager()
    {
        _todoView.HelpCommandFilter();
        _todoView.display("Enter commands : ");
        string input = Console.ReadLine();
        string[] argument = input.Split(" ");
        switch (argument[0])
        {
            case "Completed" when input.Length < 1:
                _efContext.findCompleted(bool.Parse(argument[1]));
                break;
        }
    }
    
    private DateTime ParseDate(string date)
    {
        return DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
    }

    private PriorityStatus ParsePriority(string priority)
    {
        return Enum.Parse<PriorityStatus>(priority);
    }
}