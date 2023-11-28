namespace myTodo.Controller;

public class Commands
{
    public enum Base
    {
        Help,
        CreateUser,
        Add,
        Update,
        Remove,
        Filter,
        ShowTask,
        ShowUser,
        ExportCSV
    }

    public enum Filter
    {
        Completed,
        DueDate,
        Priority,
        ShowNameUserTask,
        ShowUserWithoutTask
    }
}