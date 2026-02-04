using CrunchyCom.Models;

namespace crunchycom.Data;

public interface IRepository
{
    Task<IEnumerable<PostEntity>> GetAllPostsAsync();
}
