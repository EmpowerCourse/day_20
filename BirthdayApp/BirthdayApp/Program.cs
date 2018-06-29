using System;
using System.Configuration;
using System.Linq;

namespace BirthdayApp
{
    class Program
    {
        static void Main(string[] args)
        {
			var connectionString = ConfigurationManager.ConnectionStrings["BirthdayConnectionString"].ConnectionString;
			var database = new BirthdayDatabase(connectionString);

			//Console.WriteLine("Please enter a location name:");
			//var locationName = Console.ReadLine();

			//database.Create(new Location() { LocationName = locationName });


			var allLocations = database.GetAllLocations().ToArray();

			foreach (var location in allLocations)
			{
				Console.WriteLine($"{location.LocationId}\t {location.LocationName}");
			}

			var availableLocations = allLocations
				.Where(loc => !loc.IsAvailable.HasValue)
				.OrderBy(loc => loc.LocationName)
				.ToArray();



            Console.WriteLine("Hello World!");
        }
    }
}
