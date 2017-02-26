using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Web;

namespace DerbyHacks.Model
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
            string sql = string.Format("SELECT CrimeType, Latitude, Longitude FROM CrimeData WHERE latitude > ({0} - {2}) and latitude < ({0} + {2}) and longitude > ({1} - {2}) and longitude < ({1} + {2})", latitude, longitude, range);
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

        public void Insert(IEnumerable<CrimeData> data)
        {
            using (SQLiteConnection connection = DbInit.FindOrCreate("data"))
            {
                connection.Open();

                using (SQLiteCommand command = new SQLiteCommand(connection))
                {
                    command.CommandType = System.Data.CommandType.Text;
                    command.CommandText = @"
                        Insert into CrimeData (Id, DateOccured, CrimeType, BlockAddress, City, Zip, IncidentNumber, State, FormattedAddress, Latitude, Longitude)
                        Values 
                    ";
                    foreach (var item in data)
                    {
                        command.CommandText += string.Format("({0}, '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', {9}, {10}),",
                            item.Id, 
                            item.DateOccured, 
                            item.CrimeType,
                            item.BlockAddress, 
                            item.City,
                            item.Zip, 
                            item.IncidentNumber, 
                            item.State,
                            item.FormattedAddress, 
                            item.Latitude, 
                            item.Longitude);
                    }

                    command.CommandText = command.CommandText.Remove(command.CommandText.LastIndexOf(","), 1);

                    command.ExecuteNonQuery();
                }
            }
        }

    }
}