using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

using CrunchyCom.Data;

namespace CrunchyCom.Backend;

public class GetPosts
{
    private readonly ILogger _logger;
    private readonly PostRepository _postRepository;
    
    public GetPosts(ILogger<GetPosts> logger, PostRepository postRepository)
    {
        _logger = logger;
        _postRepository = postRepository;
    }

    [Function("GetPosts")]
    public async Task<HttpResponseData> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "Posts")]
        HttpRequestData req)
    {
        _logger.LogInformation("Retrieving all posts");
        
        var posts = await _postRepository.GetAllPostsAsync();
        
        var response = req.CreateResponse(HttpStatusCode.OK);
        response.Headers.Add("Content-Type", "application/json");
        await response.WriteAsJsonAsync(posts);
        
        return response;
    }
    
}