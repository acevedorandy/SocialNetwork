using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SocialNetwork.Domain.Entities.dbo;
using SocialNetwork.Domain.Result;
using SocialNetwork.Identity.Shared.Context;
using SocialNetwork.Persistance.Base;
using SocialNetwork.Persistance.Context;
using SocialNetwork.Persistance.Interfaces.dbo;
using SocialNetwork.Persistance.Models.dbo;
using SocialNetwork.Persistance.Models.ViewModel.dbo;
using SocialNetwork.Persistance.Validations.dbo;

namespace SocialNetwork.Persistance.Repositories.dbo
{
    public sealed class AmigosRepository(SocialNetworkContext socialNetworkContext, IdentityContext identityContext,
                                  ILogger<AmigosRepository> logger, AmigosValidations amigosValidations) : BaseRepository<Amigos>(socialNetworkContext), IAmigosRepository
    {
        private readonly SocialNetworkContext _socialNetworkContext = socialNetworkContext;
        private readonly ILogger<AmigosRepository> _logger = logger;
        private readonly AmigosValidations _amigosValidations = amigosValidations;
        private readonly IdentityContext _identityContext = identityContext;

        public async override Task<OperationResult> Save(Amigos amigos) 
        {
            OperationResult result = new OperationResult();

            _amigosValidations.ValidateSave(amigos);

            try
            {
                result = await base.Save(amigos);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Ha ocurrido un error guardando el amigo.";
                _logger.LogError(result.Message, ex.ToString());
            }
            return (result);
        }

        public async override Task<OperationResult> Update(Amigos amigos)
        {
            OperationResult result = new OperationResult();

            _amigosValidations.ValidateUpdate(amigos);

            try
            {
                Amigos? amigosToUpdate = await _socialNetworkContext.Amigos.FindAsync(amigos.AmigoID);

                amigosToUpdate.AmigoID = amigos.AmigoID;
                amigosToUpdate.UsuarioID1 = amigos.UsuarioID1;
                amigosToUpdate.UsuarioID2 = amigos.UsuarioID2;
                amigosToUpdate.FechaAmistad = amigos.FechaAmistad;

                result = await base.Update(amigosToUpdate);

            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Ha ocurrido un error actualizando el amigo.";
                _logger.LogError(result.Message, ex.ToString());
            }
            return (result);
        }

        public async override Task<OperationResult> Remove(Amigos amigos) 
        {
            OperationResult result = new OperationResult();

            _amigosValidations.ValidateRemove(amigos);

            try
            {
                result = await base.Remove(amigos);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Ha ocurrido un error eliminando el consultorio.";
                _logger.LogError(result.Message, ex.ToString());
            }
            return (result);
        }

        public async override Task<OperationResult> GetAll()
        {
            OperationResult result = new OperationResult();

            try
            {
                var amigos = await _socialNetworkContext.Amigos
                    .OrderByDescending(dbo => dbo.AmigoID)
                    .ToListAsync();

                var usuario = await _identityContext.Users
                    .ToListAsync();

                var datos = (from dbo in amigos
                             join dbo1 in usuario on dbo.UsuarioID1.ToString() equals dbo1.Id.ToString()
                             join dbo2 in usuario on dbo.UsuarioID2.ToString() equals dbo2.Id.ToString()
                             
                             select new AmigosModel
                             {
                                AmigoID = dbo.AmigoID,
                                UsuarioID1 = dbo1.Id,
                                UsuarioID2 = dbo2.Id,
                                FechaAmistad = dbo.FechaAmistad

                             }).ToList();

                result.Data = datos;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Ha ocurrido un error obteniendo los amigos.";
                _logger.LogError(result.Message, ex.ToString());
            }
            return (result);
        }

        public async override Task<OperationResult> GetById(int id) 
        {
            OperationResult result = new OperationResult();

            try
            {
                var amigos = await _socialNetworkContext.Amigos
                    .OrderByDescending(dbo => dbo.AmigoID)
                    .ToListAsync();

                var usuarios = await _identityContext.Users
                    .ToListAsync();

                var datos = (from dbo in amigos
                             join dbo1 in usuarios on dbo.UsuarioID1.ToString() equals dbo1.Id.ToString()
                             join dbo2 in usuarios on dbo.UsuarioID2.ToString() equals dbo2.Id.ToString()

                             where dbo.AmigoID == id
                             
                             select new AmigosModel
                             {
                                 AmigoID = dbo.AmigoID,
                                 UsuarioID1 = dbo1.Id,
                                 UsuarioID2 = dbo2.Id,
                                 FechaAmistad = dbo.FechaAmistad

                             }).FirstOrDefault();

                result.Data = datos;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Ha ocurrido un error obteniendo el amigo.";
                _logger.LogError(result.Message, ex.ToString());
            }
            return (result);
        }

        public async Task<OperationResult> LookingForFriends(string friendsName)
        {
            OperationResult result = new OperationResult();

            try
            {
                result.Data = await (from dbo in _identityContext.Users
                                     where dbo.Nombre.Contains(friendsName) 
                                         
                                     select new FriendsViewModel
                                     {
                                         Id = dbo.Id,
                                         Nombre = dbo.Nombre,
                                         Apellido = dbo.Apellido,
                                         Foto = dbo.Foto,
                                         UserName = dbo.UserName

                                     }).AsNoTracking().ToListAsync();
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Ha ocurrido un error buscando amigos.";
                _logger.LogError(result.Message, ex);
            }
            return result;
        }

        public async Task<OperationResult> GetFriends(string userId)
        {
            OperationResult result = new OperationResult();

            try
            {
                var amigos = await _socialNetworkContext.Amigos
                    .Where(a => a.UsuarioID1 == userId || a.UsuarioID2 == userId)
                    .ToListAsync();

                var usuariosAmigos = (from amigo in amigos
                                            join user in _identityContext.Users
                                            on (amigo.UsuarioID1 == userId ? amigo.UsuarioID2 : amigo.UsuarioID1) equals user.Id

                                            select new MisAmigosViewModel
                                            {
                                                AmistadID = amigo.AmigoID,
                                                Foto = user.Foto, 
                                                Nombre = user.Nombre,
                                                Apellido = user.Apellido,
                                                NombreUsuario = user.UserName,
                                                FechaAmistad = amigo.FechaAmistad

                                            }).ToList();

                result.Data = usuariosAmigos;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Ha ocurrido un error obteniendo sus amigos.";
                _logger.LogError(result.Message, ex.ToString());
            }
            return result;
        }

        public async Task<bool> ExistsRelation(string userId1, string userId2)
        {
            var relation = await _socialNetworkContext.Amigos
                .Where(a => (a.UsuarioID1 == userId1 && a.UsuarioID2 == userId2) || 
                (a.UsuarioID1 == userId2 && a.UsuarioID2 == userId1))
                .FirstOrDefaultAsync();

            return relation != null; 
        }

    }
}
