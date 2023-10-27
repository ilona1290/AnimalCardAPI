using AnimalCard.Application.Common.Interfaces;
using AnimalCard.Application.DTOs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SendGrid.Helpers.Mail;
using SendGrid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnimalCard.Decryption;

namespace AnimalCard.Infrastructure.Services
{
    public class EmailService
    {
        public async Task<bool> SendEmailAsync(EmailRequestDTO email)
        {
            var sendGridApiKey = ConnectionStrings.SendGrid;

            var client = new SendGridClient(sendGridApiKey);

            var msg = MailHelper.CreateSingleEmailToMultipleRecipients(
                new EmailAddress(email.From.Item1, email.From.Item2),
                email.To.Select(x => new EmailAddress(x.Key, x.Value)).ToList(),
                email.Subject,
                email.PlainMessage,
                email.HtmlMessage);

            var response = await client.SendEmailAsync(msg);

            return response.StatusCode == System.Net.HttpStatusCode.OK ||
                response.StatusCode == System.Net.HttpStatusCode.Accepted;
        }
    }
}
