using AnimalCard.Decryption;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalCard.Application.Pet.Queries.GetPetOtherVaccinations
{
    public class GetPetOtherVaccinationsQueryHandler : IRequestHandler<GetPetOtherVaccinationsQuery, PetOtherVaccinationsVm>
    {
        public async Task<PetOtherVaccinationsVm> Handle(GetPetOtherVaccinationsQuery request, CancellationToken cancellationToken)
        {
            const string PROCEDURE_NAME = "[dbo].[GetPetOtherVaccinations]";
            PetOtherVaccinationsVm petOtherVaccinations = new PetOtherVaccinationsVm();
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
                            PetOtherVaccinationDTO petOtherVaccination = new PetOtherVaccinationDTO();
                            petOtherVaccination.Id = sqlDataReader.GetInt32("Id");
                            petOtherVaccination.DiseaseName = sqlDataReader.GetString("DiseaseName");
                            petOtherVaccination.Name = sqlDataReader.GetString("Name");
                            petOtherVaccination.Series = sqlDataReader.GetString("Series");
                            petOtherVaccination.VaccinationDate = DateOnly.FromDateTime(sqlDataReader.GetDateTime("VaccinationDate"));
                            petOtherVaccination.Vet = sqlDataReader.GetString("Vet");

                            petOtherVaccinations.PetOtherVaccinations.Add(petOtherVaccination);
                        }
                    }
                }
                await sqlConnection.CloseAsync();
            }
            return petOtherVaccinations;
        }
    }
}
