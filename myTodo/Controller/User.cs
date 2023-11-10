using System.ComponentModel.DataAnnotations;

namespace myTodo.Controller;

public class User
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public User(string name)
    {
        Name = name;
    }
}