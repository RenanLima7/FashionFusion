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
                string baseFrontURL = "https://web-luto.vercel.app/";

                switch (templateType)
                {
                    case EmailTemplateType.EmailConfirmation:
                        emailBody = $@"
                        <!DOCTYPE html>
                        <html>
                          <head>
                            <meta charset=""utf-8"">
                            <title>{title}</title>
                            <!-- Inclusão da biblioteca Tailwind CSS -->
                            <link href=""https://cdn.jsdelivr.net/npm/tailwindcss@2.2.7/dist/tailwind.min.css"" rel=""stylesheet"">
                            <style>
                              /* Estilos personalizados */
                              .brand-color {{
                                color: #FFFFFF;
                              }}
                              .gradient-bg {{
                                background: linear-gradient(90deg, #1A202C 0%, #4A5568 100%);
                              }}
                              .confirmation-icon {{
                                background: #FFFFFF;
                                color: #4A5568;
                              }}
                              .confirmation-icon svg {{
                                width: 3rem;
                                height: 3rem;
                              }}
                              .button:focus {{
                                outline: none !important;
                                box-shadow: none !important;
                              }}
                            </style>
                          </head>
                          <body class=""bg-gray-900"">
                            <div class=""h-screen flex flex-col justify-center items-center"">
                              <div class=""w-full max-w-md p-8 rounded-lg shadow-lg bg-gray-800"">
                                <div class=""flex items-center justify-center confirmation-icon mx-auto rounded-full w-16 h-16 mb-8"">
                                  <svg xmlns=""http://www.w3.org/2000/svg"" viewBox=""0 0 20 20"" fill=""currentColor"">
                                    <path fill-rule=""evenodd"" d=""M18.707,4.293c0.391,0.391,0.391,1.023,0,1.414L8.414,16H7V14.586l9.293-9.293 C17.488,3.902,18.319,3.902,18.707,4.293z M10,0C4.477,0,0,4.477,0,10s4.477,10,10,10s10-4.477,10-10S15.523,0,10,0z""/>
                                  </svg>
                                </div>
                                <h1 class=""text-4xl text-center font-bold text-gray-100 mb-4"">Confirmação de Email</h1>
                                <p class=""text-lg text-center text-gray-100 mb-6"">Olá, {clientName}! Obrigado por se cadastrar em nossa plataforma!</p>
                                <p class=""text-md text-center text-gray-400 mb-8"">Seu código de confirmação é:</p>
                                <div class=""flex justify-center mb-8"">
                                  <h2 class=""text-4xl font-bold text-center text-gray-100"">{token}</h2>
                                </div>
                                <p class=""text-md text-center text-gray-400 mb-8"">Utilize este código para confirmar seu cadastro.</p>
                              </div>
                              <p class=""text-md text-gray-500 mt-8"">&copy; 2023 Webluto. Todos os direitos reservados.</p>
                            </div>
                          </body>
                        </html>";
                        break;

                    case EmailTemplateType.AccountDeletion:
                        emailBody = $@"
                        <!DOCTYPE html>
                        <html>
                          <head>
                            <meta charset=""utf-8"">
                            <title>{title}</title>
                            <!-- Inclusão da biblioteca Tailwind CSS -->
                            <link href=""https://cdn.jsdelivr.net/npm/tailwindcss@2.2.7/dist/tailwind.min.css"" rel=""stylesheet"">
                            <style>
                              /* Estilos personalizados */
                              .brand-color {{
                                color: #FFFFFF;
                              }}
                              .gradient-bg {{
                                background: linear-gradient(90deg, #1A202C 0%, #4A5568 100%);
                              }}
                              .confirmation-icon {{
                                background: #FFFFFF;
                                color: #4A5568;
                              }}
                              .confirmation-icon svg {{
                                width: 3rem;
                                height: 3rem;
                              }}
                              .text-gradient {{
                                background: -webkit-linear-gradient(#718096, #1A202C);
                                -webkit-background-clip: text;
                                -webkit-text-fill-color: transparent;
                              }}
                              .button:focus {{
                                outline: none !important;
                                box-shadow: none !important;
                              }}
                            </style>
                          </head>
                          <body class=""bg-gray-900"">
                            <div class=""h-screen flex flex-col justify-center items-center"">
                              <div class=""w-full max-w-md p-8 rounded-lg shadow-lg bg-gray-800"">
                                <div class=""flex items-center justify-center confirmation-icon mx-auto rounded-full w-16 h-16 mb-8"">
                                  <svg xmlns=""http://www.w3.org/2000/svg"" viewBox=""0 0 20 20"" fill=""currentColor"">
                                    <path fill-rule=""evenodd"" d=""M18.707,4.293c0.391,0.391,0.391,1.023,0,1.414L8.414,16H7V14.586l9.293-9.293 C17.488,3.902,18.319,3.902,18.707,4.293z M10,0C4.477,0,0,4.477,0,10s4.477,10,10,10s10-4.477,10-10S15.523,0,10,0z""/>
                                  </svg>
                                </div>
                                <h1 class=""text-4xl text-center font-bold text-gray-100 mb-4"">Exclusão de Conta</h1>
                                <p class=""text-lg text-center text-gray-100 mb-6"">Olá, {clientName}! É uma pena que você tenha decidido excluir sua conta na plataforma WebLuto.</p>
                                <p class=""text-md text-center text-gray-400 mb-8"">Caso queira voltar a utilizar nossos serviços, basta criar uma nova conta.</p>
                                <div class=""flex justify-center"">
                                  <a href=""{baseFrontURL}"" class=""button bg-blue-500 hover:bg-blue-600 text-white font-bold py-2 px-4 rounded-full"">Voltar à página inicial</a>
                                </div>
                              </div>
                              <p class=""text-md text-gray-500 mt-8"">&copy; 2023 Web Luto. Todos os direitos reservados.</p>
                            </div>
                          </body>
                        </html>";
                        break;

                    case EmailTemplateType.ChangedPassword:
                        emailBody = $@"Senha Alterada";
                        break;

                    case EmailTemplateType.ConfirmAccountCreation:
                        emailBody = $@"
                        <!DOCTYPE html>
                        <html>
                          <head>
                            <meta charset=""utf-8"">
                            <title>{title}</title>
                            <!-- Inclusão da biblioteca Tailwind CSS -->
                            <link href=""https://cdn.jsdelivr.net/npm/tailwindcss@2.2.7/dist/tailwind.min.css"" rel=""stylesheet"">
                            <style>
                              /* Estilos personalizados */
                              .brand-color {{
                                color: #FFFFFF;
                              }}
                              .gradient-bg {{
                                background: linear-gradient(90deg, #1A202C 0%, #4A5568 100%);
                              }}
                              .confirmation-icon {{
                                background: #FFFFFF;
                                color: #4A5568;
                              }}
                              .confirmation-icon svg {{
                                width: 3rem;
                                height: 3rem;
                              }}
                              .text-gradient {{
                                background: -webkit-linear-gradient(#718096, #1A202C);
                                -webkit-background-clip: text;
                                -webkit-text-fill-color: transparent;
                              }}
                              .button:focus {{
                                outline: none !important;
                                box-shadow: none !important;
                              }}
                            </style>
                          </head>
                          <body class=""bg-gray-900"">
                            <div class=""h-screen flex flex-col justify-center items-center"">
                              <div class=""w-full max-w-md p-8 rounded-lg shadow-lg bg-gray-800"">
                                <div class=""flex items-center justify-center confirmation-icon mx-auto rounded-full w-16 h-16 mb-8"">
                                  <svg xmlns=""http://www.w3.org/2000/svg"" viewBox=""0 0 20 20"" fill=""currentColor"">
                                    <path fill-rule=""evenodd"" d=""M18.707,4.293c0.391,0.391,0.391,1.023,0,1.414L8.414,16H7V14.586l9.293-9.293 C17.488,3.902,18.319,3.902,18.707,4.293z M10,0C4.477,0,0,4.477,0,10s4.477,10,10,10s10-4.477,10-10S15.523,0,10,0z""/>
                                  </svg>
                                </div>
                                <h1 class=""text-4xl text-center font-bold text-gray-100 mb-4"">Confirmação de Cadastro</h1>
                                <p class=""text-lg text-center text-gray-100 mb-6"">Olá, {clientName}! Sua conta foi confirmada com sucesso.</p>
                                <p class=""text-md text-center text-gray-400 mb-8"">Obrigado por escolher nossa plataforma. Comece a usá-la agora mesmo!</p>
                                <div class=""flex justify-center"">
                                  <a href=""{baseFrontURL}"" class=""button bg-blue-500 hover:bg-blue-600 text-white font-bold py-2 px-4 rounded-full"">Acessar minha conta</a>
                                </div>
                              </div>
                              <p class=""text-md text-gray-500 mt-8"">&copy; 2023 Web Luto. Todos os direitos reservados.</p>
                            </div>
                          </body>
                        </html>";
                        break;

                    case EmailTemplateType.AccountUpdate:
                        emailBody = $@"
                        <!DOCTYPE html>
                        <html>
                          <head>
                            <meta charset=""utf-8"">
                            <title>{title}</title>
                            <!-- Inclusão da biblioteca Tailwind CSS -->
                            <link href=""https://cdn.jsdelivr.net/npm/tailwindcss@2.2.7/dist/tailwind.min.css"" rel=""stylesheet"">
                            <style>
                              /* Estilos personalizados */
                              .brand-color {{
                                color: #FFFFFF;
                              }}
                              .gradient-bg {{
                                background: linear-gradient(90deg, #1A202C 0%, #4A5568 100%);
                              }}
                              .confirmation-icon {{
                                background: #FFFFFF;
                                color: #4A5568;
                              }}
                              .confirmation-icon svg {{
                                width: 3rem;
                                height: 3rem;
                              }}
                              .text-gradient {{
                                background: -webkit-linear-gradient(#718096, #1A202C);
                                -webkit-background-clip: text;
                                -webkit-text-fill-color: transparent;
                              }}
                              .button:focus {{
                                outline: none !important;
                                box-shadow: none !important;
                              }}
                            </style>
                          </head>
                          <body class=""bg-gray-900"">
                            <div class=""h-screen flex flex-col justify-center items-center"">
                              <div class=""w-full max-w-md p-8 rounded-lg shadow-lg bg-gray-800"">
                                <div class=""flex items-center justify-center confirmation-icon mx-auto rounded-full w-16 h-16 mb-8"">
                                  <svg xmlns=""http://www.w3.org/2000/svg"" viewBox=""0 0 20 20"" fill=""currentColor"">
                                    <path fill-rule=""evenodd"" d=""M18.707,4.293c0.391,0.391,0.391,1.023,0,1.414L8.414,16H7V14.586l9.293-9.293 C17.488,3.902,18.319,3.902,18.707,4.293z M10,0C4.477,0,0,4.477,0,10s4.477,10,10,10s10-4.477,10-10S15.523,0,10,0z""/>
                                  </svg>
                                </div>
                                <h1 class=""text-4xl text-center font-bold text-gray-100 mb-4"">Atualização de Conta</h1>
                                <p class=""text-lg text-center text-gray-100 mb-6"">Olá, {clientName}! Sua conta foi atualizada com sucesso!</p>
                                <p class=""text-md text-center text-gray-400 mb-8"">Obrigado por utilizar nossos serviços.</p>
                                <div class=""flex justify-center"">
                                  <a href=""{baseFrontURL}"" class=""button bg-blue-500 hover:bg-blue-600 text-white font-bold py-2 px-4 rounded-full"">Página Inicial</a>
                                </div>
                              </div>
                              <p class=""text-md text-gray-500 mt-8"">&copy; 2023 Web Luto. Todos os direitos reservados.</p>
                            </div>
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
            catch (Exception ex)
            {
                throw new Exception(string.Format(EmailMsg.EXC0004, ex.Message));
            }
        }
    }
}
