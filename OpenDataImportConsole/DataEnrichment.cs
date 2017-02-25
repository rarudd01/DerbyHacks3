using DerbyHacks.Model;
using Geocoding;
using Geocoding.Google;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenDataImportConsole
{
    public static class DataEnrichment
    {
        public static void Geocode(string apiKey, CrimeData data)
        {
            IGeocoder coder = new GoogleGeocoder();
            string address = BuildCrimeDataAddress(data);
            IEnumerable<Address> addresses = coder.Geocode(address);
            //Console.WriteLine("Formatted: " + addresses.First().FormattedAddress);
            //Console.WriteLine("Coordinates: " + addresses.First().Coordinates.Latitude + ", " + addresses.First().Coordinates.Longitude); //Coordinates: 38.8791981, -76.9818437
            data.FormattedAddress = addresses.First().FormattedAddress;
            data.Latitude = addresses.First().Coordinates.Latitude;
            data.Longitude = addresses.First().Coordinates.Longitude;
        }

        private static string BuildCrimeDataAddress(CrimeData data)
        {
            return string.Format("{0} {1} {2} {3}", data.BlockAddress, data.City, data.State, data.Zip);
        }
    }
}
