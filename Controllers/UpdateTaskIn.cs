namespace TodoListMongo.Controllers;

public class UpdateTaskIn
{
    public string Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public bool? Finished { get; set; }
}
