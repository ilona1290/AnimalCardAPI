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
using AnimalCard.Application.Owner.Queries.GetOwners;
using AnimalCard.Application.Pet.Queries.GetPetsByUserRole;

namespace AnimalCard.Application.Visit.Query.GetDataToNewVisit
{
    public class GetDataToNewVisitQueryHandler : IRequestHandler<GetDataToNewVisitQuery, DataToNewVisitVm>
    {
        public async Task<DataToNewVisitVm> Handle(GetDataToNewVisitQuery request, CancellationToken cancellationToken)
        {
            const string PROCEDURE_NAME = "[dbo].[GetDataToNewVisit]";
            int currentOwnerId = 0;
            OwnerWithPetsDTO currentOwner = new OwnerWithPetsDTO();
            DataToNewVisitVm dataToNewVisit = new DataToNewVisitVm();
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionStrings.Database))
            {
                await sqlConnection.OpenAsync();

                using (SqlCommand sqlCommand = new SqlCommand(PROCEDURE_NAME, sqlConnection) { CommandType = System.Data.CommandType.StoredProcedure })
                {
                    sqlCommand.Parameters.Add("@vetId", SqlDbType.NVarChar).Value = request.VetId;
                    using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                    {
                        while (sqlDataReader.Read())
                        {
                            dataToNewVisit.VisitTypes.Add(new VisitTypeDTO
                            {
                                Id = sqlDataReader.GetInt32("Id"),
                                Name = sqlDataReader.GetString("Name")
                            });
                        }

                        if (sqlDataReader.NextResult())
                        {
                            while (sqlDataReader.Read())
                            {
                                dataToNewVisit.Owners.Add(new OwnerWithPetsDTO
                                {
                                    Id = sqlDataReader.GetInt32("Id"),
                                    FullName = sqlDataReader.GetString("FullName")
                                });
                            }
                        }
                        if (sqlDataReader.NextResult())
                        {
                            while (sqlDataReader.Read())
                            {
                                if(currentOwnerId != sqlDataReader.GetInt32("OwnerId"))
                                {
                                    currentOwnerId = sqlDataReader.GetInt32("OwnerId");
                                    currentOwner = dataToNewVisit.Owners.First(o => o.Id == currentOwnerId);
                                }
                                currentOwner?.Pets.Add(new UserPetDTO
                                {
                                    Id = sqlDataReader.GetInt32("Id"),
                                    Name = sqlDataReader.GetString("Name"),
                                    Photo = sqlDataReader.GetString("Photo")
                                });
                                
                            }

                        }

                        if (sqlDataReader.NextResult())
                        {
                            while (sqlDataReader.Read())
                            {
                                dataToNewVisit.DisabledTerms.Add(new DisabledTermDTO
                                {
                                    StartDate = sqlDataReader.GetDateTime("StartDate"),
                                    EndDate = sqlDataReader.GetDateTime("EndDate")
                                });

                            }
                        }
                    }
                }
                await sqlConnection.CloseAsync();
            }
            return dataToNewVisit;
        }
    }
}
