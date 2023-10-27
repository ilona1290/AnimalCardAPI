using AnimalCard.Decryption;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalCard.Application.User.Commands.UpdateContactData
{
    public class UpdateContactDataCommandHandler : IRequestHandler<UpdateContactDataCommand, bool>
    {
        public async Task<bool> Handle(UpdateContactDataCommand request, CancellationToken cancellationToken)
        {
            const string PROCEDURE_NAME = "[dbo].[UpdateContactData]";
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionStrings.Database))
            {
                await sqlConnection.OpenAsync();

                using (SqlCommand sqlCommand = new SqlCommand(PROCEDURE_NAME, sqlConnection) { CommandType = System.Data.CommandType.StoredProcedure })
                {
                    sqlCommand.Parameters.Add("@who", SqlDbType.NVarChar).Value = request.Who;
                    sqlCommand.Parameters.Add("@id", SqlDbType.Int).Value = request.Id;
                    sqlCommand.Parameters.Add("@email", SqlDbType.NVarChar).Value = request.Email;
                    sqlCommand.Parameters.Add("@phoneNumber", SqlDbType.NVarChar).Value = request.PhoneNumber;

                    sqlCommand.ExecuteNonQuery();
                }
                await sqlConnection.CloseAsync();

            }
            return true;
        }
    }
}
