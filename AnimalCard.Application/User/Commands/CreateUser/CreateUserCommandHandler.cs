using AnimalCard.Application.Common.Interfaces;
using AnimalCard.Decryption;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace AnimalCard.Application.User.Commands.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, bool>
    {
        private readonly IIdentityService _identityService;
        public CreateUserCommandHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }
        public async Task<bool> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            string fullName = request.Name + " " + request.Surname;
            var result = await _identityService.CreateUserAsync(request.Password, request.Email, fullName, request.Roles);
            const string PROCEDURE_NAME = "[dbo].[CreateOwner]";
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionStrings.Database))
            {
                await sqlConnection.OpenAsync();

                using (SqlCommand sqlCommand = new SqlCommand(PROCEDURE_NAME, sqlConnection) { CommandType = System.Data.CommandType.StoredProcedure })
                {
                    sqlCommand.Parameters.Add("@name", SqlDbType.NVarChar).Value = request.Name;
                    sqlCommand.Parameters.Add("@surname", SqlDbType.NVarChar).Value = request.Surname;
                    sqlCommand.Parameters.Add("@sex", SqlDbType.NChar).Value = request.Sex.First();
                    sqlCommand.Parameters.Add("@email", SqlDbType.NVarChar).Value = request.Email;
                    sqlCommand.Parameters.Add("@phoneNumber", SqlDbType.NVarChar).Value = request.PhoneNumber;

                    sqlCommand.ExecuteNonQuery();
                }
                await sqlConnection.CloseAsync();

            }
            return result.isSucceed;
        }
    }
}
