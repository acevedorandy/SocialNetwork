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
    public sealed class PublicacionesRepository(SocialNetworkContext socialNetworkContext, IdentityContext identityContext,
                                        ILogger<PublicacionesRepository> logger, PublicacionesValidations publicacionesValidations) : BaseRepository<Publicaciones>(socialNetworkContext), IPublicacionesRepository
    {
        private readonly SocialNetworkContext _socialNetworkContext = socialNetworkContext;
        private readonly ILogger<PublicacionesRepository> _logger = logger;
        private readonly PublicacionesValidations _publicacionesValidations = publicacionesValidations;
        private readonly IdentityContext _identityContext = identityContext;

        public async override Task<OperationResult> Save(Publicaciones publicaciones)
        {
            OperationResult result = new OperationResult();

            _publicacionesValidations.ValidateSave(publicaciones);

            try
            {
                result = await base.Save(publicaciones);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Ha ocurrido un error guardando la publicacion.";
                _logger.LogError(result.Message, ex.ToString());
            }
            return (result);
        }

        public async override Task<OperationResult> Update(Publicaciones publicaciones)
        {
            OperationResult result = new OperationResult();

            _publicacionesValidations.ValidateUpdate(publicaciones);

            try
            {
                Publicaciones? publicacionesToUpdate = await _socialNetworkContext.Publicaciones.FindAsync(publicaciones.PublicacionID);

                publicacionesToUpdate.PublicacionID = publicaciones.PublicacionID;
                publicacionesToUpdate.UsuarioID = publicaciones.UsuarioID;
                publicacionesToUpdate.Contenido = publicaciones.Contenido;
                publicacionesToUpdate.FechaPublicacion = publicaciones.FechaPublicacion;
                publicacionesToUpdate.Imagen = publicaciones.Imagen;
                publicacionesToUpdate.Video = publicaciones.Video;

                result = await base.Update(publicacionesToUpdate);

            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Ha ocurrido un error actualizando la publicacion.";
                _logger.LogError(result.Message, ex.ToString());
            }
            return (result);
        }

        public async override Task<OperationResult> Remove(Publicaciones publicaciones)
        {
            OperationResult result = new OperationResult();

            _publicacionesValidations.ValidateRemove(publicaciones);

            try
            {
                result = await base.Remove(publicaciones);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Ha ocurrido un error eliminando la publicacion.";
                _logger.LogError(result.Message, ex.ToString());
            }
            return (result);
        }

        public async override Task<OperationResult> GetAll()
        {
            OperationResult result = new OperationResult();

            try
            {
                var publicaciones = await _socialNetworkContext.Publicaciones
                    .OrderByDescending(dbo => dbo.PublicacionID)
                    .ToListAsync();

                var usuarios = await _identityContext.Users
                    .ToListAsync();

                var datos = (from dbo in publicaciones
                             join dbol in usuarios on dbo.UsuarioID.ToString() equals dbol.Id.ToString()
                             select new PublicacionesModel
                             {
                                 PublicacionID = dbo.PublicacionID,
                                 UsuarioID = dbol.Id.ToString(),
                                 Contenido = dbo.Contenido,
                                 FechaPublicacion = dbo.FechaPublicacion,
                                 Imagen = dbo.Imagen,
                                 Video = dbo.Video
                             }).ToList();

                result.Data = datos;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Ha ocurrido un error obteniendo las publicaciones.";
                _logger.LogError(result.Message, ex.ToString());
            }

            return result;
        }


        public async override Task<OperationResult> GetById(int id)
        {
            OperationResult result = new OperationResult();

            try
            {
                var publicaciones = await _socialNetworkContext.Publicaciones
                    .OrderByDescending(dbo => dbo.PublicacionID)
                    .ToListAsync();

                var usuarios = await _identityContext.Users
                    .ToListAsync();

                var datos = (from dbo in publicaciones
                             join dbol in usuarios on dbo.UsuarioID.ToString() equals dbol.Id.ToString()

                             where dbo.PublicacionID == id

                             select new PublicacionesModel
                             {
                                 PublicacionID = dbo.PublicacionID,
                                 UsuarioID = dbol.Id.ToString(),
                                 Contenido = dbo.Contenido,
                                 FechaPublicacion = dbo.FechaPublicacion,
                                 Imagen = dbo.Imagen,
                                 Video = dbo.Video
                             }).FirstOrDefault();

                result.Data = datos;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Ha ocurrido un error obteniendo las publicaciones.";
                _logger.LogError(result.Message, ex.ToString());
            }

            return result;
        }

        public async Task<OperationResult> PostByFriends(string userId)
        {
            OperationResult result = new OperationResult();

            try
            {
                var amigos = await _socialNetworkContext.Amigos
                    .Where(a => a.UsuarioID1 == userId || a.UsuarioID2 == userId)
                    .Select(a => a.UsuarioID1 == userId ? a.UsuarioID2 : a.UsuarioID1)
                    .ToListAsync();

                // Obtener los usuarios (esto se hace con el otro contexto)
                var usuarios = await _identityContext.Users.ToListAsync();

                // Traer las publicaciones de los amigos
                var publicaciones = await _socialNetworkContext.Publicaciones
                    .Where(p => amigos.Contains(p.UsuarioID))
                    .OrderByDescending(p => p.FechaPublicacion)
                    .ToListAsync();

                // Realizar el join en memoria
                var publicacionesConUsuarios = publicaciones
                    .Join(usuarios, p => p.UsuarioID, u => u.Id, (p, u) => new PublicacionesModel
                    {
                        PublicacionID = p.PublicacionID,
                        UsuarioID = p.UsuarioID,
                        Usuario = u.UserName,  // Asignar el nombre de usuario
                        Foto = u.Foto,        // Asignar la foto del usuario
                        Contenido = p.Contenido,
                        FechaPublicacion = p.FechaPublicacion,
                        Imagen = p.Imagen,
                        Video = p.Video
                    }).ToList();

                // Asignar el resultado
                result.Data = publicacionesConUsuarios;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Ha ocurrido un error obteniendo las publicaciones de tus amigos.";
                _logger.LogError(result.Message, ex.ToString());
            }
            return result;
        }

        public async Task<OperationResult> MyPost(string userId)
        {
            OperationResult result = new OperationResult();

            try
            { 
                var getUser = await _identityContext.Users
                    .Where(u => u.Id == userId)
                    .Select(u => u.Id)
                    .FirstOrDefaultAsync();

                var publicaciones = await _socialNetworkContext.Publicaciones
                    .Where(p => p.UsuarioID == getUser)
                    .ToListAsync();

                var datos = (from p in publicaciones
                             join u in _identityContext.Users on p.UsuarioID equals u.Id
                             select new MyPostViewModel
                             {
                                 PublicacionID = p.PublicacionID,
                                 Usuario = u.UserName,
                                 FotoUsuario = u.Foto,
                                 Contenido = p.Contenido,
                                 FechaPublicacion = p.FechaPublicacion,
                                 Imagen = p.Imagen,
                                 Video = p.Video
                             })
                             .OrderByDescending(p => p.FechaPublicacion)  
                             .ToList();

                result.Data = datos;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Ha ocurrido un error obteniendo las publicaciones del usuario.";
                _logger.LogError(result.Message, ex.ToString());
            }
            return result;
        }
    }
}
