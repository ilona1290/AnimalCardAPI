using AnimalCard.Decryption;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalCard.Application.Pet.Queries.GetPetTreatments
{
    public class GetPetTreatmentsQueryHandler : IRequestHandler<GetPetTreatmentsQuery, PetTreatmentsVm>
    {
        public async Task<PetTreatmentsVm> Handle(GetPetTreatmentsQuery request, CancellationToken cancellationToken)
        {
            const string PROCEDURE_NAME = "[dbo].[GetPetTreatments]";
            PetTreatmentsVm petTreatments = new PetTreatmentsVm();
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionStrings.Database))
            {
                await sqlConnection.OpenAsync();

                using (SqlCommand sqlCommand = new SqlCommand(PROCEDURE_NAME, sqlConnection) { CommandType = CommandType.StoredProcedure })
                {
                    sqlCommand.Parameters.Add("@petId", SqlDbType.Int).Value = request.PetId;
                    using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                    {
                        while (sqlDataReader.Read())
                        {
                            PetTreatmentDTO petTreatment = new PetTreatmentDTO();
                            petTreatment.Id = sqlDataReader.GetInt32("Id");
                            petTreatment.Name = sqlDataReader.GetString("Name");
                            petTreatment.Diagnosis = sqlDataReader.GetString("Diagnosis");
                            petTreatment.TreatmentDescription = sqlDataReader.GetString("TreatmentDescription");
                            petTreatment.Recommendations = sqlDataReader.GetString("Recommendations");
                            petTreatment.TreatmentDate = DateOnly.FromDateTime(sqlDataReader.GetDateTime("TreatmentDate"));
                            petTreatment.Vet = sqlDataReader.GetString("Vet");

                            petTreatments.PetTreatments.Add(petTreatment);
                        }
                    }
                }
                await sqlConnection.CloseAsync();
            }
            return petTreatments;
        }
    }
}
