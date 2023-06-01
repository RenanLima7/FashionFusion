using WebLuto.Common.Interfaces;
using WebLuto.Models;
using WebLuto.Models.Enums;

namespace WebLuto.Services.Interfaces
{
    public interface IEmailService 
    {
        void SendEmail(Client client, EmailTemplateType emailTemplateType, string token = null);

        string GetEmailTemplateType(string clientName, EmailTemplateType templateType, string token = null);
    }
}
