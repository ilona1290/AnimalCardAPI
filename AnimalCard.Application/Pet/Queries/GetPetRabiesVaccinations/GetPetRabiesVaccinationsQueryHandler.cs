using AnimalCard.Decryption;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalCard.Application.Pet.Queries.GetPetRabiesVaccinations
{
    public class GetPetRabiesVaccinationsQueryHandler : IRequestHandler<GetPetRabiesVaccinationsQuery, PetRabiesVaccinationsVm>
    {
        public async Task<PetRabiesVaccinationsVm> Handle(GetPetRabiesVaccinationsQuery request, CancellationToken cancellationToken)
        {
            const string PROCEDURE_NAME = "[dbo].[GetPetRabiesVaccinations]";
            PetRabiesVaccinationsVm petRabiesVaccinations = new PetRabiesVaccinationsVm();
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
                            PetRabiesVaccinationDTO petRabiesVaccination = new PetRabiesVaccinationDTO();
                            petRabiesVaccination.Id = sqlDataReader.GetInt32("Id");
                            petRabiesVaccination.Name = sqlDataReader.GetString("Name");
                            petRabiesVaccination.Series = sqlDataReader.GetString("Series");
                            petRabiesVaccination.VaccinationDate = DateOnly.FromDateTime(sqlDataReader.GetDateTime("VaccinationDate"));
                            petRabiesVaccination.TermValidity = DateOnly.FromDateTime(sqlDataReader.GetDateTime("TermValidity"));
                            petRabiesVaccination.TermNext = DateOnly.FromDateTime(sqlDataReader.GetDateTime("TermNext"));
                            petRabiesVaccination.Vet = sqlDataReader.GetString("Vet");

                            petRabiesVaccinations.PetRabiesVaccinations.Add(petRabiesVaccination);
                        }
                    }
                }
                await sqlConnection.CloseAsync();
            }
            return petRabiesVaccinations;
        }
    }
}
