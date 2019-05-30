using System;

namespace ToDoOperations
{
    public class ToDoItem
    {
        public string Id { get; set; } = Guid.NewGuid().ToString("n");
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
    }
}
