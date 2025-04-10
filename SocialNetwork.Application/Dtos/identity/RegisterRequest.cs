﻿

using System.Diagnostics.Contracts;

namespace SocialNetwork.Application.Dtos.identity
{
    public class RegisterRequest
    {
        public string Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Phone { get; set; }
        public string Foto { get; set; }

    }
}
