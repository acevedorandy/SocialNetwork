

using SocialNetwork.Application.Core;
using SocialNetwork.Application.Dtos.identity.account;
using SocialNetwork.Application.Model.ViewModel;

namespace SocialNetwork.Application.Contracts.identity
{
    public interface IProfileService 
    {
        Task<PerfilViewModel> GetUserByEmail(string userId);
        Task<ServiceResponse> UpdateProfile(PerfilDto perfilDto);

    }
}
