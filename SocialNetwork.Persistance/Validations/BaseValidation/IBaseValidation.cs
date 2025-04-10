

using SocialNetwork.Domain.Result;

namespace SocialNetwork.Persistance.Validations.BaseValidation
{
    public interface IBaseValidation<T>
    {
        OperationResult ValidateSave(T entity);
        OperationResult ValidateUpdate(T entity);
        OperationResult ValidateRemove(T entity);
    }
}
