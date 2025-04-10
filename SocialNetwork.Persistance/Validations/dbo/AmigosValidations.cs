using SocialNetwork.Domain.Entities.dbo;
using SocialNetwork.Domain.Result;
using SocialNetwork.Persistance.Validations.BaseValidation;


namespace SocialNetwork.Persistance.Validations.dbo
{
    public class AmigosValidations : IBaseValidation<Amigos>
    {
        public OperationResult ValidateRemove(Amigos entity)
        {
            OperationResult result = new OperationResult();

            if (entity == null || entity.AmigoID < 0)
            {
                result.Success = false;
                result.Message = "El amigo es requerido.";
                return result;
            }
            return result;
        }

        public OperationResult ValidateSave(Amigos entity)
        {
            OperationResult result = new OperationResult();

            if (entity == null)
            {
                result.Success = false;
                result.Message = "El amigo es requerido.";
                return result;
            }
            if (string.IsNullOrEmpty(entity.UsuarioID1) || (string.IsNullOrEmpty(entity.UsuarioID2)))
            {
                result.Success = false;
                result.Message = "El ID de los amigos es requerido.";
                return result;
            }
            return result;
        }

        public OperationResult ValidateUpdate(Amigos entity)
        {
            throw new NotImplementedException();
        }
    }
}
