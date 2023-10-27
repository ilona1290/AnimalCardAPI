using AnimalCard.Decryption;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalCard.Application.Pet.Commands.AddResearchResult
{
    public class AddResearchResultCommandHandler : IRequestHandler<AddResearchResultCommand, bool>
    {
        public async Task<bool> Handle(AddResearchResultCommand request, CancellationToken cancellationToken)
        {
            const string PROCEDURE_NAME = "[dbo].[AddResearchResult]";
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionStrings.Database))
            {
                await sqlConnection.OpenAsync();
                using (SqlCommand sqlCommand = new SqlCommand(PROCEDURE_NAME, sqlConnection) { CommandType = System.Data.CommandType.StoredProcedure })
                {
                    sqlCommand.Parameters.Add("@resultFileName", SqlDbType.NVarChar).Value = request.ResultFileName;
                    sqlCommand.Parameters.Add("@resultPath", SqlDbType.NVarChar).Value = request.ResultPath;;
                    sqlCommand.Parameters.Add("@resultDate", SqlDbType.Date).Value = DateTime.Now.Date;
                    sqlCommand.Parameters.Add("@petId", SqlDbType.Int).Value = request.PetId;

                    try
                    {
                        sqlCommand.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }
                }
                await sqlConnection.CloseAsync();

            }
            return true;
        }
    }
}
