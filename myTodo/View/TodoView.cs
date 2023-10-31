namespace MyApp.View;

public class TodoView
{
    public void display(string text)
    {
        Console.WriteLine(text);
    }

    public void HelpCommandFilter()
    {
        Console.WriteLine("Filter task completed : Completed\n" +
                          "Filter due date : DueDate\n" +
                          "Filter priority : Priority");
    } 
}