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
                          "CreateUser [name]\n" +
                          "Add [id user] [priority] [due date] [name] [description]\n" +
                          "Update [id] [description]\n" +
                          "Remove [id]\n" +
                          "Remove-Priority [priority]\n" +
                          "Filter\n" +
                          "ShowTask\n" +
                          "ShowUser\n" +
                          "CreateUser [name]");
    }

    public void HelpFilterCommand()
    {
        Console.WriteLine("What Filter :\n" + 
                          "Task completed : Task-Complete\n" + 
                          "Due date : Due-Date\n" +
                          "Priority : Priority");
    }
}