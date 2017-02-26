using DerbyHacks.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenDataImportConsole
{
    public interface IDataImporter
    {
        IEnumerable<CrimeData> Import(DataModelType dataModelType);
    }

    public enum DataModelType
    {
        CrimeData
    }
}
