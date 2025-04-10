

//using SocialNetwork.Application.Dtos.email;
using SocialNetwork.Infrastructure.Core;
using SocialNetwork.Infrastructure.dto;
using SocialNetwork.Infrastructure.Settings;

namespace SocialNetwork.Infrastructure.Interfaces
{
    public interface IEmailService
    {
        public MailSettings MailSettings { get; }
        Task <NotificationResponse> SendEmailAsync(EmailRequest emailRequest);
    }
}
