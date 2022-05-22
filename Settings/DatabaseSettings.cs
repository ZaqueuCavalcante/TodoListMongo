namespace TodoListMongo.Settings;

public class DatabaseSettings
{
    public string Name { get; set; }
    public string Connection { get; set; }

    public DatabaseSettings(IConfiguration configuration) 
    {
        configuration.GetSection("Database").Bind(this);
    }
}
