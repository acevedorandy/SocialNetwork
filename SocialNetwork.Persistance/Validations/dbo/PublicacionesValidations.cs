

using SocialNetwork.Domain.Entities.dbo;
using SocialNetwork.Domain.Result;
using SocialNetwork.Persistance.Validations.BaseValidation;

namespace SocialNetwork.Persistance.Validations.dbo
{
    public class PublicacionesValidations : IBaseValidation<Publicaciones>
    {
        public OperationResult ValidateRemove(Publicaciones entity)
        {
            OperationResult result = new OperationResult();

            if (entity == null || entity.PublicacionID < 0)
            {
                result.Success = false;
                result.Message = "La publicacion es requerida.";
                return result;
            }
            return result;
        }

        public OperationResult ValidateSave(Publicaciones entity)
        {
            OperationResult result = new OperationResult();

            if (entity == null || (string.IsNullOrEmpty(entity.UsuarioID)))
            {
                result.Success = false;
                result.Message = "El usuario es requerido.";
                return result;
            }
            if (string.IsNullOrEmpty(entity.Contenido))
            {
                result.Success = false;
                result.Message = "El contenido es requerido.";
                return result;
            }
            return result;
        }

        public OperationResult ValidateUpdate(Publicaciones entity)
        {
            OperationResult result = new OperationResult();

            if (entity == null || entity.PublicacionID < 0)
            {
                result.Success = false;
                result.Message = "El ID de la publicacion es requerido.";
                return result;
            }
            if (string.IsNullOrEmpty(entity.UsuarioID))
            {
                result.Success = false;
                result.Message = "El ID del usuario es requerido.";
                return result;
            }
            if (string.IsNullOrEmpty(entity.Contenido))
            {
                result.Success = false;
                result.Message = "El contenido no puede quedar vacio.";
                return result;
            }
            return result;
        }
    }
}
