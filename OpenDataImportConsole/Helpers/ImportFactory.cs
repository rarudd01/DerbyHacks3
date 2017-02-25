using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenDataImportConsole
{
    public static class ImportFactory
    {
        public static IDataImporter Create(string importSource)
        {
            switch (importSource)
            {
                case "Excel":
                    return new Excelmporter();
                case "API":
                    return new ApiImporter();
                default:
                    throw new InvalidOperationException("Invalid data importer selected.");
            }
        }
    }
}
