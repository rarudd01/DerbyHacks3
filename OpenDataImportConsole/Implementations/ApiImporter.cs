using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DerbyHacks.Data;

namespace OpenDataImportConsole
{
    public class ApiImporter : IDataImporter
    {
        public IEnumerable<IDataModel> Import(DataModelType dataModelType)
        {
            throw new NotImplementedException();
        }
    }
}
