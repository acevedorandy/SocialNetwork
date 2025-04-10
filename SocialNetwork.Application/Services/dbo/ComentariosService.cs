

using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using SocialNetwork.Application.Contracts.dbo;
using SocialNetwork.Application.Core;
using SocialNetwork.Application.Dtos.dbo;
using SocialNetwork.Application.Response;
using SocialNetwork.Domain.Entities.dbo;
using SocialNetwork.Persistance.Interfaces.dbo;
using SocialNetwork.Application.Helpers.web; 

namespace SocialNetwork.Application.Services.dbo
{
    public class ComentariosService : IComentariosServices
    {
        private readonly IComentariosRepository _comentariosRepository;
        private readonly ILogger<ComentariosService> _logger;
        private readonly IMapper _mapper;
        private IHttpContextAccessor _httpContextAccessor;
        private readonly AuthenticationResponse _authenticationResponse;

        public ComentariosService(IComentariosRepository comentariosRepository, ILogger<ComentariosService> logger, IMapper mapper,
                                  IHttpContextAccessor httpContextAccessor)
        {
            _comentariosRepository = comentariosRepository;
            _logger = logger;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _authenticationResponse = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("usuario");
        }
        public async Task<ServiceResponse> GetAllAsync()
        {
            ServiceResponse response = new ServiceResponse();

            try
            {
                var result = await _comentariosRepository.GetAll();
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
                response.Messages = "Ha ocurrido un error obteniendo los comentarios.";
                _logger.LogError(response.Messages, ex.ToString());
            }
            return response;
        }

        public async Task<ServiceResponse> GetByIDAsync(int id)
        {
            ServiceResponse response = new ServiceResponse();

            try
            {
                var result = await _comentariosRepository.GetById(id);
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
                response.Messages = "Ha ocurrido un error obteniendo el comentario.";
                _logger.LogError(response.Messages, ex.ToString());
            }
            return response;
        }
        
        public async Task<ServiceResponse> RemoveAsync(ComentariosDto dto)
        {
            ServiceResponse response = new ServiceResponse();

            try
            {
                Comentarios comentarios = new Comentarios();

                comentarios.ComentarioID = dto.ComentarioID;

                var result = await _comentariosRepository.Remove(comentarios);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Messages = "Ha ocurrido un error eliminando el comentario.";
                _logger.LogError(response.Messages, ex.ToString());
            }
            return response;
        }

        public async Task<ServiceResponse> SaveAsync(ComentariosDto dto)
        {
            ServiceResponse response = new ServiceResponse();

            try
            {
                dto.UsuarioID = _authenticationResponse.Id;
                var comentarios = _mapper.Map<Comentarios>(dto);
                var result = await _comentariosRepository.Save(comentarios);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Messages = "Ha ocurrido un error guardando el comentario.";
                _logger.LogError(response.Messages, ex.ToString());
            }
            return response;
        }

        public async Task<ServiceResponse> UpdateAsync(ComentariosDto dto)
        {
            ServiceResponse response = new ServiceResponse();

            try
            {
                var resultGetBy = await _comentariosRepository.GetById(dto.ComentarioID);
                if (!resultGetBy.Success)
                {
                    response.IsSuccess = resultGetBy.Success;
                    response.Messages = resultGetBy.Message;

                    return response;
                }

                var comentarios = _mapper.Map<Comentarios>(dto);
                var result = await _comentariosRepository.Update(comentarios);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Messages = "Ha ocurrido un error actualizando el comentario.";
                _logger.LogError(response.Messages, ex.ToString());
            }
            return response;
        }
    }
}
