using System.Net;
using System.Net.Mail;
using WebLuto.Models;
using WebLuto.Models.Enums;
using WebLuto.Security;
using WebLuto.Services.Interfaces;
using WebLuto.Utils;

namespace WebLuto.Services
{
    public class EmailService : IEmailService
    {
        public void SendEmail(Client client, EmailTemplateType emailTemplateType, string token = null)
        {
            string emailDestination = client.Email;
            string clientName = client.FirstName + " " + client.LastName;

            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(new Settings().EmailContact);
            mailMessage.To.Add(new MailAddress(emailDestination, clientName));
            mailMessage.Subject = UtilityMethods.GetEnumDescription(emailTemplateType);
            mailMessage.Body = GetEmailTemplateType(clientName, emailTemplateType, token);
            mailMessage.IsBodyHtml = true;

            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(new Settings().EmailContact, "ipbxhgtkikkbzrxv");
            smtp.EnableSsl = true;

            smtp.Send(mailMessage);
        }

        public string GetEmailTemplateType(string clientName, EmailTemplateType templateType, string token = null)
        {
            string emailBody = "";
            string title = UtilityMethods.GetEnumDescription(templateType);
            string confirmationLink = new Settings().DefaultUrlApi + "api/confirmAccount/" + token;

            switch (templateType)
            {
                case EmailTemplateType.AccountCreation:
                    emailBody = $@"
                        <html>
                          <head>
                            <title>{title}</title>
                          </head>
                          <body>
                            <p>Olá, {clientName}!</p>
                            <p>Obrigado por se cadastrar!<br>Para confirmar seu endereço de email, por favor clique no link abaixo:</p>
                            <p><a href='{confirmationLink}'>Link de confirmação!</a></p>
                            <p>Se você não se cadastrou em nosso site, por favor ignore este email.</p>
                            <p>Obrigado!</p>
                          </body>
                        </html>";
                    break;

                case EmailTemplateType.AccountDeletion:
                    emailBody = $@"
                        <html>
                          <head>
                            <title>{title}</title>
                          </head>
                          <body>
                            <p>Olá, {clientName}!</p>
                            <p>Lamentamos informar que sua conta foi excluída.</p>
                            <p>Caso tenha sido um engano, por favor entre em contato conosco o mais breve possível para resolvermos o problema.</p>
                            <p>Obrigado!</p>
                          </body>
                        </html>";
                    break;
                case EmailTemplateType.ChangedPassword:
                    emailBody = $@"
                        <html>
                          <head>
                            <title>{title}</title>
                          </head>
                          <body>
                            <p>Olá, {clientName}!</p>
                            <p>Sua senha foi alterada com sucesso!</p>
                            <p>Caso não tenha sido você que solicitou a alteração de senha, por favor entre em contato conosco o mais breve possível para resolvermos o problema.</p>
                            <p>Obrigado!</p>
                          </body>
                        </html>";
                    break;
                case EmailTemplateType.Default:
                default:
                    emailBody = $@"
                        <html>
                          <head>
                            <title>{title}</title>
                          </head>
                          <body>
                            <p>Olá, {clientName}!</p>
                            <p>Por favor, ignore este email.</p>                            
                          </body>
                        </html>";
                    break;
            }

            return emailBody;
        }
    }
}
