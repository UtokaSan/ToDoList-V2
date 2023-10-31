namespace MyApp.View;

public class DisplayMenu
{
    public void LaunchMenu()
    {
        Console.WriteLine("[Welcome to TodoList]");
        Console.WriteLine("First use command Help");
    }

    public void HelpMenuCommand()
    {
        Console.WriteLine("The commands available are :\n" +
                          "Add [priority] [due date] [name] [description]\n" +
                          "Update [id] [description]\n" +
                          "Remove [id]\n" +
                          "Remove-Priority [priority]\n" +
                          "Filter\n" +
                          "Show");
    }

    public void HelpFilterCommand()
    {
        Console.WriteLine("What Filter :\n" + 
                          "Task completed : Task-Complete\n" + 
                          "Due date : Due-Date\n" +
                          "Priority : Priority");
    }
}