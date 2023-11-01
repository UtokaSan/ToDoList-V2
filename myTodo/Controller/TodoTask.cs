using System.ComponentModel.DataAnnotations;

namespace myTodo.Controller;

public class TodoTask
{
    [Key]
    public int Id { get; set; }

    public List<int> TodoTaskIds;
    public string Name { get; set; }
    public string? Description { get; set; }
    public PriorityStatus Priority { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime DueDate { get; set; }
    public bool IsCompleted { get; set; }

    public TodoTask(PriorityStatus priority, DateTime creationDate, DateTime dueDate, string name,
        string? description, bool isCompleted)
    {
        Priority = priority;
        CreationDate = DateTime.Now;
        DueDate = dueDate;
        Name = name;
        Description = description;
        IsCompleted = isCompleted;
    }

    public override string ToString()
    {
        return
            $"Task: {Name}, Priority: {Priority}, Due Date: {DueDate}, " +
            $"Completed: {IsCompleted}, Description: {Description}";
    }
}