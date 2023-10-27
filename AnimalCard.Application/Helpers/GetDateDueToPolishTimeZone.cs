using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalCard.Application.Helpers
{
    public static class GetDateDueToPolishTimeZone
    {
        public static DateTime ReturnDateNow()
        {
            // Przekonwertowanie na strefę czasową polską (CET)
            TimeZoneInfo polishTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Central European Standard Time"); // Polska strefa czasowa (CET)
            return TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), polishTimeZone);
        }

        public static DateTime ReturnDate(DateTime date)
        {
            TimeZoneInfo polishTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Central European Standard Time"); // Polska strefa czasowa (CET)
            return TimeZoneInfo.ConvertTimeFromUtc(date.ToUniversalTime(), polishTimeZone);
        }
    }
}
