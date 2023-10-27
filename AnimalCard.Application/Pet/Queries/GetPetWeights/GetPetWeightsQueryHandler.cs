using AnimalCard.Decryption;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalCard.Application.Pet.Queries.GetPetWeights
{
    public class GetPetWeightsQueryHandler : IRequestHandler<GetPetWeightsQuery, PetWeightsVm>
    {
        public async Task<PetWeightsVm> Handle(GetPetWeightsQuery request, CancellationToken cancellationToken)
        {
            const string PROCEDURE_NAME = "[dbo].[GetPetWeights]";
            PetWeightsVm petWeights = new PetWeightsVm();
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
                            PetWeightDTO petWeight = new PetWeightDTO();
                            petWeight.WeighingDate = DateOnly.FromDateTime(sqlDataReader.GetDateTime("WeighingDate")).ToString("dd.MM.yyyy");
                            petWeight.Value = sqlDataReader.GetDecimal("Value");

                            petWeights.PetWeights.Add(petWeight);
                        }
                    }
                }
                await sqlConnection.CloseAsync();
            }
            return petWeights;
        }
    }
}
