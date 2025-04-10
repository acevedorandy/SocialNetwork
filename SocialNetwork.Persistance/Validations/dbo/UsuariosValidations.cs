

using SocialNetwork.Domain.Entities.dbo;
using SocialNetwork.Domain.Result;
using SocialNetwork.Persistance.Validations.BaseValidation;

namespace SocialNetwork.Persistance.Validations.dbo
{
    public class UsuariosValidations : IBaseValidation<Usuarios>
    {
        public OperationResult ValidateRemove(Usuarios entity)
        {
            OperationResult result = new OperationResult();

            if (entity == null || entity.UsuarioID < 0)
            {
                result.Success = false;
                result.Message = "El usuario es requerido.";
                return result;
            }
            return result;
        }

        public OperationResult ValidateSave(Usuarios entity)
        {
            OperationResult result = new OperationResult();

            if (string.IsNullOrEmpty(entity.Nombre) || entity.Nombre.Length > 50 ||
                string.IsNullOrEmpty(entity.Apellido) || entity.Apellido.Length > 50)
            {
                result.Success = false;
                result.Message = "El nombre y apellido son requeridos y deben ser menor a 50 caracteres.";
                return result;
            }
            if (string.IsNullOrEmpty(entity.Telefono) || entity.Telefono.Length > 15 )
            {
                result.Success = false;
                result.Message = "El telefono es requerido y debe ser menor a 15 caracteres.";
                return result;
            }
            if (string.IsNullOrEmpty(entity.Correo) || entity.Correo.Length > 100)
            {
                result.Success = false;
                result.Message = "El correo es requerido y debe ser menor a 100 caracteres.";
                return result;
            }
            if (string.IsNullOrEmpty(entity.NombreUsuario) || entity.NombreUsuario.Length > 50)
            {
                result.Success = false;
                result.Message = "El nombre de usuario es requerido y debe ser menor a 50 caracteres.";
                return result;
            }
            if (string.IsNullOrEmpty(entity.Contraseña) || entity.Contraseña.Length > 255)
            {
                result.Success = false;
                result.Message = "La contraseña es requerido y debe ser menor a 2555 caracteres.";
                return result;
            }
            return result;
        }

        public OperationResult ValidateUpdate(Usuarios entity)
        {
            OperationResult result = new OperationResult();

            if (entity == null || entity.UsuarioID < 0 )
            {
                result.Success = false;
                result.Message = "El ID del usuario es requerido.";
                return result;
            }
            if (string.IsNullOrEmpty(entity.Nombre) || entity.Nombre.Length > 50 ||
                string.IsNullOrEmpty(entity.Apellido) || entity.Apellido.Length > 50)
            {
                result.Success = false;
                result.Message = "El nombre y apellido son requeridos y deben ser menor a 50 caracteres.";
                return result;
            }
            if (string.IsNullOrEmpty(entity.Telefono) || entity.Telefono.Length > 15)
            {
                result.Success = false;
                result.Message = "El telefono es requerido y debe ser menor a 15 caracteres.";
                return result;
            }
            if (string.IsNullOrEmpty(entity.Correo) || entity.Correo.Length > 100)
            {
                result.Success = false;
                result.Message = "El correo es requerido y debe ser menor a 100 caracteres.";
                return result;
            }
            if (string.IsNullOrEmpty(entity.NombreUsuario) || entity.NombreUsuario.Length > 50)
            {
                result.Success = false;
                result.Message = "El nombre de usuario es requerido y debe ser menor a 50 caracteres.";
                return result;
            }
            if (string.IsNullOrEmpty(entity.Contraseña) || entity.Contraseña.Length > 255)
            {
                result.Success = false;
                result.Message = "La contraseña es requerido y debe ser menor a 2555 caracteres.";
                return result;
            }
            return result;
        }
    }
}
