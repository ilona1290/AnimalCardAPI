using AnimalCard.Application.Vet.Queries.GetDiseasesAndServicesTreatments;
using AnimalCard.Decryption;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalCard.Application.Admin.Queries.GetCustomsToConfirm
{
    public class GetCustomsToConfirmQueryHandler : IRequestHandler<GetCustomsToConfirmQuery, CustomsToConfirmVm>
    {
        public async Task<CustomsToConfirmVm> Handle(GetCustomsToConfirmQuery request, CancellationToken cancellationToken)
        {
            const string PROCEDURE_NAME = "[dbo].[GetCustomDiseasesAndServicesTreatmentsToConfirm]";
            CustomsToConfirmVm customsToConfirmVm = new CustomsToConfirmVm();
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionStrings.Database))
            {
                await sqlConnection.OpenAsync();

                using (SqlCommand sqlCommand = new SqlCommand(PROCEDURE_NAME, sqlConnection) { CommandType = System.Data.CommandType.StoredProcedure })
                {
                    using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                    {

                        while (sqlDataReader.Read())
                        {
                            customsToConfirmVm.CustomsToConfirm.Add(new CustomsToConfirmDTO
                            {
                                IdVet = sqlDataReader.GetInt32("Id"),
                                VetName = sqlDataReader.GetString("Name"),
                                VetSurname = sqlDataReader.GetString("Surname"),
                            });
                        }
                        if (sqlDataReader.NextResult())
                        {
                            while (sqlDataReader.Read())
                            {
                                var vet = customsToConfirmVm.CustomsToConfirm.FirstOrDefault(a => a.IdVet == sqlDataReader.GetInt32("IdVet"));
                                vet?.DiseasesToConfirm.Add(new CustomDiseaseToConfirmDTO
                                {
                                    DiseaseId = sqlDataReader.GetInt32("Id"),
                                    DiseaseName = sqlDataReader.GetString("Name"),
                                });
                            }
                        }

                        if (sqlDataReader.NextResult())
                        {
                            while (sqlDataReader.Read())
                            {
                                var vet = customsToConfirmVm.CustomsToConfirm.FirstOrDefault(a => a.IdVet == sqlDataReader.GetInt32("IdVet"));
                                vet?.ServicesTreatmentsToConfirm.Add(new CustomServiceTreatmentToConfirmDTO
                                {
                                    ServiceTreatmentId = sqlDataReader.GetInt32("Id"),
                                    ServiceTreatmentName = sqlDataReader.GetString("Name"),
                                });
                            }
                        }
                    }

                }
                await sqlConnection.CloseAsync();
            }
            return customsToConfirmVm;
        }
    }
}
