using AnimalCard.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalCard.Application.Common.Interfaces
{
    public interface IEmailService
    {
        Task<bool> SendEmailAsync(EmailRequestDTO email);
    }
}
