using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InnoShop.UserService.Application.Services;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MailKit;
using MimeKit;

namespace InnoShop.UserService.Infrastructure.Services
{
    public class EmailService(IConfiguration configuration) : IEmailService
    {
        public async Task SendEmailAsync(string toEmail,string subject,string body)
        {
            var host = configuration["EmailSettings:Host"];
            var port = int.Parse(configuration["EmailSettings:Port"]!);
            var username = configuration["EmailSettings:Username"];
            var password = configuration["EmailSettings:Password"];
            var fromEmail = configuration["EmailSettings:FromEmail"];
            var fromName = configuration["EmailSettings:FromName"];

            var email = new MimeMessage();
            email.From.Add(new MailboxAddress(fromName, fromEmail!));
            email.To.Add(new MailboxAddress("", toEmail));
            email.Subject = subject;
            email.Body=new TextPart("html") { Text=body };

            using var smtp = new SmtpClient();
            await smtp.ConnectAsync(host, port, false);
            await smtp.AuthenticateAsync(username, password);
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
        }
    }
   
}
