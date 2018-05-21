using Clothes.Core.Models.Email;
using System;
using System.Collections.Generic;
using System.Text;

namespace Clothes.Business.Email
{
    public interface IEmailService
    {
        void Send(EmailMessage emailMessage);
        List<EmailMessage> ReceiveEmail(int maxCount = 10);
    }
}
