using AnimalCard.Application.Common.Interfaces;
using AnimalCard.Application.Vet.Commands.CreateVet;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnimalCard.Application.Helpers;
using AnimalCard.Decryption;

namespace AnimalCard.Application.Vet.Commands.UpdateProfile
{
    public class UpdateVetProfileCommandHandler : IRequestHandler<UpdateVetProfileCommand, string>
    {
        private readonly IIdentityService _identityService;
        public UpdateVetProfileCommandHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }
        public async Task<string> Handle(UpdateVetProfileCommand request, CancellationToken cancellationToken)
        {
            const string PROCEDURE_NAME = "[dbo].[UpdateVetProfile]";
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionStrings.Database))
            {
                await sqlConnection.OpenAsync();

                using (SqlCommand sqlCommand = new SqlCommand(PROCEDURE_NAME, sqlConnection) { CommandType = System.Data.CommandType.StoredProcedure })
                {
                    DataTable dtAddresses = new DataTable();
                    DataTable dtDiseases = new DataTable();
                    DataTable dtServicesTreatments = new DataTable();
                    dtAddresses = ListToDataTable.OfObjectsToDataTable(request.Addresses);
                    dtDiseases = ListToDataTable.OfStringsToDataTable(request.TreatedDiseases);
                    dtServicesTreatments = ListToDataTable.OfStringsToDataTable(request.ProvidedServicesTreatments);
                    sqlCommand.Parameters.Add("@idVet", SqlDbType.Int).Value = request.IdVet;
                    sqlCommand.Parameters.Add("@profilePicture", SqlDbType.NVarChar).Value = request.ProfilePicture;
                    sqlCommand.Parameters.Add("@aboutMe", SqlDbType.NVarChar).Value = request.AboutMe;
                    sqlCommand.Parameters.Add(new SqlParameter("@addressesTableType", dtAddresses));
                    sqlCommand.Parameters.Add(new SqlParameter("@diseasesTableType", dtDiseases));
                    sqlCommand.Parameters.Add(new SqlParameter("@servicesTreatmentsTableType", dtServicesTreatments));

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

            return string.Empty;
        }
    }
}
