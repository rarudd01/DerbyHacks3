using DerbyHacks.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace OpenDataImportConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            string config = ConfigurationManager.AppSettings["ImportSource"];
            string apiKey = ConfigurationManager.AppSettings["ApiKey"];

            IDataImporter importer = ImportFactory.Create(config);

            List<CrimeData> models = importer.Import(DataModelType.CrimeData).ToList() ;

            Console.WriteLine(string.Format("{0} data models imported.", models.Count));

            Console.WriteLine("Beginning geocoding enrichment...");
            DataEnrichment.Geocode(apiKey, models.First());
            Console.WriteLine("Enrichment complete.");

            DataHelper helper = new DataHelper();
            try
            {
                helper.Insert(models);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            

            Console.ReadKey();
        }
    }
}
