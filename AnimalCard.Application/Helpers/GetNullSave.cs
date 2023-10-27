using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalCard.Application.Helpers
{
    public static class GetNullSave
    {
        public static string? SafeGetString(this SqlDataReader reader, int colIndex)
        {
            if (!reader.IsDBNull(colIndex))
                return reader.GetString(colIndex);
            return null;
        }

        public static int? SafeGetInt(this SqlDataReader reader, int colIndex)
        {
            if (!reader.IsDBNull(colIndex))
                return reader.GetInt32(colIndex);
            return null;
        }

        public static DateOnly? SafeGetDateOnly(this SqlDataReader reader, int colIndex)
        {
            if (!reader.IsDBNull(colIndex))
                return DateOnly.FromDateTime(reader.GetDateTime(colIndex));
            return null;
        }
    }
}
