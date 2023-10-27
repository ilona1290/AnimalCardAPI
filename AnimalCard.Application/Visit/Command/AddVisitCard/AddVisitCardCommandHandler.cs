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

namespace AnimalCard.Application.Visit.Command.AddVisitCard
{
    public class AddVisitCardCommandHandler : IRequestHandler<AddVisitCardCommand, bool>
    {
        public async Task<bool> Handle(AddVisitCardCommand request, CancellationToken cancellationToken)
        {
            const string PROCEDURE_NAME_GET_PET_ID = "[dbo].[GetPetIdByVisitId]";
            const string PROCEDURE_NAME_ADD_COMPLETED_VISIT = "[dbo].[AddCompletedVisit]";

            int petId = 0;
            int vetId = request.VetId;

            using (SqlConnection sqlConnection = new SqlConnection(ConnectionStrings.Database))
            {
                await sqlConnection.OpenAsync();

                if (request.VisitId != 0)
                {
                    using (SqlCommand sqlCommand = new SqlCommand(PROCEDURE_NAME_GET_PET_ID, sqlConnection) { CommandType = System.Data.CommandType.StoredProcedure })
                    {
                        sqlCommand.Parameters.Add("@visitId", SqlDbType.Int).Value = request.VisitId;
                        using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                        {
                            while (sqlDataReader.Read())
                            {
                                petId = sqlDataReader.GetInt32("PetId");
                            }
                        }
                    }
                }
                else
                {
                    petId = request.PetId;
                }

                using (SqlCommand sqlCommand = new SqlCommand(PROCEDURE_NAME_ADD_COMPLETED_VISIT, sqlConnection) { CommandType = System.Data.CommandType.StoredProcedure })
                {
                    sqlCommand.Parameters.Add("@visitCardFileName", SqlDbType.NVarChar).Value = request.VisitCardFileName;
                    sqlCommand.Parameters.Add("@visitCardPath", SqlDbType.NVarChar).Value = request.VisitCardPath;
                    sqlCommand.Parameters.Add("@visitDate", SqlDbType.Date).Value = GetDateDueToPolishTimeZone.ReturnDateNow().Date;
                    sqlCommand.Parameters.Add("@petId", SqlDbType.Int).Value = petId;
                    sqlCommand.Parameters.Add("@vetId", SqlDbType.Int).Value = vetId;
                    sqlCommand.ExecuteNonQuery();
                }
            }
            return true;
        }
    }
}
