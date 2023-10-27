using AnimalCard.Decryption;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalCard.Application.ProfilePicture.Commands.UpdateProfilePicture
{
    public class UpdateProfilePictureCommandHandler : IRequestHandler<UpdateProfilePictureCommand, bool>
    {
        public async Task<bool> Handle(UpdateProfilePictureCommand request, CancellationToken cancellationToken)
        {
            const string PROCEDURE_NAME = "[dbo].[UpdatePhoto]";
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionStrings.Database))
            {
                await sqlConnection.OpenAsync();
                using (SqlCommand sqlCommand = new SqlCommand(PROCEDURE_NAME, sqlConnection) { CommandType = System.Data.CommandType.StoredProcedure })
                {
                    sqlCommand.Parameters.Add("@who", SqlDbType.NVarChar).Value = request.Who;
                    sqlCommand.Parameters.Add("@id", SqlDbType.Int).Value = request.Id;
                    sqlCommand.Parameters.Add("@photo", SqlDbType.NVarChar).Value = request.Photo;

                    sqlCommand.ExecuteNonQuery();
                }
                await sqlConnection.CloseAsync();

            }
            return true;
        }
    }
}
