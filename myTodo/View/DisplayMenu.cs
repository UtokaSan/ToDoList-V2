namespace MyApp.View;

public class DisplayMenu
{
    public void LaunchMenu()
    {
        Console.WriteLine("[Welcome to TodoList]\n" +
                          "First use command Help");
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("/!\\ You must create user before create todo");
        Console.ResetColor();
    }
    public void HelpMenuCommand()
    {
        Console.WriteLine("The commands available are :\n" +
                          "CreateUser [name]    Create user with name\n" +
                          "Add [id user] [priority] [due date] [name] [description]     Create task\n" +
                          "Update [id] [description]    Update task\n" +
                          "Remove [id]      Remove task\n" +
                          "Remove-Priority [priority]   Remove all task with a certain priority\n" +
                          "Filter   All filter\n" +
                          "ShowTask    Show all Tasks\n" +
                          "ShowUser    Show all Users\n" +
                          "CreateUser [name]    Create User with name\n");
    }

    public void HelpFilterCommand()
    {
        Console.WriteLine("What Filter :\n" + 
                          "Task completed : Task-Complete\n" + 
                          "Due date : Due-Date\n" +
                          "Priority : Priority");
    }
}