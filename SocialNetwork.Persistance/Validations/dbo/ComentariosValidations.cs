

using SocialNetwork.Domain.Entities.dbo;
using SocialNetwork.Domain.Result;
using SocialNetwork.Persistance.Validations.BaseValidation;

namespace SocialNetwork.Persistance.Validations.dbo
{
    public class ComentariosValidations : IBaseValidation<Comentarios>
    {
        public OperationResult ValidateRemove(Comentarios entity)
        {
            OperationResult result = new OperationResult();

            if (entity == null || entity.ComentarioID < 0 )
            {
                result.Success = false;
                result.Message = "El comentario es requerido.";
                return result;
            }
            return result;
        }

        public OperationResult ValidateSave(Comentarios entity)
        {
            OperationResult result = new OperationResult();

            if (entity == null || entity.PublicacionID < 0)
            {
                result.Success = false;
                result.Message = "La publicacion es requerida.";
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

        public OperationResult ValidateUpdate(Comentarios entity)
        {
            OperationResult result = new OperationResult();

            if (entity == null || entity.ComentarioID < 0)
            {
                result.Success = false;
                result.Message = "El ID del comentario es requerido.";
                return result;
            }
            return result;
        }
    }
}
