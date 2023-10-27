using AnimalCard.Application.Owner.Queries.GetOwners;
using AnimalCard.Decryption;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalCard.Application.Pet.Queries.GetPetsByUserRole
{
    public class GetPetsByUserRoleQueryHandler : IRequestHandler<GetPetsByUserRoleQuery, UserPetsVm>
    {
        public async Task<UserPetsVm> Handle(GetPetsByUserRoleQuery request, CancellationToken cancellationToken)
        {
            const string PROCEDURE_NAME = "[dbo].[GetPetsByUserRole]";
            UserPetsVm userPets = new UserPetsVm();
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionStrings.Database))
            {
                await sqlConnection.OpenAsync();

                using (SqlCommand sqlCommand = new SqlCommand(PROCEDURE_NAME, sqlConnection) { CommandType = CommandType.StoredProcedure })
                {
                    sqlCommand.Parameters.Add("@role", SqlDbType.NVarChar).Value = request.Role;
                    sqlCommand.Parameters.Add("@userId", SqlDbType.Int).Value = request.UserId;
                    using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                    {
                        while (sqlDataReader.Read())
                        {
                            UserPetDTO userPet = new UserPetDTO();
                            userPet.Id = sqlDataReader.GetInt32("Id");
                            userPet.Photo = sqlDataReader.GetString("Photo");
                            userPet.Name = sqlDataReader.GetString("Name");

                            userPets.UserPets.Add(userPet);
                        }
                    }
                }
                await sqlConnection.CloseAsync();
            }
            return userPets;
        }
    }
}
