

using SocialNetwork.Application.Base;
using SocialNetwork.Application.Core;
using SocialNetwork.Application.Dtos.dbo;

namespace SocialNetwork.Application.Contracts.dbo
{
    public interface IPublicacionesServices : IBaseService<ServiceResponse, PublicacionesDto>
    {
        Task<ServiceResponse> PostByFriendsAsync();
        Task<ServiceResponse> MyPostAsync();
    }
}
