using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Web;
using DerbyHacks.Model;

namespace DerbyHacksApi
{
    public class DataHelper
    {
        private SQLiteConnection dbConnection;

        public DataHelper()
        {
            dbConnection = DbInit.FindOrCreate("data");
            dbConnection.Open();
        }

        public IEnumerable<CrimeData> GetIncidentsInRange(double longitude, double latitude, double range)
        {
            List<CrimeData> incidents = new List<CrimeData>();
            string sql = string.Format("SELECT CrimeType, Latitude, Longitude FROM Incidents WHERE latitude > ({0} - {2}) and latitude < ({0} + {2}) and longitude > ({1} - {2}) and longitude < ({1} + {2})", latitude, longitude, range);
            SQLiteCommand command = new SQLiteCommand(sql, dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            while(reader.Read())
            {
                CrimeData incident = new CrimeData();
                incident.CrimeType = (string)reader["CrimeType"];
                incident.Latitude = (double)reader["Latitude"];
                incident.Longitude = (double)reader["Longitude"];
                incidents.Add(incident);
            }

            return incidents;

        }

    }
}