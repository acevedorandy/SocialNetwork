namespace SocialNetwork.Web.Helpers.perfil.Base
{
    public interface ILoadPhoto<Dto, T>
    {
        Task<Dto> LoadPhoto(Dto dto, T entity);
    }

}
