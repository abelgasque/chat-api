using Microsoft.Extensions.Options;
using SecurityApp.Web.Infrastructure.Entities.Models;
using SecurityApp.Web.Infrastructure.Entities.Settings;
using System.IO;
using System;
using System.Net;
using System.Net.Mail;

namespace SecurityApp.Web.Infrastructure.Helpers
{
    public class MailMessageHelper
    {
        private readonly MailMessageSettings _settings;

        public MailMessageHelper(IOptions<MailMessageSettings> settings)
        {
            _settings = settings.Value;
        }

        public void GetTemplateEmailResetPassword(string mail, string name, string password)
        {
            string template = GetTemplate("TemplateEmailResetPassword.html");
            string body = string.Format(template, name, password);
            SendEmail(mail, "Password reset", body);
        }

        private string GetTemplate(string nameTemplate)
        {
            string pathApp = Environment.CurrentDirectory;

            var pathTemplateHtml = Path.Combine(pathApp, "Templates", nameTemplate);
            string template = string.Empty;

            using (StreamReader sr = File.OpenText(pathTemplateHtml))
            {
                template = sr.ReadToEnd();
            }

            return template;
        }

        private void SendEmail(string to, string subject, string body)
        {
            using (var message = new MailMessage())
            {
                message.To.Add(to);
                message.Subject = subject;
                message.Body = body;
                message.IsBodyHtml = true;

                using (var client = new SmtpClient(_settings.Server, _settings.Port))
                {
                    client.UseDefaultCredentials = false;
                    client.Credentials = new NetworkCredential(_settings.Username, _settings.Password);
                    client.EnableSsl = _settings.EnableSsl;
                    client.Send(message);
                }
            }
        }
    }
}
