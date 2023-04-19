using System.Net;
using System.Net.Mail;
using WebLuto.Models;
using WebLuto.Models.Enums;
using WebLuto.Security;
using WebLuto.Services.Interfaces;
using WebLuto.Utils;
using WebLuto.Utils.Messages;

namespace WebLuto.Services
{
    public class EmailService : IEmailService
    {
        public void SendEmail(Client client, EmailTemplateType emailTemplateType, string token = null)
        {
            try
            {
                Settings settings = new Settings();
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
                smtp.Credentials = new NetworkCredential(settings.EmailContact, settings.EmailPassword);
                smtp.EnableSsl = true;

                smtp.Send(mailMessage);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format(EmailMsg.EXC0003, ex.Message));
            }
        }

        public string GetEmailTemplateType(string clientName, EmailTemplateType templateType, string token = null)
        {
            try
            {
                string emailBody;
                string title = UtilityMethods.GetEnumDescription(templateType);
                string confirmationLink = new Settings().DefaultUrlApi + "api/confirmAccount/" + token;

                switch (templateType)
                {
                    case EmailTemplateType.EmailConfirmation:
                        emailBody = $@"<!DOCTYPE html> <html>   <head>     <meta charset=""utf-8"">     <title>{title}</title>     <!-- Inclusão da biblioteca Tailwind CSS -->     <link href=""https://cdn.jsdelivr.net/npm/tailwindcss@2.2.7/dist/tailwind.min.css"" rel=""stylesheet"">     <style>       /* Estilos personalizados */       .brand-color {{         color: #FFFFFF;       }}       .gradient-bg {{         background: linear-gradient(90deg, #1A202C 0%, #4A5568 100%);       }}       .confirmation-icon {{         background: #FFFFFF;         color: #4A5568;       }}       .confirmation-icon svg {{         width: 3rem;         height: 3rem;       }}       .text-gradient {{         background: -webkit-linear-gradient(#718096, #1A202C);         -webkit-background-clip: text;         -webkit-text-fill-color: transparent;       }}       .button:focus {{         outline: none !important;         box-shadow: none !important;       }}     </style>   </head>   <body class=""bg-gray-100"">     <div class=""h-screen flex flex-col justify-center items-center gradient-bg"">       <div class=""w-full max-w-md p-8 rounded-lg shadow-lg bg-white"">         <div class=""flex items-center justify-center confirmation-icon mx-auto rounded-full w-16 h-16 mb-8"">           <svg xmlns=""http://www.w3.org/2000/svg"" viewBox=""0 0 20 20"" fill=""currentColor"">             <path fill-rule=""evenodd"" d=""M18.707,4.293c0.391,0.391,0.391,1.023,0,1.414L8.414,16H7V14.586l9.293-9.293 C17.488,3.902,18.319,3.902,18.707,4.293z M10,0C4.477,0,0,4.477,0,10s4.477,10,10,10s10-4.477,10-10S15.523,0,10,0z""/>           </svg>         </div>  <h1 class=""text-4xl text-center font-bold text-gradient mb-4"">Confirmação de E-mail</h1>         <p class=""text-lg text-center text-gray-00 mb-6"">Olá, {clientName}!</p>         <p class=""text-md text-center text-gray-400 mb-8"">Para confirmar seu endereço de e-mail, por favor clique no link abaixo:</p>         <div class=""flex justify-center mb-8"">           <a href=""{confirmationLink}"" class=""button bg-blue-700 hover:bg-blue-900 text-white font-bold py-2 px-4 rounded-full"">Confirmar E-mail</a>         </div>         <p class=""text-md text-center text-gray-400 mb-8"">Se você não se cadastrou em nosso site, por favor ignore este e-mail.</p>         <p class=""text-md text-center text-gray-400 mb-8"">Obrigado!</p>       </div>       <p class=""text-md text-gray-300 mt-8"">&copy; 2023 Web Luto. Todos os direitos reservados.</p>     </div>   </body> </html>";
                        break;

                    case EmailTemplateType.AccountDeletion:
                        emailBody = $@"<!DOCTYPE html> <html>   <head>     <meta charset=""utf-8"">     <title>{title}</title>     <!-- Inclusão da biblioteca Tailwind CSS -->     <link href=""https://cdn.jsdelivr.net/npm/tailwindcss@2.2.7/dist/tailwind.min.css"" rel=""stylesheet"">     <style>       /* Estilos personalizados */       .brand-color {{         color: #FFFFFF;       }}       .gradient-bg {{         background: linear-gradient(90deg, #1A202C 0%, #4A5568 100%);       }}       .confirmation-icon {{         background: #FFFFFF;         color: #4A5568;       }}       .confirmation-icon svg {{         width: 3rem;         height: 3rem;       }}       .text-gradient {{         background: -webkit-linear-gradient(#718096, #1A202C);         -webkit-background-clip: text;         -webkit-text-fill-color: transparent;       }}       .button:focus {{         outline: none !important;         box-shadow: none !important;       }}     </style>   </head>   <body class=""bg-gray-100"">     <div class=""h-screen flex flex-col justify-center items-center gradient-bg"">       <div class=""w-full max-w-md p-8 rounded-lg shadow-lg bg-white"">         <div class=""flex items-center justify-center confirmation-icon mx-auto rounded-full w-16 h-16 mb-8"">           <svg xmlns=""http://www.w3.org/2000/svg"" viewBox=""0 0 20 20"" fill=""currentColor"">             <path fill-rule=""evenodd"" d=""M18.707,4.293c0.391,0.391,0.391,1.023,0,1.414L8.414,16H7V14.586l9.293-9.293 C17.488,3.902,18.319,3.902,18.707,4.293z M10,0C4.477,0,0,4.477,0,10s4.477,10,10,10s10-4.477,10-10S15.523,0,10,0z""/>           </svg>         </div>         <h1 class=""text-4xl text-center font-bold text-gradient mb-4"">Exclusão de conta</h1>         <p class=""text-lg text-center text-gray-00 mb-6"">Lamentamos que tenha optado por excluir sua conta.</p>         <p class=""text-md text-center text-gray-400 mb-8"">Esperamos que tenha tido uma boa experiência conosco e que possamos atendê-lo novamente no futuro.</p>         <div class=""flex justify-center"">           <a href=""https://webluto.azurewebsites.net/"" class=""button bg-blue-700 hover:bg-blue-900 text-white font-bold py-2 px-4 rounded-full"">Voltar para a página inicial</a>         </div>       </div>       <p class=""text-md text-gray-300 mt-8"">&copy; 2023 Web Luto. Todos os direitos reservados.</p>     </div>   </body> </html>";
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

                    case EmailTemplateType.ConfirmAccountCreation:
                        emailBody = $@"<!DOCTYPE html><html><head><meta charset=""utf-8""><title>{title}</title><link href=""https://cdn.jsdelivr.net/npm/tailwindcss@2.2.7/dist/tailwind.min.css"" rel=""stylesheet""><style>.brand-color {{color: #FFFFFF;}}.gradient-bg {{background: linear-gradient(90deg, #1A202C 0%, #4A5568 100%);}}.confirmation-icon {{background: #FFFFFF;color: #4A5568;}}.confirmation-icon svg {{width: 3rem;height: 3rem;}}.text-gradient {{background: -webkit-linear-gradient(#718096, #1A202C);-webkit-background-clip: text;-webkit-text-fill-color: transparent;}}.button:focus {{outline: none !important;box-shadow: none !important;}}</style></head><body class=""bg-gray-100""><div class=""h-screen flex flex-col justify-center items-center gradient-bg""><div class=""w-full max-w-md p-8 rounded-lg shadow-lg bg-white""><div class=""flex items-center justify-center confirmation-icon mx-auto rounded-full w-16 h-16 mb-8""><svg xmlns=""http://www.w3.org/2000/svg"" viewBox=""0 0 20 20"" fill=""currentColor""><path fill-rule=""evenodd"" d=""M18.707,4.293c0.391,0.391,0.391,1.023,0,1.414L8.414,16H7V14.586l9.293-9.293 C17.488,3.902,18.319,3.902,18.707,4.293z M10,0C4.477,0,0,4.477,0,10s4.477,10,10,10s10-4.477,10-10S15.523,0,10,0z""/></svg></div><h1 class=""text-4xl text-center font-bold text-gradient mb-4"">Confirmação de conta</h1><p class=""text-lg text-center text-gray-00 mb-6"">Parabéns! Sua conta foi confirmada com sucesso.</p><p class=""text-md text-center text-gray-400 mb-8"">Agora você pode acessar nossa plataforma e aproveitar todos os recursos que oferecemos.</p><div class=""flex justify-center""><a href=""https://webluto.azurewebsites.net/swagger/index.html"" class=""button bg-blue-700 hover:bg-blue-900 text-white font-bold py-2 px-4 rounded-full"">Acessar plataforma</a></div></div><p class=""text-md text-gray-300 mt-8"">&copy; 2023 Web Luto. Todos os direitos reservados.</p></div></body></html>";
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
            catch (Exception ex)
            {
                throw new Exception(string.Format(EmailMsg.EXC0004, ex.Message));
            }
        }
    }
}
