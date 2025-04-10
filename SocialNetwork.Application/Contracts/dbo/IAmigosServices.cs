

using SocialNetwork.Application.Base;
using SocialNetwork.Application.Core;
using SocialNetwork.Application.Dtos.dbo;
using SocialNetwork.Domain.Result;

namespace SocialNetwork.Application.Contracts.dbo
{
    public interface IAmigosServices : IBaseService<ServiceResponse, AmigosDto>
    {
        Task<ServiceResponse> LookingForFriendsAsync(string friendsName);
        Task<ServiceResponse> GetFriendAsync();
        Task<bool> ExistsRelation(string userId2);


    }
}
