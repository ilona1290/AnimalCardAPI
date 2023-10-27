using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalCard.Decryption
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDecryption(this IServiceCollection services,
            IConfiguration configuration)
        {
            ConnectionStrings.Database = Decrypt.DecryptString(configuration.GetConnectionString("AnimalCardDatabase"));
            ConnectionStrings.Azure = Decrypt.DecryptString(configuration.GetConnectionString("AzureConnectionString"));
            ConnectionStrings.SendGrid = Decrypt.DecryptString(configuration.GetConnectionString("SendGridConnectionString"));
            return services;
        }
    }
}
