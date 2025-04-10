

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SocialNetwork.Application.Contracts.identity;
using SocialNetwork.Application.Core;
using SocialNetwork.Application.Dtos.identity.account;
using SocialNetwork.Application.Model.ViewModel;
using SocialNetwork.Identity.Shared.Context;
using SocialNetwork.Identity.Shared.Entities;

namespace SocialNetwork.Identity.Services
{
    public class ProfileService : IProfileService
    {
        private readonly IdentityContext _identityContext;
        private readonly UserManager<ApplicationUser> _userManager;
        public ProfileService(IdentityContext identityContext, UserManager<ApplicationUser> userManager)
        {
            _identityContext = identityContext;
            _userManager = userManager;
        }

        public async Task<PerfilViewModel?> GetUserByEmail(string userId)
        {
            try
            {
                var user = await _identityContext.Users
                    .Where(dbo => dbo.Id == userId)
                    .Select(dbo => new PerfilViewModel
                    {
                        Id = dbo.Id,
                        Nombre = dbo.Nombre,
                        Apellido = dbo.Apellido,
                        Telefono = dbo.PhoneNumber,
                        Email = dbo.Email,
                        Foto = dbo.Foto
                    })
                    .AsNoTracking()
                    .FirstOrDefaultAsync();

                return user;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el perfil del usuario.", ex);
            }
        }

        public async Task<ServiceResponse> UpdateProfile(PerfilDto perfilDto)
        {
            ServiceResponse response = new ServiceResponse();

            try
            {
                var user = await _userManager.FindByIdAsync(perfilDto.Id);
                if (user == null)
                {
                    response.IsSuccess = false;
                    response.Messages = "Usuario no encontrado.";
                    return response;
                }

                bool isPasswordValid = await _userManager.CheckPasswordAsync(user, perfilDto.Contraseña);
                if (!isPasswordValid)
                {
                    response.IsSuccess = false;
                    response.Messages = "La contraseña es incorrecta.";
                    return response;
                }

                user.Nombre = perfilDto.Nombre;
                user.Apellido = perfilDto.Apellido;
                user.PhoneNumber = perfilDto.Telefono;
                user.Email = perfilDto.Email;
                user.Foto = perfilDto.Foto;

                var result = await _userManager.UpdateAsync(user);

                perfilDto.Foto = user.Foto;
                response.Model = perfilDto; 

                if (!result.Succeeded)
                {
                    response.IsSuccess = false;
                    response.Messages = "Error al actualizar el usuario.";
                }
                
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Messages = "Ocurrió un error inesperado.";
            }

            return response;
        }

    }
}
