using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace BirthdayApp
{
    internal class BirthdayDatabase
    {
		private readonly string connectionString;

		internal BirthdayDatabase(string connectionString)
		{
			this.connectionString = connectionString;
		}

		public int Create(CelebrationLocation location)
		{
			using (var con = new SqlConnection(connectionString))
			{
				con.Open();
				using (var com = new SqlCommand())
				{
					com.Connection = con;
					com.CommandText = @"
						INSERT INTO [Location] (LocationName) VALUES (@LocationName)
						SELECT CAST(SCOPE_IDENTITY() AS INT)
						";
					com.Parameters.AddWithValue("@LocationName", location.LocationName);
					var id = (int)com.ExecuteScalar();
					return id;
				}
			}
		}

		public IEnumerable<CelebrationLocation> GetAllLocations()
		{
			using (var con = new SqlConnection(connectionString))
			{
				con.Open();
				using (var com = new SqlCommand())
				{
					com.Connection = con;
					com.CommandText = @"SELECT * FROM Location";
					using (var rdr = com.ExecuteReader())
					{
						while (rdr.Read())
						{
							var location = new CelebrationLocation();
							location.LocationId = (int)rdr["LocationId"];
							location.LocationName = (string)rdr["LocationName"];
							location.IsAvailable = rdr.GetBooleanOrNull(2);
							yield return location;
						}
					}
				}
			}
		}
    }
}
