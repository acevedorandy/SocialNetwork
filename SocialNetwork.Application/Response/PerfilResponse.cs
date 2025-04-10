
using SocialNetwork.Application.Response.Base;

namespace SocialNetwork.Application.Response
{
    public class PerfilResponse : BaseResponse
    {
        public PerfilResponse()
        {
            HasError = false;
        }

        public dynamic Data { get; set; }
    }
}
