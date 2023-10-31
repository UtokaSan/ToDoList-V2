using myTodo.Controller;

namespace MyApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            TodoList todoList = new TodoList();
            todoList.Menu();
        }
    }
}