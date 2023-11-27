using myTodo.Controller;

namespace MyApp.View;

public class TodoView
{
    public void display(string text)
    {
        Console.WriteLine(text);
    }

    public void displayError(Exception e)
    {
        Console.WriteLine(e);
    }

    public void displayObject(int id)
    {
        Console.WriteLine(id);
    }
    public void ColorText(ConsoleColor color, string text)
    {
        Console.ForegroundColor = color;
        Console.WriteLine(text);
        Console.ResetColor();
    }
    
    public void HelpCommandFilter()
    {
        Console.WriteLine("Filter task completed : Completed [true/false]\n" +
                          "Filter due date : DueDate\n" +
                          "Filter priority : Priority");
    } 
}