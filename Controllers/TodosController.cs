using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using TodoListMongo.Domain;
using TodoListMongo.Settings;

namespace TodoListMongo.Controllers;

[ApiController]
[Route("todos")]
public class TodosController : ControllerBase
{
    private readonly IMongoCollection<TaskToDo> _tasks;

    public TodosController(DatabaseSettings databaseSettings)
    {
        var client = new MongoClient(databaseSettings.Connection);
        var database = client.GetDatabase(databaseSettings.Name);
        _tasks = database.GetCollection<TaskToDo>("tasks");
    }

    [HttpGet("")]
    public async Task<IActionResult> GetAll()
    {
        var allTasks = await _tasks.Find(_ => true).ToListAsync();

        if (allTasks.Count == 0)
            return NotFound("No tasks have been registered yet.");

        return Ok(allTasks);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        var task = await _tasks.Find(t => t.Id == id).FirstOrDefaultAsync();

        if (task is null)
            return NotFound("Task not found.");

        return Ok(task);
    }

    [HttpPost("")]
    public async Task<IActionResult> Insert(InsertTaskIn data)
    {
        var task = new TaskToDo(data.Title, data.Description);

        await _tasks.InsertOneAsync(task);

        return NoContent();
    }

    [HttpPut("")]
    public async Task<IActionResult> Update(UpdateTaskIn data)
    {
        var task = await _tasks.Find(t => t.Id == data.Id).FirstOrDefaultAsync();

        if (task is null)
            return NotFound("Task not found.");

        task.Update(data.Title, data.Description, data.Finished);

        await _tasks.ReplaceOneAsync(t => t.Id == data.Id, task);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var task = await _tasks.Find(t => t.Id == id).FirstOrDefaultAsync();

        if (task is null)
            return NotFound("Task not found.");

        await _tasks.DeleteOneAsync(t => t.Id == id);

        return NoContent();
    }
}
