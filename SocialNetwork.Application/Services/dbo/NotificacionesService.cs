

using AutoMapper;
using Microsoft.Extensions.Logging;
using SocialNetwork.Application.Contracts.dbo;
using SocialNetwork.Application.Core;
using SocialNetwork.Application.Dtos.dbo;
using SocialNetwork.Domain.Entities.dbo;
using SocialNetwork.Persistance.Interfaces.dbo;

namespace SocialNetwork.Application.Services.dbo
{
    public class NotificacionesService : INotificacionesServices
    {
        private readonly INotificacionesRepository _notificacionesRepository;
        private readonly ILogger<NotificacionesService> _logger;
        private readonly IMapper _mapper;

        public NotificacionesService(INotificacionesRepository notificacionesRepository, ILogger<NotificacionesService> logger, IMapper mapper)
        {
            _notificacionesRepository = notificacionesRepository;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<ServiceResponse> GetAllAsync()
        {
            ServiceResponse response = new ServiceResponse();

            try
            {
                var result = await _notificacionesRepository.GetAll();
                if (!result.Success)
                {
                    response.IsSuccess = result.Success;
                    response.Messages = result.Message;

                    return response;
                }
                response.Model = result.Data;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Messages = "Ha ocurrido un error obteniendo las notificaciones.";
                _logger.LogError(response.Messages, ex.ToString());
            }
            return response;
        }

        public async Task<ServiceResponse> GetByIDAsync(int id)
        {
            ServiceResponse response = new ServiceResponse();

            try
            {
                var result = await _notificacionesRepository.GetById(id);
                if (!result.Success)
                {
                    response.IsSuccess = result.Success;
                    response.Messages = result.Message;

                    return response;
                }
                response.Model = result.Data;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Messages = "Ha ocurrido un error obteniendo la notificacion.";
                _logger.LogError(response.Messages, ex.ToString());
            }
            return response;
        }

        public async Task<ServiceResponse> RemoveAsync(NotificacionesDto dto)
        {
            ServiceResponse response = new ServiceResponse();

            try
            {
                Notificaciones notificaciones = new Notificaciones();

                notificaciones.NotificacionID = dto.NotificacionID;

                var result = await _notificacionesRepository.Remove(notificaciones);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Messages = "Ha ocurrido un error eliminando la notificacion.";
                _logger.LogError(response.Messages, ex.ToString());
            }
            return response;
        }

        public async Task<ServiceResponse> SaveAsync(NotificacionesDto dto)
        {
            ServiceResponse response = new ServiceResponse();

            try
            {
                var notificaciones = _mapper.Map<Notificaciones>(dto);
                var result = await _notificacionesRepository.Save(notificaciones);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Messages = "Ha ocurrido un error guardando la notificacion.";
                _logger.LogError(response.Messages, ex.ToString());
            }
            return response;
        }

        public async Task<ServiceResponse> UpdateAsync(NotificacionesDto dto)
        {
            ServiceResponse response = new ServiceResponse();

            try
            {
                var resultGetBy = await _notificacionesRepository.GetById(dto.NotificacionID);
                if (!resultGetBy.Success)
                {
                    response.IsSuccess = resultGetBy.Success;
                    response.Messages = resultGetBy.Message;

                    return response;
                }
                var notificacion = _mapper.Map<Notificaciones>(dto);
                var result = await _notificacionesRepository.Update(notificacion);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Messages = "Ha ocurrido un error actualizando la notificacion.";
                _logger.LogError(response.Messages, ex.ToString());
            }
            return response;
        }
    }
}
