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
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task SendEmailAsync(string toEmail,string subject,string body)
        {
            var host = _configuration["EmailSettings:Host"];
            var port = int.Parse(_configuration["EmailSettings:Port"]!);
            var username = _configuration["EmailSettings:Username"];
            var password = _configuration["EmailSettings:Password"];
            var fromEmail = _configuration["EmailSettings:FromEmail"];
            var fromName = _configuration["EmailSettings:FromName"];

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
