
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SocialNetwork.Domain.Entities.dbo;
using SocialNetwork.Domain.Result;
using SocialNetwork.Identity.Shared.Context;
using SocialNetwork.Persistance.Base;
using SocialNetwork.Persistance.Context;
using SocialNetwork.Persistance.Interfaces.dbo;
using SocialNetwork.Persistance.Models.dbo;
using SocialNetwork.Persistance.Validations.dbo;

namespace SocialNetwork.Persistance.Repositories.dbo
{
    public sealed class NotificacionesRepository(SocialNetworkContext socialNetworkContext, IdentityContext identityContext,
                                          ILogger<NotificacionesRepository> logger, NotificacionesValidations notificacionesValidations) : BaseRepository<Notificaciones>(socialNetworkContext), INotificacionesRepository
    {
        private readonly SocialNetworkContext _socialNetworkContext = socialNetworkContext;
        private readonly ILogger<NotificacionesRepository> _logger = logger;
        private readonly NotificacionesValidations _notificacionesValidations = notificacionesValidations;
        private readonly IdentityContext _identityContext = identityContext;

        public async override Task<OperationResult> Save(Notificaciones notificaciones)
        {
            OperationResult result = new OperationResult();

            _notificacionesValidations.ValidateSave(notificaciones);

            try
            {
                result = await base.Save(notificaciones);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Ha ocurrido un error guardando la notificacion.";
                _logger.LogError(result.Message, ex.ToString());
            }
            return (result);
        }

        public async override Task<OperationResult> Update(Notificaciones notificaciones)
        {
            OperationResult result = new OperationResult();

            _notificacionesValidations.ValidateUpdate(notificaciones);

            try
            {
                Notificaciones? notificacionToUpdate = await _socialNetworkContext.Notificaciones.FindAsync(notificaciones.NotificacionID);

                notificacionToUpdate.NotificacionID = notificaciones.NotificacionID;
                notificacionToUpdate.UsuarioID = notificaciones.UsuarioID;
                notificacionToUpdate.Mensaje = notificaciones.Mensaje;
                notificacionToUpdate.Leida = notificaciones.Leida;
                notificacionToUpdate.FechaNotificacion = notificaciones.FechaNotificacion;

                result = await base.Update(notificacionToUpdate);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Ha ocurrido un error actualizando la notificacion.";
                _logger.LogError(result.Message, ex.ToString());
            }
            return (result);
        }

        public async override Task<OperationResult> Remove(Notificaciones notificaciones)
        {
            OperationResult result = new OperationResult();

            _notificacionesValidations.ValidateRemove(notificaciones);

            try
            {
                result = await base.Remove(notificaciones);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Ha ocurrido un error eliminando la notificacion.";
                _logger.LogError(result.Message, ex.ToString());
            }
            return (result);
        }

        public async override Task<OperationResult> GetAll()
        {
            OperationResult result = new OperationResult();

            try
            {
                var notificaciones = await _socialNetworkContext.Notificaciones
                    .OrderByDescending(dbo => dbo.NotificacionID)
                    .ToListAsync();

                var usuario = await _identityContext.Users
                    .ToListAsync();

                var datos = (from dbo in notificaciones
                             join dbo1 in usuario on dbo.UsuarioID.ToString() equals dbo1.Id.ToString()

                             select new NotificacionesModel
                             {
                                 NotificacionID = dbo.NotificacionID,
                                 UsuarioID = dbo1.Id,
                                 Mensaje = dbo.Mensaje,
                                 Leida = dbo.Leida,
                                 FechaNotificacion = dbo.FechaNotificacion

                             }).ToList();

                result.Data = datos;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Ha ocurrido un error obteniendo las notificaciones.";
                _logger.LogError(result.Message, ex.ToString());
            }
            return (result);
        }

        public async override Task<OperationResult> GetById(int id)
        {
            OperationResult result = new OperationResult();

            try
            {
                var notificaciones = await _socialNetworkContext.Notificaciones
                    .OrderByDescending(dbo => dbo.NotificacionID)
                    .ToListAsync();

                var usuario = await _identityContext.Users
                    .ToListAsync();

                var datos = (from dbo in notificaciones
                             join dbo1 in usuario on dbo.UsuarioID.ToString() equals dbo1.Id.ToString()

                             where dbo.NotificacionID == id

                             select new NotificacionesModel
                             {
                                 NotificacionID = dbo.NotificacionID,
                                 UsuarioID = dbo1.Id,
                                 Mensaje = dbo.Mensaje,
                                 Leida = dbo.Leida,
                                 FechaNotificacion = dbo.FechaNotificacion

                             }).FirstOrDefault();

                result.Data = datos;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Ha ocurrido un error obteniendo las notificaciones.";
                _logger.LogError(result.Message, ex.ToString());
            }
            return (result);
        }
    }
}
