namespace CrunchyCom.Data;

using Azure.Data.Tables;

using CrunchyCom.Models;

public class PostRepository
{
    private readonly TableClient _tableClient;

    public PostRepository(string connectionString)
    {
        _tableClient = new TableClient(connectionString, "Posts");
    }

    public async Task<List<PostEntity>> GetAllPostsAsync()
    {
        var posts = new List<PostEntity>();

        var queryResults = _tableClient.QueryAsync<PostEntity>(filter: "");

        await foreach (var entity in queryResults)
        {
            posts.Add(entity);
        }

        return posts;
    }
}