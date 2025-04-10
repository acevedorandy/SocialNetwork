

using SocialNetwork.Domain.Entities.dbo;
using SocialNetwork.Domain.Repositories;
using SocialNetwork.Domain.Result;

namespace SocialNetwork.Persistance.Interfaces.dbo
{
    public interface IPublicacionesRepository : IBaseRepository<Publicaciones>
    {
        Task<OperationResult> PostByFriends(string userId);
        Task<OperationResult> MyPost(string userId);
    }
}
