using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace myTodo.Controller;

public class User
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

    public int Id { get; set; }
    public string Name { get; set; }
    public ICollection<TodoTask> TodoTasks { get; set; }
    public User(string name)
    {
        Name = name;
    }

    public User()
    {
        
    }
}