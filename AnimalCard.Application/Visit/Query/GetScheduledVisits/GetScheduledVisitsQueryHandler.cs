using AnimalCard.Decryption;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalCard.Application.Visit.Query.GetScheduledVisits
{
    public class GetScheduledVisitsQueryHandler : IRequestHandler<GetScheduledVisitsQuery, ScheduledVisitsVm>
    {
        public async Task<ScheduledVisitsVm> Handle(GetScheduledVisitsQuery request, CancellationToken cancellationToken)
        {
            const string PROCEDURE_NAME = "[dbo].[GetScheduledVisits]";
            ScheduledVisitsVm visitsVm = new ScheduledVisitsVm();
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionStrings.Database))
            {
                await sqlConnection.OpenAsync();

                using (SqlCommand sqlCommand = new SqlCommand(PROCEDURE_NAME, sqlConnection) { CommandType = System.Data.CommandType.StoredProcedure })
                {
                    sqlCommand.Parameters.Add("@vetId", SqlDbType.NVarChar).Value = request.VetId;
                    sqlCommand.Parameters.Add("@ownerId", SqlDbType.NVarChar).Value = request.OwnerId;
                    using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                    {
                        while (sqlDataReader.Read())
                        {
                            ScheduledVisitDTO visit = new ScheduledVisitDTO();
                            visit.Id = sqlDataReader.GetInt32("Id");
                            visit.VisitTypeId = sqlDataReader.GetInt32("VisitTypeId");
                            if(visit.VisitTypeId != 6)
                            {
                                visit.VisitTypeName = sqlDataReader.GetString("VisitTypeName");
                            }
                            else
                            {
                                visit.VisitTypeName = sqlDataReader.GetString("CustomVisitTypeName");
                            }
                            visit.VetId = sqlDataReader.GetInt32("VetId");
                            visit.Vet = sqlDataReader.GetString("Vet");
                            visit.OwnerId = sqlDataReader.GetInt32("OwnerId");
                            visit.Owner = sqlDataReader.GetString("Owner");
                            visit.PatientId = sqlDataReader.GetInt32("PetId");
                            visit.Patient = sqlDataReader.GetString("Patient");
                            visit.StartDate = sqlDataReader.GetDateTime("StartDate");
                            visit.EndDate = sqlDataReader.GetDateTime("EndDate");
                            visit.ExtraInfo = sqlDataReader.GetString("ExtraInfo");
                            visit.IsCompleted = sqlDataReader.GetBoolean("IsCompleted");

                            visitsVm.ScheduledVisits.Add(visit);
                        }
                    }
                }
                await sqlConnection.CloseAsync();
            }
            return visitsVm;
        }
    }
}
