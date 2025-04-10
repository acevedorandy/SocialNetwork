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

namespace SocialNetwork.Application.Services.dbo
{
    public class AmigosService : IAmigosServices
    {
        private readonly IAmigosRepository _amigosRepository;
        private readonly ILogger<AmigosService> _logger;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AuthenticationResponse authentication;
        public AmigosService(IAmigosRepository amigosRepository, ILogger<AmigosService> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _amigosRepository = amigosRepository;
            _logger = logger;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            authentication = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("usuario");

        }

        public async Task<bool> ExistsRelation(string userId2)
        {
            string userId1 = authentication.Id;

            var result = await _amigosRepository.ExistsRelation(userId1, userId2);
            return result;
        }

        public async Task<ServiceResponse> GetAllAsync()
        {
            ServiceResponse response = new ServiceResponse();

            try
            {
                var result = await _amigosRepository.GetAll();
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
                response.Messages = "Ha ocurrido un error obteniendo los amigos.";
                _logger.LogError(response.Messages, ex.ToString());
            }
            return response;
        }

        public async Task<ServiceResponse> GetByIDAsync(int id)
        {
            ServiceResponse response = new ServiceResponse();

            try
            {
                var result = await _amigosRepository.GetById(id);
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
                response.Messages = "Ha ocurrido un error obteniendo el amigo.";
                _logger.LogError(response.Messages, ex.ToString());
            }
            return response;
        }

        public async Task<ServiceResponse> GetFriendAsync()
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

                var result = await _amigosRepository.GetFriends(authentication.Id);

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
                response.Messages = "Ha ocurrido un error obteniendo sus amigos.";
                _logger.LogError(response.Messages, ex.ToString());
            }
            return response;
        }


        public async Task<ServiceResponse> LookingForFriendsAsync(string friendsName)
        {
            ServiceResponse response = new ServiceResponse();

            try
            {
                var result = await _amigosRepository.LookingForFriends(friendsName);

                if (!result.Success)
                {
                    response.IsSuccess = result.Success;
                    response.Messages = result.Message;
                }

                response.Model = result.Data;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Messages = "Ha ocurrido un error obteniendo amigos.";
                _logger.LogError(response.Messages, ex.ToString());
            }
            return response;
        }


        public async Task<ServiceResponse> RemoveAsync(AmigosDto dto)
        {
            ServiceResponse response = new ServiceResponse();

            try
            {
                Amigos amigos = new Amigos();

                amigos.AmigoID = dto.AmigoID;

                var result = await _amigosRepository.Remove(amigos);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Messages = "Ha ocurrido un error guardando el estado.";
                _logger.LogError(response.Messages, ex.ToString());
            }
            return response;
        }

        public async Task<ServiceResponse> SaveAsync(AmigosDto dto)
        {
            ServiceResponse response = new ServiceResponse();

            try
            {
                dto.UsuarioID1 = authentication.Id;
                var amigos = _mapper.Map<Amigos>(dto);
                var result = await _amigosRepository.Save(amigos);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Messages = "Ha ocurrido un error guardando el amigo.";
                _logger.LogError(response.Messages, ex.ToString());
            }
            return response;
        }


        public async Task<ServiceResponse> UpdateAsync(AmigosDto dto)
        {
            ServiceResponse response = new ServiceResponse();

            try
            {
                var resultGetBy = await _amigosRepository.GetById(dto.AmigoID);

                if (!resultGetBy.Success)
                {
                    response.IsSuccess = resultGetBy.Success;
                    response.Messages = resultGetBy.Message;

                    return response;
                }

                var amigos = _mapper.Map<Amigos>(dto);
                var result = await _amigosRepository.Update(amigos);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Messages = "Ha ocurrido un error actualizando el amigo.";
                _logger.LogError(response.Messages, ex.ToString());
            }
            return response;
        }
    }
}

