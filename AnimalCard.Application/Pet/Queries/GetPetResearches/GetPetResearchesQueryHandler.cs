using AnimalCard.Application.Pet.Queries.GetPetRabiesVaccinations;
using AnimalCard.Decryption;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnimalCard.Application.Helpers;

namespace AnimalCard.Application.Pet.Queries.GetPetResearches
{
    public class GetPetResearchesQueryHandler : IRequestHandler<GetPetResearchesQuery, PetResearchesVm>
    {
        public async Task<PetResearchesVm> Handle(GetPetResearchesQuery request, CancellationToken cancellationToken)
        {
            const string PROCEDURE_NAME = "[dbo].[GetPetResearches]";
            PetResearchesVm petResearches = new PetResearchesVm();
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
                            PetReaserchDTO petReaserch = new PetReaserchDTO();
                            petReaserch.Id = sqlDataReader.GetInt32("Id");
                            petReaserch.ResearchesList = sqlDataReader.GetString("ResearchesList");
                            petReaserch.ResearchesDate = DateOnly.FromDateTime(sqlDataReader.GetDateTime("ResearchesDate"));
                            petReaserch.ResultFileName = sqlDataReader.GetString("ResultFileName");
                            petReaserch.ResultPath = sqlDataReader.GetString("ResultPath");
                            petReaserch.ResultDate = GetNullSave.SafeGetDateOnly(sqlDataReader, 5);
                            petReaserch.Vet = sqlDataReader.GetString("Vet");

                            petResearches.PetReaserches.Add(petReaserch);
                        }
                    }
                }
                await sqlConnection.CloseAsync();
            }
            return petResearches;
        }
    }
}
