using AnimalCard.Application.Admin.Queries.GetCustomsToConfirm;
using AnimalCard.Decryption;
using Azure.Storage.Blobs.Models;
using iText.StyledXmlParser.Jsoup.Select;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalCard.Application.Vet.Queries.GetVets
{
    public class GetVetsQueryHandler : IRequestHandler<GetVetsQuery, VetsVm>
    {
        public async Task<VetsVm> Handle(GetVetsQuery request, CancellationToken cancellationToken)
        {
            const string PROCEDURE_NAME = "[dbo].[GetVets]";
            VetsVm vets = new VetsVm();
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionStrings.Database))
            {
                await sqlConnection.OpenAsync();

                using (SqlCommand sqlCommand = new SqlCommand(PROCEDURE_NAME, sqlConnection) { CommandType = System.Data.CommandType.StoredProcedure })
                {
                    using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                    {
                        while (sqlDataReader.Read())
                        {
                            vets.VetsList.Add(new VetDTO
                            {
                                Id = sqlDataReader.GetInt32("Id"),
                                Vet = sqlDataReader.GetString("Vet"),
                                ProfilePicture = sqlDataReader.GetString("ProfilePicture"),
                            });
                        }
                        if (sqlDataReader.NextResult())
                        {
                            while (sqlDataReader.Read())
                            {
                                var vet = vets.VetsList.FirstOrDefault(a => a.Id == sqlDataReader.GetInt32("VetId"));
                                vet.VetsCitiesDistricts += sqlDataReader.GetString("City") + ", " + sqlDataReader.GetString("District") + ", ";
                            }
                        }

                        for (int i = 0; i < 2; i++)
                        {
                            if (sqlDataReader.NextResult())
                            {
                                while (sqlDataReader.Read())
                                {
                                    var vet = vets.VetsList.FirstOrDefault(a => a.Id == sqlDataReader.GetInt32("VetId"));
                                    vet.VetDiseases += sqlDataReader.GetString("Name") + ", ";
                                }
                            }
                        }

                        for (int i = 0; i < 2; i++)
                        {
                            if (sqlDataReader.NextResult())
                            {
                                while (sqlDataReader.Read())
                                {
                                    var vet = vets.VetsList.FirstOrDefault(a => a.Id == sqlDataReader.GetInt32("VetId"));
                                    vet.VetServicesTreatments += sqlDataReader.GetString("Name") + ", ";
                                }
                            }
                        }
                    }

                }
                await sqlConnection.CloseAsync();
            }
            return vets;
        }
    }
}
