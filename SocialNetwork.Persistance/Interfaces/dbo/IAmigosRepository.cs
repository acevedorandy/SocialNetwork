

using SocialNetwork.Domain.Entities.dbo;
using SocialNetwork.Domain.Repositories;
using SocialNetwork.Domain.Result;

namespace SocialNetwork.Persistance.Interfaces.dbo
{
    public interface IAmigosRepository : IBaseRepository<Amigos>
    {
        Task<OperationResult> LookingForFriends(string friendsName);
        Task<OperationResult> GetFriends(string userId);
        Task<bool> ExistsRelation(string userId, string userId2);
    }
}
