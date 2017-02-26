using DerbyHacks.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenDataImportConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            string config = ConfigurationManager.AppSettings["ImportSource"];
            string apiKey = ConfigurationManager.AppSettings["ApiKey"];

            IDataImporter importer = ImportFactory.Create(config);

            List<IDataModel> models = importer.Import(DataModelType.CrimeData).ToList();

            Console.WriteLine(string.Format("{0} data models imported.", models.Count));

            Console.WriteLine("Beginning geocoding enrichment...");
            DataEnrichment.Geocode(apiKey, (CrimeData)models.First());
            Console.WriteLine("Enrichment complete.");



            Console.ReadKey();
        }
    }
}
