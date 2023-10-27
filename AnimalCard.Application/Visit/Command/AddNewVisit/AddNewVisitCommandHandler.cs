using AnimalCard.Decryption;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnimalCard.Application.Owner.Queries.GetOwner;

namespace AnimalCard.Application.Visit.Command.AddNewVisit
{
    public class AddNewVisitCommandHandler : IRequestHandler<AddNewVisitCommand, bool>
    {
        public async Task<bool> Handle(AddNewVisitCommand request, CancellationToken cancellationToken)
        {
            //try
            TimeZoneInfo localTimeZone = TimeZoneInfo.Local;
            
            //{DateTime americanDateTime = DateTime.Now; // Przykładowa data i godzina w amerykańskiej strefie czasowej
            TimeZoneInfo americanTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time"); // Amerykańska strefa czasowa (EST)

            // Przekonwertowanie na strefę czasową polską (CET)
            TimeZoneInfo polishTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Central European Standard Time"); // Polska strefa czasowa (CET)
            
            request.DateVisit = TimeZoneInfo.ConvertTimeFromUtc(request.DateVisit.ToUniversalTime(), polishTimeZone);
            request.TimeStartVisit = TimeZoneInfo.ConvertTimeFromUtc(request.TimeStartVisit.ToUniversalTime(), polishTimeZone);
            request.TimeEndVisit = TimeZoneInfo.ConvertTimeFromUtc(request.TimeEndVisit.ToUniversalTime(), polishTimeZone);


            var shortDateVisit = request.DateVisit.ToShortDateString();
            var shortStartTime = request.TimeStartVisit.ToLongTimeString();
            var shortEndTime = request.TimeEndVisit.ToLongTimeString();
            string combinedDateTimeStartString = $"{shortDateVisit} {shortStartTime}";
            string combinedDateTimeEndString = $"{shortDateVisit} {shortEndTime}";
            DateTime combinedStartDateTime = new DateTime();
            DateTime combinedEndDateTime = new DateTime();

            if (localTimeZone.Id.Equals(polishTimeZone.Id, StringComparison.OrdinalIgnoreCase))
            {
                DateTime.TryParseExact(combinedDateTimeStartString, "dd.MM.yyyy HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out combinedStartDateTime);
                DateTime.TryParseExact(combinedDateTimeEndString, "dd.MM.yyyy HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out combinedEndDateTime);
            }
            else
            {
                DateTime.TryParseExact(combinedDateTimeStartString, "M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture, DateTimeStyles.None, out combinedStartDateTime);
                DateTime.TryParseExact(combinedDateTimeEndString, "M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture, DateTimeStyles.None, out combinedEndDateTime);
            }
            

            request.TimeStartVisit = combinedStartDateTime;
            request.TimeEndVisit = combinedEndDateTime;


            const string PROCEDURE_NAME = "[dbo].[AddNewVisit]";
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionStrings.Database))
            {
                await sqlConnection.OpenAsync();

                using (SqlCommand sqlCommand = new SqlCommand(PROCEDURE_NAME, sqlConnection) { CommandType = System.Data.CommandType.StoredProcedure })
                {
                    sqlCommand.Parameters.Add("@vetId", SqlDbType.Int).Value = request.Vet;
                    sqlCommand.Parameters.Add("@visitTypeId", SqlDbType.Int).Value = request.VisitType;
                    sqlCommand.Parameters.Add("@customVisitType", SqlDbType.NVarChar).Value = request.CustomType;
                    sqlCommand.Parameters.Add("@ownerId", SqlDbType.Int).Value = request.Owner;
                    sqlCommand.Parameters.Add("@petId", SqlDbType.Int).Value = request.Patient;
                    sqlCommand.Parameters.Add("@dateStartVisit", SqlDbType.DateTime).Value = request.TimeStartVisit;
                    sqlCommand.Parameters.Add("@dateEndVisit", SqlDbType.DateTime).Value = request.TimeEndVisit;
                    sqlCommand.Parameters.Add("@extraInfo", SqlDbType.NVarChar).Value = request.ExtraInfo;
                    sqlCommand.ExecuteNonQuery();
                }
                await sqlConnection.CloseAsync();

            }
            return true;
        }
    }
}
