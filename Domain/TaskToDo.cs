namespace TodoListMongo.Domain;

public class TaskToDo
{
    public string Id { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? FinishedAt { get; set; }

    public TaskToDo(string title, string description)
    {
        Id = Guid.NewGuid().ToString();
        Title = title;
        Description = description;
        CreatedAt = DateTime.Now;
        FinishedAt = null;
    }

    public void Update(string? title, string? description, bool? finished)
    {
        Title = title ?? Title;
        Description = description ?? Description;
        if (finished != null)
            FinishedAt = finished!.Value ? DateTime.Now : null;
    }
}
