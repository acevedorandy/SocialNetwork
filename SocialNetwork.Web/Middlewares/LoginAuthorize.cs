﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace SocialNetwork.Web.Middlewares
{
    public class LoginAuthorize : IAsyncActionFilter
    {
        private ValidateUserSesion _userSesion;

        public LoginAuthorize(ValidateUserSesion validateUserSesion)
        {
            _userSesion = validateUserSesion;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!_userSesion.HasUser()) 
            {
                var controller = (Controller)context.Controller;
                context.Result = controller.RedirectToAction("Home", "Usuarios"); 
                return;
            }

            await next(); 
        }

    }
}
