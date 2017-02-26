using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DerbyHacks.Model;

namespace OpenDataImportConsole
{
    public class ApiImporter : IDataImporter
    {
        public IEnumerable<CrimeData> Import(DataModelType dataModelType)
        {
            throw new NotImplementedException();
        }
    }
}
