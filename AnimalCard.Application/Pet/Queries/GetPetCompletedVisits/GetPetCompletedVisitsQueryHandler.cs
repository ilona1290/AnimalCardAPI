using AnimalCard.Application.Pet.Queries.GetPetOtherVaccinations;
using AnimalCard.Decryption;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalCard.Application.Pet.Queries.GetPetCompletedVisits
{
    public class GetPetCompletedVisitsQueryHandler : IRequestHandler<GetPetCompletedVisitsQuery, PetCompletedVisitsVm>
    {
        public async Task<PetCompletedVisitsVm> Handle(GetPetCompletedVisitsQuery request, CancellationToken cancellationToken)
        {
            const string PROCEDURE_NAME = "[dbo].[GetPetCompletedVisits]";
            PetCompletedVisitsVm petCompletedVisits = new PetCompletedVisitsVm();
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
                            PetCompletedVisitDTO petCompletedVisit = new PetCompletedVisitDTO();

                            petCompletedVisit.Id = sqlDataReader.GetInt32("Id");
                            petCompletedVisit.VisitCardFileName = sqlDataReader.GetString("VisitCardFileName");
                            petCompletedVisit.VisitCardPath = sqlDataReader.GetString("VisitCardPath");
                            petCompletedVisit.VisitDate = DateOnly.FromDateTime(sqlDataReader.GetDateTime("VisitDate"));
                            petCompletedVisit.Vet = sqlDataReader.GetString("Vet");

                            petCompletedVisits.PetCompletedVisits.Add(petCompletedVisit);
                        }
                    }
                }
                await sqlConnection.CloseAsync();
            }
            return petCompletedVisits;
        }
    }
}
