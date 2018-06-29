using System;
using System.Data.SqlClient;

namespace BirthdayApp
{
    public static class SqlExtensions
    {
		public static bool? GetBooleanOrNull(this SqlDataReader reader, int index)
		{
			return reader.IsDBNull(index) ? (bool?)null : reader.GetBoolean(index);
		}
    }
}
