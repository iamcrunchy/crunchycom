using crunchycom.Data;

namespace CrunchyCom.Data;

using Azure.Data.Tables;

using CrunchyCom.Models;

public class PostRepository : IRepository
{
    private readonly TableClient _tableClient;
	private const string PartitionKey = "General"; // TODO: hardcoded but needs to be made dynamic when more subjects are written about

    public PostRepository(TableServiceClient tableServiceClient)
    {
        _tableClient = tableServiceClient.GetTableClient("Posts");
		_tableClient.CreateIfNotExists();
    }

    public async Task<IEnumerable<PostEntity>> GetAllPostsAsync()
    {
        var posts = new List<PostEntity>();

        var queryResults = _tableClient.QueryAsync<PostEntity>(filter: $"PartitionKey eq '{PartitionKey}'");

        await foreach (var page in queryResults.AsPages())
        {
            posts.AddRange(page.Values);
        }

        return posts;
    }
}