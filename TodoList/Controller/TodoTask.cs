using System;

namespace TodoList.Controller
{
    public class TodoTask
    {
        public int _id { get; set; }
        public string _name { get; set; }
        public string? _description { get; set; }
        public PriorityStatus _priority { get; set; }
        private DateTime _creationDate;
        public DateTime _dueDate { get; set; }
        public bool _isCompleted { get; set; }
        
        public TodoTask(PriorityStatus priority, DateTime creationDate, DateTime dueDate, int id, string name, string? description, bool isCompleted)
        {
            _priority = priority;
            _creationDate = creationDate;
            _dueDate = dueDate;
            _id = id;
            _name = name;
            _description = description;
            _isCompleted = isCompleted;
        }

        public override string ToString()
        {
            return $"ID: {_id} Task: {_name}, Priority: {_priority}, Due Date: {_dueDate}, Completed: {_isCompleted}, Description: {_description}";
        }
    }
}