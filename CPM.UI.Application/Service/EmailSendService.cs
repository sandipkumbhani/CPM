using AutoMapper.Configuration;
using CPM.UI.Application.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace CPM.UI.Application.Service
{
    public class EmailSendService : IEmailSendService
    {
        private readonly ILogger<EmailSendService> _logger;
        private readonly IConfiguration _configuration;
            public EmailSendService(IConfiguration configuration, ILogger<EmailSendService> logger)
            {
                _configuration = configuration;
                _logger = logger;
            }
        public void SendEmail(string Email,string pwd)
        {
            try
            {
                var fromEmail = new MailAddress(_configuration["EmailSetting:SenderEmail"], _configuration["EmailSetting:EmailHeader"]);
                var toEmail = new MailAddress(Email);
                string password = pwd;
                string body = "Email Id :" + Email.ToString();
                body += "<br>";
                body += "Password :" + password.ToString();
                Console.WriteLine(body);
                Console.WriteLine();
                var smtpClient = new SmtpClient
                {
                    Host = _configuration["EmailSetting:Host"],
                    Port = Convert.ToInt16(_configuration["EmailSetting:Port"]),
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(_configuration["EmailSetting:SenderEmail"], _configuration["EmailSetting:SenderPassword"]),
                };
                using (var mess = new MailMessage(fromEmail, toEmail)
                {
                    Subject = "CPM password",
                    Body = body,
                    IsBodyHtml = true
                })
                {
                    smtpClient.Send(mess);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }
        public static class PasswordGenerator
        {
            public static string GenerateRandomPassword(int length = 8)
            {
                const string validChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
                Random random = new Random();
                return new string(Enumerable.Repeat(validChars, length)
                    .Select(s => s[random.Next(s.Length)]).ToArray());
            }
        }
    }
}
