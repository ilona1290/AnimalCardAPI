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

namespace AnimalCard.Application.Owner.Queries.GetOwners
{
    public class GetOwnersQueryHandler : IRequestHandler<GetOwnersQuery, OwnersVm>
    {
        public async Task<OwnersVm> Handle(GetOwnersQuery request, CancellationToken cancellationToken)
        {
            const string PROCEDURE_NAME = "[dbo].[GetOwners]";
            OwnersVm owners = new OwnersVm();
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionStrings.Database))
            {
                await sqlConnection.OpenAsync();

                using (SqlCommand sqlCommand = new SqlCommand(PROCEDURE_NAME, sqlConnection) { CommandType = CommandType.StoredProcedure })
                {
                    using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                    {
                        while (sqlDataReader.Read())
                        {
                            OwnerDTO owner = new OwnerDTO();
                            owner.Id = sqlDataReader.GetInt32("Id");
                            owner.FullName = sqlDataReader.GetString("Name") + " " + sqlDataReader.GetString("Surname");
                            owner.Email = sqlDataReader.GetString("Email");
                            owner.PhoneNumber = sqlDataReader.GetString("PhoneNumber");
                            owner.ProfilePicture = sqlDataReader.GetString("ProfilePicture");
                            
                            owners.Owners.Add(owner);
                        }
                    }
                }
                await sqlConnection.CloseAsync();
            }
            return owners;
        }
    }
}
