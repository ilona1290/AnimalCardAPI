using AnimalCard.Application.Helpers;
using AnimalCard.Application.Vet.Queries.GetVetDetails;
using AnimalCard.Decryption;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalCard.Application.Pet.Queries.GetPet
{
    public class GetPetQueryHandler : IRequestHandler<GetPetQuery, PetVm>
    {
        public async Task<PetVm> Handle(GetPetQuery request, CancellationToken cancellationToken)
        {
            const string PROCEDURE_NAME = "[dbo].[GetPet]";
            PetVm pet = new PetVm();
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionStrings.Database))
            {
                await sqlConnection.OpenAsync();

                using (SqlCommand sqlCommand = new SqlCommand(PROCEDURE_NAME, sqlConnection) { CommandType = System.Data.CommandType.StoredProcedure })
                {
                    sqlCommand.Parameters.Add("@petId", SqlDbType.NVarChar).Value = request.PetId;
                    using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                    {
                        while (sqlDataReader.Read())
                        {
                            pet.Id = sqlDataReader.GetInt32("Id");
                            pet.IdentityNumber = sqlDataReader.GetString("IdentityNumber");
                            pet.Photo = sqlDataReader.GetString("Photo");
                            pet.Name = sqlDataReader.GetString("Name");
                            pet.Sex = sqlDataReader.GetString("Sex");
                            pet.DateBirth = DateOnly.FromDateTime(sqlDataReader.GetDateTime("DateBirth"));
                            pet.Breed = sqlDataReader.GetString("Breed");
                            pet.Color = sqlDataReader.GetString("Color");
                            pet.HairType = sqlDataReader.GetString("HairType");
                            pet.Trademarks = sqlDataReader.GetString("Trademarks");
                            pet.Allergies = sqlDataReader.GetString("Allergies");
                            pet.ExtraInfo = sqlDataReader.GetString("ExtraInfo");
                            pet.Owner = sqlDataReader.GetString("Owner");
                        } 
                    }
                }
                await sqlConnection.CloseAsync();
            }
            return pet;
        }
    }
}
