using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace myTodo.Controller;

public class TodoTask
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public int UserId { get; set; }
    public PriorityStatus Priority { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime DueDate { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public bool IsCompleted { get; set; }
    [ForeignKey("UserId")]
    public User User { get; set; }
    public TodoTask(int userId, PriorityStatus priority, DateTime creationDate, DateTime dueDate, string name, string? description, bool isCompleted)
    {
        Priority = priority;
        CreationDate = creationDate;
        DueDate = dueDate;
        Name = name;
        Description = description;
        IsCompleted = isCompleted;
        UserId = userId;
    }
    
    public TodoTask()
    {
        
    }
    
    public override string ToString()
    {
        return
            $"Task: {Name}, Priority: {Priority}, Due Date: {DueDate}, " +
            $"Completed: {IsCompleted}, Description: {Description}";
    }
}