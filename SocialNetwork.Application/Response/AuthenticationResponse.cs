﻿

using SocialNetwork.Application.Response.Base;

namespace SocialNetwork.Application.Response
{
    public class AuthenticationResponse : BaseResponse
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public List<string> Roles { get; set; }
        public bool IsVerified { get; set; }

    }
}
