using AnimalCard.Application.Helpers;
using AnimalCard.Decryption;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalCard.Application.Visit.Command.DeleteScheduledVisit
{
    public class DeleteScheduledVisitCommandHandler : IRequestHandler<DeleteScheduledVisitCommand, bool>
    {
        public async Task<bool> Handle(DeleteScheduledVisitCommand request, CancellationToken cancellationToken)
        {
            const string PROCEDURE_NAME = "[dbo].[DeleteScheduledVisit]";

            using (SqlConnection sqlConnection = new SqlConnection(ConnectionStrings.Database))
            {
                await sqlConnection.OpenAsync();

                using (SqlCommand sqlCommand = new SqlCommand(PROCEDURE_NAME, sqlConnection) { CommandType = System.Data.CommandType.StoredProcedure })
                {
                    sqlCommand.Parameters.Add("@visitId", SqlDbType.Int).Value = request.VisitId;
                    sqlCommand.ExecuteNonQuery();
                }
            }
            return true;
        }
    }
}
