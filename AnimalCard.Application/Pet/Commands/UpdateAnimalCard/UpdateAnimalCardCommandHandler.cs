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

namespace AnimalCard.Application.Pet.Commands.UpdateAnimalCard
{
    public class UpdateAnimalCardCommandHandler : IRequestHandler<UpdateAnimalCardCommand, bool>
    {
        public async Task<bool> Handle(UpdateAnimalCardCommand request, CancellationToken cancellationToken)
        {
            const string PROCEDURE_NAME = "[dbo].[UpdatePet]";
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionStrings.Database))
            {
                await sqlConnection.OpenAsync();

                using (SqlCommand sqlCommand = new SqlCommand(PROCEDURE_NAME, sqlConnection) { CommandType = System.Data.CommandType.StoredProcedure })
                {
                    sqlCommand.Parameters.Add("@id", SqlDbType.Int).Value = request.Id;
                    sqlCommand.Parameters.Add("@ownerId", SqlDbType.NVarChar).Value = request.Owner;
                    sqlCommand.Parameters.Add("@identityNumber", SqlDbType.NVarChar).Value = request.IdentityNumber;
                    sqlCommand.Parameters.Add("@photo", SqlDbType.NVarChar).Value = request.Photo;
                    sqlCommand.Parameters.Add("@name", SqlDbType.NVarChar).Value = request.Name;
                    sqlCommand.Parameters.Add("@sex", SqlDbType.NVarChar).Value = request.Sex;
                    sqlCommand.Parameters.Add("@dateBirth", SqlDbType.Date).Value = GetDateDueToPolishTimeZone.ReturnDate(request.DateBirth).Date;
                    sqlCommand.Parameters.Add("@breed", SqlDbType.NVarChar).Value = request.Breed;
                    sqlCommand.Parameters.Add("@color", SqlDbType.NVarChar).Value = request.Color;
                    sqlCommand.Parameters.Add("@hairType", SqlDbType.NVarChar).Value = request.HairType;
                    sqlCommand.Parameters.Add("@trademarks", SqlDbType.NVarChar).Value = request.Trademarks;
                    sqlCommand.Parameters.Add("@allergies", SqlDbType.NVarChar).Value = request.Allergies;
                    sqlCommand.Parameters.Add("@extraInfo", SqlDbType.NVarChar).Value = request.ExtraInfo;

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
