using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.IO;

namespace DerbyHacks.Model
{
    public static class DbInit
    {
        public static SQLiteConnection FindOrCreate(string dbName)
        {
            string dbFileName = dbName.Contains(".sqlite") ? dbName : string.Format("C:\\{0}.sqlite", dbName);

            if (!File.Exists(dbFileName))
            {
                SQLiteConnection.CreateFile(dbFileName);
                SQLiteConnection connection = new SQLiteConnection(string.Format("Data Source={0};Version=3;", dbFileName));
                buildTables(connection);
            }

            return new SQLiteConnection(string.Format("Data Source={0};Version=3;", dbFileName));
        }

        private static void buildTables(SQLiteConnection connection)
        {
            using (connection)
            {
                connection.Open();

                using (SQLiteCommand command = new SQLiteCommand(connection))
                {
                    command.CommandType = System.Data.CommandType.Text;
                    command.CommandText = @"
                        Create Table CrimeData (Id int, DateOccured DateTime, CrimeType varchar(50), BlockAddress varchar(50), City varchar(50), Zip varchar(50), IncidentNumber varchar(50), State varchar(50), FormattedAddress varchar(50), Latitude float, Longitude float)
                        ";

                    command.ExecuteNonQuery();
                }
            }
            
        }
    }
}
