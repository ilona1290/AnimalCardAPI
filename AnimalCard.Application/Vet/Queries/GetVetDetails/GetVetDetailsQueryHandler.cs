using AnimalCard.Application.Helpers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnimalCard.Application.Vet.Queries.GetDiseasesAndServicesTreatments;
using AnimalCard.Decryption;

namespace AnimalCard.Application.Vet.Queries.GetVetDetails
{
    public class GetVetDetailsQueryHandler : IRequestHandler<GetVetDetailsQuery, VetVm>
    {
        public async Task<VetVm> Handle(GetVetDetailsQuery request, CancellationToken cancellationToken)
        {
            const string PROCEDURE_NAME = "[dbo].[GetVetDetails]";
            VetVm vet = new VetVm();
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
                            vet.Id = sqlDataReader.GetInt32("Id");
                            vet.Name = sqlDataReader.GetString("Name");
                            vet.Surname = sqlDataReader.GetString("Surname");
                            vet.Email = sqlDataReader.GetString("Email");
                            vet.PhoneNumber = sqlDataReader.GetString("PhoneNumber");
                            vet.ProfilePicture = sqlDataReader.GetString("ProfilePicture");
                            vet.AboutMe = sqlDataReader.GetString("AboutMe");
                        }

                        if (sqlDataReader.NextResult())
                        {
                            while (sqlDataReader.Read())
                            {
                                vet.Addresses.Add(new AddressToShowDTO
                                {
                                    Id = sqlDataReader.GetInt32("Id"),
                                    NameOfPlace = GetNullSave.SafeGetString(sqlDataReader, 1),
                                    City = sqlDataReader.GetString("City"),
                                    District = GetNullSave.SafeGetString(sqlDataReader, 3),
                                    Street = sqlDataReader.GetString("Street"),
                                    HouseNumber = sqlDataReader.GetInt32("HouseNumber"),
                                    PremisesNumber = GetNullSave.SafeGetInt(sqlDataReader, 6)
                                });
                            }
                        }
                        for (int i = 0; i < 2; i++)
                        {
                            if (sqlDataReader.NextResult())
                            {
                                while (sqlDataReader.Read())
                                {
                                    vet.Diseases.Add(new DiseaseDTO
                                    {
                                        Id = sqlDataReader.GetInt32("Id"),
                                        Name = sqlDataReader.GetString("Name")
                                    });
                                }

                            }
                        }

                        for (int i = 0; i < 2; i++)
                        { 
                            if (sqlDataReader.NextResult())
                            {
                                while (sqlDataReader.Read())
                                {
                                    vet.ServicesTreatments.Add(new ServiceTreatmentDTO
                                    {
                                        Id = sqlDataReader.GetInt32("Id"),
                                        Name = sqlDataReader.GetString("Name")
                                    });
                                }
                            }
                        }
                    }
                }
                await sqlConnection.CloseAsync();
            }
            return vet;
        }
    }
}
