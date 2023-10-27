using AnimalCard.Application.Helpers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnimalCard.Decryption;

namespace AnimalCard.Application.Admin.Commands.ConfirmOrRejectCustoms
{
    public class ConfirmOrRejectCustomsCommandHandler : IRequestHandler<ConfirmOrRejectCustomsCommand, bool>
    {
        public async Task<bool> Handle(ConfirmOrRejectCustomsCommand request, CancellationToken cancellationToken)
        {
            const string PROCEDURE_NAME = "[dbo].[ConfirmOrRejectCustomsDisAndServTreat]";
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionStrings.Database))
            {
                await sqlConnection.OpenAsync();

                using (SqlCommand sqlCommand = new SqlCommand(PROCEDURE_NAME, sqlConnection) { CommandType = System.Data.CommandType.StoredProcedure })
                {
                    DataTable dtConfirmedDiseases = new DataTable();
                    DataTable dtRejectedDiseases = new DataTable();
                    DataTable dtConfirmedServicesTreatments = new DataTable();
                    DataTable dtRejectedServicesTreatments = new DataTable();

                    dtConfirmedDiseases = ListToDataTable.OfIntsToDataTable(request.ConfirmedDiseases);
                    dtRejectedDiseases = ListToDataTable.OfIntsToDataTable(request.RejectedDiseases);
                    dtConfirmedServicesTreatments = ListToDataTable.OfIntsToDataTable(request.ConfirmedServicesTreatments);
                    dtRejectedServicesTreatments = ListToDataTable.OfIntsToDataTable(request.RejectedServicesTreatments);

                    sqlCommand.Parameters.Add(new SqlParameter("@confirmedDiseasesTableType", dtConfirmedDiseases));
                    sqlCommand.Parameters.Add(new SqlParameter("@rejectedDiseasesTableType", dtRejectedDiseases));
                    sqlCommand.Parameters.Add(new SqlParameter("@confirmedServicesTreatmentsTableType", dtConfirmedServicesTreatments));
                    sqlCommand.Parameters.Add(new SqlParameter("@rejectedServicesTreatmentsTableType", dtRejectedServicesTreatments));

                    sqlCommand.ExecuteNonQuery();
                }
                await sqlConnection.CloseAsync();

            }

            return true;
        }
    }
}
