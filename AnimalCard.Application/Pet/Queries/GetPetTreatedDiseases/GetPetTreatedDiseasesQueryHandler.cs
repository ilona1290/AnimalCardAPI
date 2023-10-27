using AnimalCard.Decryption;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalCard.Application.Pet.Queries.GetPetTreatedDiseases
{
    public class GetPetTreatedDiseasesQueryHandler : IRequestHandler<GetPetTreatedDiseasesQuery, PetTreatedDiseasesVm>
    {
        public async Task<PetTreatedDiseasesVm> Handle(GetPetTreatedDiseasesQuery request, CancellationToken cancellationToken)
        {
            const string PROCEDURE_NAME = "[dbo].[GetPetTreatedDiseases]";
            PetTreatedDiseasesVm petTreatedDiseases = new PetTreatedDiseasesVm();
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
                            PetTreatedDiseaseDTO petTreatedDisease = new PetTreatedDiseaseDTO();
                            petTreatedDisease.Id = sqlDataReader.GetInt32("Id");
                            petTreatedDisease.Name = sqlDataReader.GetString("Name");
                            petTreatedDisease.DiseaseDescription = sqlDataReader.GetString("DiseaseDescription");
                            petTreatedDisease.TreatmentDescription = sqlDataReader.GetString("TreatmentDescription");
                            petTreatedDisease.PrescribedMedications = sqlDataReader.GetString("PrescribedMedications");
                            petTreatedDisease.Recommendations = sqlDataReader.GetString("Recommendations");
                            petTreatedDisease.DiagnosisDate = DateOnly.FromDateTime(sqlDataReader.GetDateTime("DiagnosisDate"));
                            petTreatedDisease.Vet = sqlDataReader.GetString("Vet");

                            petTreatedDiseases.PetTreatedDiseases.Add(petTreatedDisease);
                        }
                    }
                }
                await sqlConnection.CloseAsync();
            }
            return petTreatedDiseases;
        }
    }
}
