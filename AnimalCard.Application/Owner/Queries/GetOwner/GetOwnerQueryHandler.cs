using AnimalCard.Application.Helpers;
using AnimalCard.Application.Vet.Queries.GetVetDetails;
using AnimalCard.Decryption;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnimalCard.Application.Pet.Queries.GetPetsByUserRole;

namespace AnimalCard.Application.Owner.Queries.GetOwner
{
    public class GetOwnerQueryHandler : IRequestHandler<GetOwnerQuery, OwnerVm>
    {
        public async Task<OwnerVm> Handle(GetOwnerQuery request, CancellationToken cancellationToken)
        {
            const string PROCEDURE_NAME = "[dbo].[GetOwner]";
            OwnerVm owner = new OwnerVm();
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionStrings.Database))
            {
                await sqlConnection.OpenAsync();

                using (SqlCommand sqlCommand = new SqlCommand(PROCEDURE_NAME, sqlConnection) { CommandType = System.Data.CommandType.StoredProcedure })
                {
                    sqlCommand.Parameters.Add("@ownerId", SqlDbType.NVarChar).Value = request.OwnerId;
                    using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                    {
                        while (sqlDataReader.Read())
                        {
                            owner.Id = sqlDataReader.GetInt32("Id");
                            owner.Name = sqlDataReader.GetString("Name");
                            owner.Surname = sqlDataReader.GetString("Surname");
                            owner.Email = sqlDataReader.GetString("Email");
                            owner.PhoneNumber = sqlDataReader.GetString("PhoneNumber");
                            owner.ProfilePicture = sqlDataReader.GetString("ProfilePicture");
                        }

                        if (sqlDataReader.NextResult())
                        {
                            while (sqlDataReader.Read())
                            {
                                owner.Pets.UserPets.Add(new UserPetDTO
                                {
                                    Id = sqlDataReader.GetInt32("Id"),
                                    Photo = sqlDataReader.GetString("Photo"),
                                    Name = sqlDataReader.GetString("Name"),
                                });
                            }
                        }
                    }
                }
                await sqlConnection.CloseAsync();
            }
            return owner;
        }
    }
}
