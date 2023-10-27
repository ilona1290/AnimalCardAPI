using MediatR;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnimalCard.Decryption;

namespace AnimalCard.Application.Vet.Queries.GetDiseasesAndServicesTreatments
{
    public class GetDiseasesAndServicesTreatmentsQueryHandler : IRequestHandler<GetDiseasesAndServicesTreatmentsQuery, DiseasesAndServicesTreatmentsVm>
    {
        public async Task<DiseasesAndServicesTreatmentsVm> Handle(GetDiseasesAndServicesTreatmentsQuery request, CancellationToken cancellationToken)
        {
            const string PROCEDURE_NAME = "[dbo].[GetDiseasesAndServicesTreatments]";
            DiseasesAndServicesTreatmentsVm diseasesAndServicesTreatments = new DiseasesAndServicesTreatmentsVm();
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionStrings.Database))
            {
                await sqlConnection.OpenAsync();

                using (SqlCommand sqlCommand = new SqlCommand(PROCEDURE_NAME, sqlConnection) { CommandType = System.Data.CommandType.StoredProcedure })
                {
                    using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                    {

                        while (sqlDataReader.Read())
                        {
                            diseasesAndServicesTreatments.Diseases.Add(sqlDataReader.GetString("Name"));
                        }

                        if (sqlDataReader.NextResult())
                        {
                            while (sqlDataReader.Read())
                            {
                                diseasesAndServicesTreatments.ServicesTreatments.Add(sqlDataReader.GetString("Name"));
                            }
                        }
                    }

                }
                await sqlConnection.CloseAsync();
            }
            return diseasesAndServicesTreatments;
        }
    }
}
