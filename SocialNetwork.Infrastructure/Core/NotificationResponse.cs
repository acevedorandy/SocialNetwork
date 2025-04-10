﻿

using MailKit;

namespace SocialNetwork.Infrastructure.Core
{
    public class NotificationResponse
    {
        public NotificationResponse()
        {
            this.IsSuccsesfully = true;
        }
        public bool IsSuccsesfully { get; set; }
        public string? Messages { get; set; }
    }
}
