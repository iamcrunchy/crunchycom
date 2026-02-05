using Azure.Data.Tables;

namespace crunchycom.Seed;

public class SeedEntity : ITableEntity
{
    public string PartitionKey { get; set; } = "General"; // Matches your hardcoded PK
    public string RowKey { get; set; }
    public DateTimeOffset? Timestamp { get; set; }
    public Azure.ETag ETag { get; set; }
    
    // Custom Blog Fields
    public string Title { get; set; }
    public string Content { get; set; }
    public DateTime CreatedDate { get; set; }
}

public class TableSeeder
{
    public static async Task SeedLocalData()
    {
        // Shortcut connection string for local Azurite/Emulator
        string connectionString = "UseDevelopmentStorage=true";
        string tableName = "Posts";

        var tableClient = new TableClient(connectionString, tableName);
        await tableClient.CreateIfNotExistsAsync();

        var posts = new[]
        {
            new SeedEntity { RowKey = "001", Title = "First Local Post", Content = "Hello from Azurite!", CreatedDate = DateTime.UtcNow },
            new SeedEntity { RowKey = "002", Title = "The Professional Way", Content = "Seeding data locally is safer than Prod.", CreatedDate = DateTime.UtcNow.AddMinutes(-10) },
            new SeedEntity { RowKey = "003", Title = "Ubuntu vs Windows", Content = "This code works the same on both OSs.", CreatedDate = DateTime.UtcNow.AddHours(-1) }
        };

        foreach (var post in posts)
        {
            // Upsert will either Insert or Update existing RowKeys
            await tableClient.UpsertEntityAsync(post);
            Console.WriteLine($"Seeded: {post.Title}");
        }
    }
}