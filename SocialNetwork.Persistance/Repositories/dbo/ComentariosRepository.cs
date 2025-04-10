

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
    public sealed class ComentariosRepository(SocialNetworkContext socialNetworkContext, IdentityContext identityContext,
                                       ILogger<ComentariosRepository> logger, ComentariosValidations comentariosValidations) : BaseRepository<Comentarios>(socialNetworkContext), IComentariosRepository
    {
        private readonly SocialNetworkContext _socialNetworkContext = socialNetworkContext;
        private readonly ILogger<ComentariosRepository> _logger = logger;
        private readonly ComentariosValidations _comentariosValidations = comentariosValidations;
        private readonly IdentityContext _identityContext = identityContext;

        public async override Task<OperationResult> Save(Comentarios comentarios)
        {
            OperationResult result = new OperationResult();

            _comentariosValidations.ValidateSave(comentarios);

            try
            {
                result = await base.Save(comentarios);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Ha ocurrido un error guardando el comentario.";
                _logger.LogError(result.Message, ex.ToString());
            }
            return (result);
        }

        public async override Task<OperationResult> Update(Comentarios comentarios)
        {
            OperationResult result = new OperationResult();

            _comentariosValidations.ValidateUpdate(comentarios);

            try
            {
                Comentarios? comentariosToUpdate = await _socialNetworkContext.Comentarios.FindAsync(comentarios.ComentarioID);

                comentariosToUpdate.ComentarioID = comentarios.ComentarioID;
                comentariosToUpdate.PublicacionID = comentarios.PublicacionID;
                comentariosToUpdate.UsuarioID = comentarios.UsuarioID;
                comentariosToUpdate.Contenido = comentarios.Contenido;
                comentariosToUpdate.FechaComentario = comentarios.FechaComentario;
                comentariosToUpdate.ComentarioPadreID = comentarios.ComentarioPadreID;

                result = await base.Update(comentariosToUpdate);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Ha ocurrido un error actualizando el comentario.";
                _logger.LogError(result.Message, ex.ToString());
            }
            return (result);
        }

        public async override Task<OperationResult> Remove(Comentarios comentarios)
        {
            OperationResult result = new OperationResult();

            _comentariosValidations.ValidateRemove(comentarios);
            try
            {
                result = await base.Remove(comentarios);

            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Ha ocurrido un error eliminando el comentario.";
                _logger.LogError(result.Message, ex.ToString());
            }
            return (result);
        }

        public async override Task<OperationResult> GetAll()
        {
            OperationResult result = new OperationResult();

            try
            {
                var comentarios = await _socialNetworkContext.Comentarios
                    .OrderByDescending(dbo => dbo.ComentarioID)
                    .ToListAsync();

                var publicaciones = await _socialNetworkContext.Publicaciones
                    .ToListAsync();

                var usuario = await _identityContext.Users
                    .ToListAsync();

                var datos = (from dbo in comentarios
                             join dbo1 in publicaciones on dbo.PublicacionID equals dbo1.PublicacionID
                             join dbo2 in usuario on dbo.UsuarioID.ToString() equals dbo2.Id.ToString()

                             select new ComentariosModel
                             {
                                 ComentarioID = dbo.ComentarioID,
                                 PublicacionID = dbo1.PublicacionID,
                                 UsuarioID = dbo2.Id,
                                 Usuario = dbo2.UserName,
                                 Foto = dbo2.Foto,
                                 Contenido = dbo.Contenido,
                                 FechaComentario = dbo.FechaComentario,
                                 ComentarioPadreID = dbo.ComentarioPadreID

                             }).OrderBy(d => d.FechaComentario)
                             .ToList();

                result.Data = datos;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Ha ocurrido un error obteniendo los comentarios.";
                _logger.LogError(result.Message, ex.ToString());
            }
            return (result);
        }

        public async override Task<OperationResult> GetById(int id)
        {
            OperationResult result = new OperationResult();

            try
            {
                var comentarios = await _socialNetworkContext.Comentarios
                    .OrderByDescending(dbo => dbo.ComentarioID)
                    .ToListAsync();

                var publicaciones = await _socialNetworkContext.Publicaciones
                    .ToListAsync();

                var usuario = await _identityContext.Users
                    .ToListAsync();

                var datos = (from dbo in comentarios
                             join dbo1 in publicaciones on dbo.PublicacionID equals dbo1.PublicacionID
                             join dbo2 in usuario on dbo.UsuarioID.ToString() equals dbo2.Id.ToString()

                             where dbo.ComentarioID == id

                             select new ComentariosModel
                             {
                                 ComentarioID = dbo.ComentarioID,
                                 PublicacionID = dbo1.PublicacionID,
                                 UsuarioID = dbo2.Id,
                                 Contenido = dbo.Contenido,
                                 FechaComentario = dbo.FechaComentario,
                                 ComentarioPadreID = dbo.ComentarioPadreID

                             }).FirstOrDefault();

                result.Data = datos;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Ha ocurrido un error obteniendo el comentario.";
                _logger.LogError(result.Message, ex.ToString());
            }
            return (result);
        }
    }
}
