    

using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using SocialNetwork.Application.Contracts.dbo;
using SocialNetwork.Application.Core;
using SocialNetwork.Application.Dtos.dbo;
using SocialNetwork.Domain.Entities.dbo;
using SocialNetwork.Persistance.Interfaces.dbo;
using SocialNetwork.Application.Helpers.web;
using SocialNetwork.Application.Response;
using SocialNetwork.Persistance.Models.dbo;
using SocialNetwork.Application.Model.ViewModel;

namespace SocialNetwork.Application.Services.dbo
{
    public class PublicacionesService : IPublicacionesServices
    {
        private readonly IPublicacionesRepository _publicacionesRepository;
        private readonly ILogger<PublicacionesService> _logger;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AuthenticationResponse _authentication;

        public PublicacionesService(IPublicacionesRepository publicacionesRepository, ILogger<PublicacionesService> logger,
                                    IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _publicacionesRepository = publicacionesRepository;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
            _mapper = mapper;
            _authentication = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("usuario");
        }
        public async Task<ServiceResponse> GetAllAsync()
        {
            ServiceResponse response = new ServiceResponse();

            try
            {
                var result = await _publicacionesRepository.GetAll();
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
                response.Messages = "Ha ocurrido un error obteniendo las publicaciones.";
                _logger.LogError(response.Messages, ex.ToString());
            }
            return response;
        }

        public async Task<ServiceResponse> GetByIDAsync(int id)
        {
            ServiceResponse response = new ServiceResponse();

            try
            {
                var result = await _publicacionesRepository.GetById(id);

                if (result.Success)
                {
                    var publicacionesModel = result.Data as PublicacionesModel;

                    if (publicacionesModel != null)
                    {
                        var publicacionesViewModel = _mapper.Map<PublicacionesViewModel>(publicacionesModel);

                        response.Model = publicacionesViewModel;
                    }
                    else
                    {
                        response.IsSuccess = false;
                        response.Messages = "No se encontró la publicación.";
                    }
                }
                else
                {
                    response.IsSuccess = result.Success;
                    response.Messages = result.Message;
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Messages = "Ha ocurrido un error obteniendo la publicacion.";
                _logger.LogError(response.Messages, ex.ToString());
            }

            return response;
        }


        public async Task<ServiceResponse> MyPostAsync()
        {
            ServiceResponse response = new ServiceResponse();

            try
            {
                var authentication = _authentication.Id;

                if (authentication == null)
                {
                    response.IsSuccess = false;
                    response.Messages = "Usuario no autenticado.";
                    return response;
                }

                var result = await _publicacionesRepository.MyPost(authentication);

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
                response.Messages = "Ha ocurrido un error obteniendo sus publicaciones.";
                _logger.LogError(response.Messages, ex.ToString());
            }
            return response;
        }

        public async Task<ServiceResponse> PostByFriendsAsync()
        {
            ServiceResponse response = new ServiceResponse();

            try
            {
                var authentication = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("usuario");
                if (authentication == null)
                {
                    response.IsSuccess = false;
                    response.Messages = "Usuario no autenticado.";
                    return response;
                }

                var result = await _publicacionesRepository.PostByFriends(authentication.Id);

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
                response.Messages = "Ha ocurrido un error obteniendo las publicaciones de sus amigos.";
                _logger.LogError(response.Messages, ex.ToString());
            }
            return response;
        }

        public async Task<ServiceResponse> RemoveAsync(PublicacionesDto dto)
        {
            ServiceResponse response = new ServiceResponse();

            try
            {
                Publicaciones publicaciones = new Publicaciones();

                publicaciones.PublicacionID = dto.PublicacionID;

                var result = await _publicacionesRepository.Remove(publicaciones);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Messages = "Ha ocurrido un error eliminando la publicacion.";
                _logger.LogError(response.Messages, ex.ToString());
            }
            return response;
        }

        public async Task<ServiceResponse> SaveAsync(PublicacionesDto dto)
        {
            ServiceResponse response = new ServiceResponse();

            try
            {
                dto.UsuarioID = _authentication.Id;

                var publicacion = _mapper.Map<Publicaciones>(dto);
                var result = await _publicacionesRepository.Save(publicacion);

                dto.PublicacionID = publicacion.PublicacionID;
                response.Model = dto;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Messages = "Ha ocurrido un error guardando la publicacion.";
                _logger.LogError(response.Messages, ex.ToString());
            }
            return response;
        }

        public async Task<ServiceResponse> UpdateAsync(PublicacionesDto dto)
        {
            ServiceResponse response = new ServiceResponse();

            try
            {
                var resultGetBy = await _publicacionesRepository.GetById(dto.PublicacionID);
                if (!resultGetBy.Success)
                {
                    response.IsSuccess = resultGetBy.Success;
                    response.Messages = resultGetBy.Message;

                    return response;
                }
                dto.UsuarioID = _authentication.Id;
                var publicacion = _mapper.Map<Publicaciones>(dto);
                var result = await _publicacionesRepository.Update(publicacion);

                var dtoConvertion = _mapper.Map<PublicacionesDto>(publicacion);
                response.Model = dtoConvertion;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Messages = "Ha ocurrido un error actualizando la publicacion.";
                _logger.LogError(response.Messages, ex.ToString());
            }
            return response;
        }
    }
}
