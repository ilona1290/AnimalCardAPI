using AnimalCard.Application.Common.Exceptions;
using AnimalCard.Application.Common.Interfaces;
using AnimalCard.Application.DTOs;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnimalCard.Decryption;

namespace AnimalCard.Application.Auth.Commands.Authorize
{
    public class AuthorizeCommandHandler : IRequestHandler<AuthorizeCommand, AuthResponseDTO>
    {
        private readonly ITokenGenerator _tokenGenerator;
        private readonly IIdentityService _identityService;

        public AuthorizeCommandHandler(IIdentityService identityService, ITokenGenerator tokenGenerator)
        {
            _identityService = identityService;
            _tokenGenerator = tokenGenerator;
        }

        public async Task<AuthResponseDTO> Handle(AuthorizeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _identityService.SigninUserAsync(request.Email, request.Password);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            //if (!result)
            //{
            //    throw new BadRequestException("Invalid username or password");
            //}

            var (userId, fullName, userName, email, roles) = await _identityService.GetUserDetailsAsync(await _identityService.GetUserIdAsync(request.Email));

            string token = _tokenGenerator.GenerateJWTToken((userId: userId, email: email, roles: roles));

            const string PROCEDURE_NAME = "[dbo].[GetUserInfo]";
            int userIdByRole = 0;
            string profilePicture = "";
            bool isCompletedVetProfile = false;
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionStrings.Database))
            {
                await sqlConnection.OpenAsync();

                using (SqlCommand sqlCommand = new SqlCommand(PROCEDURE_NAME, sqlConnection) { CommandType = System.Data.CommandType.StoredProcedure })
                {
                    sqlCommand.Parameters.Add("@email", SqlDbType.NVarChar).Value = email;
                    sqlCommand.Parameters.Add("@role", SqlDbType.NVarChar).Value = roles[0];
                    using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                    {
                        while (sqlDataReader.Read())
                        {
                            userIdByRole = sqlDataReader.GetInt32("Id");
                            profilePicture = sqlDataReader.GetString("ProfilePicture");
                            if (roles[0] == "Vet")
                            {
                                isCompletedVetProfile = sqlDataReader.GetBoolean("IsCompletedProfile");
                            }
                        }
                    }
                }
                await sqlConnection.CloseAsync();
            }
            return new AuthResponseDTO()
            {
                UserId = userIdByRole,
                FullName = fullName,
                Token = token,
                Role = roles[0],
                IsCompletedVetProfile = isCompletedVetProfile,
                ProfilePicture = profilePicture
            };
        }
    }
}
