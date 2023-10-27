using AnimalCard.Decryption;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Xml.Linq;

namespace AnimalCard.Application.Pet.Commands.AddPet
{
    public class AddPetCommandHandler : IRequestHandler<AddPetCommand, int>
    {
        public async Task<int> Handle(AddPetCommand request, CancellationToken cancellationToken)
        {
            const string PROCEDURE_NAME = "[dbo].[AddPet]";
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionStrings.Database))
            {
                await sqlConnection.OpenAsync();

                using (SqlCommand sqlCommand = new SqlCommand(PROCEDURE_NAME, sqlConnection) { CommandType = System.Data.CommandType.StoredProcedure })
                {
                    sqlCommand.Parameters.Add("@vetId", SqlDbType.Int).Value = request.Vet;
                    sqlCommand.Parameters.Add("@ownerId", SqlDbType.Int).Value = request.Owner;
                    sqlCommand.Parameters.Add("@identityNumber", SqlDbType.NVarChar).Value = request.IdentityNumber;
                    sqlCommand.Parameters.Add("@photo", SqlDbType.NVarChar).Value = request.Photo;
                    sqlCommand.Parameters.Add("@name", SqlDbType.NVarChar).Value = request.Name;
                    sqlCommand.Parameters.Add("@sex", SqlDbType.NVarChar).Value = request.Sex;
                    sqlCommand.Parameters.Add("@dateBirth", SqlDbType.Date).Value = request.DateBirth;
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
                    catch(Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }
                }
                await sqlConnection.CloseAsync();

            }
            return 1;
        }
    }
}
