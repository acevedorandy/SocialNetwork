

using SocialNetwork.Domain.Entities.dbo;
using SocialNetwork.Domain.Result;
using SocialNetwork.Persistance.Validations.BaseValidation;

namespace SocialNetwork.Persistance.Validations.dbo
{
    public class NotificacionesValidations : IBaseValidation<Notificaciones>
    {
        public OperationResult ValidateRemove(Notificaciones entity)
        {
            OperationResult result = new OperationResult();

            if (entity == null || entity.NotificacionID < 0)
            {
                result.Success = false;
                result.Message = "La notificacion es requerida.";
                return result;
            }
            return result;
        }

        public OperationResult ValidateSave(Notificaciones entity)
        {
            OperationResult result = new OperationResult();

            if (entity == null || (string.IsNullOrEmpty(entity.UsuarioID)))
            {
                result.Success = false;
                result.Message = "El ID del usuario es requerido.";
                return result;
            }
            if (string.IsNullOrEmpty(entity.Mensaje) || entity.Mensaje.Length > 255)
            {
                result.Success = false;
                result.Message = "El mensaje no puede estar vacio y debe ser menor a 255 caracteres.";
                return result;
            }
            return result;
        }

        public OperationResult ValidateUpdate(Notificaciones entity)
        {
            OperationResult result = new OperationResult();

            if (entity == null || (string.IsNullOrEmpty(entity.UsuarioID)))
            {
                result.Success = false;
                result.Message = "El ID del usuario es requerido.";
                return result;
            }
            if (string.IsNullOrEmpty(entity.Mensaje) || entity.Mensaje.Length > 255)
            {
                result.Success = false;
                result.Message = "El mensaje no puede estar vacio y debe ser menor a 255 caracteres.";
                return result;
            }
            return result;
        }
    }
}
