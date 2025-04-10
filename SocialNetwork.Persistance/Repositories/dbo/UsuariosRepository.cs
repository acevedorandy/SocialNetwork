

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SocialNetwork.Domain.Entities.dbo;
using SocialNetwork.Domain.Result;
using SocialNetwork.Persistance.Base;
using SocialNetwork.Persistance.Context;
using SocialNetwork.Persistance.HerpersRepository.dbo.usuarios;
using SocialNetwork.Persistance.Interfaces.dbo;
using SocialNetwork.Persistance.Models.dbo;
using SocialNetwork.Persistance.Validations.dbo;

namespace SocialNetwork.Persistance.Repositories.dbo
{
    public sealed class UsuariosRepository(SocialNetworkContext socialNetworkContext,
                                    ILogger<UsuariosRepository> logger, UsuariosValidations usuariosValidations) : BaseRepository<Usuarios>(socialNetworkContext), IUsuariosRepository
    {
        private readonly SocialNetworkContext _socialNetworkContext = socialNetworkContext;
        private readonly ILogger<UsuariosRepository> _logger = logger;
        private readonly UsuariosValidations _usuariosValidations = usuariosValidations;

        public async override Task<OperationResult> Save(Usuarios usuarios)
        {
            OperationResult result = new OperationResult();

            _usuariosValidations.ValidateSave(usuarios);

            try
            {
                usuarios.Contraseña = PasswordEncryption.Compute256Hash(usuarios.Contraseña);
                result = await base.Save(usuarios);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Ha ocurrido un error guardando el usuario.";
                _logger.LogError(result.Message, ex.ToString());
            }
            return (result);
        }

        public async override Task<OperationResult> Update(Usuarios usuarios)
        {
            OperationResult result = new OperationResult();

            _usuariosValidations.ValidateUpdate(usuarios);

            try
            {
                Usuarios? usuariosToUpdate = await _socialNetworkContext.Usuarios.FindAsync(usuarios.UsuarioID);

                usuariosToUpdate.UsuarioID = usuarios.UsuarioID;
                usuariosToUpdate.Nombre = usuarios.Nombre;
                usuariosToUpdate.Apellido = usuarios.Apellido;
                usuariosToUpdate.Telefono = usuarios.Telefono;
                usuariosToUpdate.Correo = usuarios.Correo;
                usuariosToUpdate.FotoPerfil = usuarios.FotoPerfil;
                usuariosToUpdate.NombreUsuario = usuarios.NombreUsuario;
                usuarios.Contraseña = PasswordEncryption.Compute256Hash(usuarios.Contraseña);
                usuariosToUpdate.Activo = usuarios.Activo;
                usuariosToUpdate.FechaRegistro = usuarios.FechaRegistro;

                result = await base.Update(usuariosToUpdate);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Ha ocurrido un error actualizando el usuario.";
                _logger.LogError(result.Message, ex.ToString());
            }
            return (result);
        }

        public async override Task<OperationResult> Remove(Usuarios usuarios)
        {
            OperationResult result = new OperationResult();

            _usuariosValidations.ValidateRemove(usuarios);

            try
            {
                result = await base.Remove(usuarios);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Ha ocurrido un error eliminando el usuario.";
                _logger.LogError(result.Message, ex.ToString());
            }
            return (result);
        }

        public async override Task<OperationResult> GetAll()
        {
            OperationResult result = new OperationResult();

            try
            {
                result.Data = await (from dbo in _socialNetworkContext.Usuarios
                                     orderby dbo.UsuarioID descending

                                     select new UsuariosModel()
                                     {
                                         UsuarioID = dbo.UsuarioID,
                                         Nombre = dbo.Nombre,
                                         Apellido = dbo.Apellido,
                                         Telefono = dbo.Telefono,
                                         Correo = dbo.Correo,
                                         FotoPerfil = dbo.FotoPerfil,
                                         NombreUsuario = dbo.NombreUsuario,
                                         Contraseña = dbo.Contraseña,
                                         Activo = dbo.Activo,
                                         FechaRegistro = dbo.FechaRegistro

                                     }).AsNoTracking()
                                     .ToListAsync();
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Ha ocurrido un error obteniendo los usuarios.";
                _logger.LogError(result.Message, ex.ToString());
            }
            return (result);
        }

        public async override Task<OperationResult> GetById(int id)
        {
            OperationResult result = new OperationResult();

            try
            {
                result.Data = await (from dbo in _socialNetworkContext.Usuarios
                                     where dbo.UsuarioID == id

                                     select new UsuariosModel()
                                     {
                                         UsuarioID = dbo.UsuarioID,
                                         Nombre = dbo.Nombre,
                                         Apellido = dbo.Apellido,
                                         Telefono = dbo.Telefono,
                                         Correo = dbo.Correo,
                                         FotoPerfil = dbo.FotoPerfil,
                                         NombreUsuario = dbo.NombreUsuario,
                                         Contraseña = dbo.Contraseña,
                                         Activo = dbo.Activo,
                                         FechaRegistro = dbo.FechaRegistro

                                     }).AsNoTracking()
                                     .ToListAsync();
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Ha ocurrido un error obteniendo el usuario.";
                _logger.LogError(result.Message, ex.ToString());
            }
            return (result);
        }
    }
}
