using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Web;

namespace DerbyHacksApi.Models
{
    public class DataHelper
    {
        private SQLiteConnection dbConnection;

        public DataHelper()
        {
            dbConnection = new SQLiteConnection("Data Source=data.sqlite;Version=3;");
            dbConnection.Open();
        }

        public IEnumerable<Incident> GetIncidents(double longitude, double latitude)
        {
            List<Incident> incidents = new List<Incident>();
            string sql = string.Format("SELECT * FROM");
            return incidents;

        }

    }
}