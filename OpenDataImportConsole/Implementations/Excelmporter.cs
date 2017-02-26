using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DerbyHacks.Model;

namespace OpenDataImportConsole
{
    public class Excelmporter : IDataImporter
    {
        public IEnumerable<CrimeData> Import(DataModelType modelType)
        {
            switch (modelType)
            {
                case DataModelType.CrimeData:
                    return ImportCrimeData();
                default:
                    throw new InvalidOperationException("Invalid model type selected.");
            }
        }

        private IEnumerable<CrimeData> ImportCrimeData()
        {
            string filePath = "data.xlsx";

            DataTable dt = GetDataTableFromExcel(filePath);

            List<CrimeData> items = new List<CrimeData>();
            foreach (DataRow row in dt.Rows)
            {
                CrimeData item = new CrimeData();
                int id;
                if (Int32.TryParse(row["ID"].ToString(), out id))
                {
                    DateTime date;
                    if (DateTime.TryParse(row["DATE_OCCURED"].ToString(), out date))
                    {
                        item.DateOccured = date;
                    }
                    item.CrimeType = row["CRIME_TYPE"].ToString();
                    item.BlockAddress = row["BLOCK_ADDRESS"].ToString();
                    item.City = row["CITY"].ToString();
                    item.Zip = row["ZIP_CODE"].ToString();
                    item.IncidentNumber = row["INCIDENT_NUMBER"].ToString();

                    items.Add(item);
                }
            }

            return items;
        }

        private static DataTable GetDataTableFromExcel(string path, bool hasHeader = true)
        {
            using (var pck = new OfficeOpenXml.ExcelPackage())
            {
                using (var stream = File.OpenRead(path))
                {
                    pck.Load(stream);
                }
                var ws = pck.Workbook.Worksheets.First();
                DataTable tbl = new DataTable();
                foreach (var firstRowCell in ws.Cells[1, 1, 1, ws.Dimension.End.Column])
                {
                    tbl.Columns.Add(hasHeader ? firstRowCell.Text : string.Format("Column {0}", firstRowCell.Start.Column));
                }
                var startRow = hasHeader ? 2 : 1;
                for (int rowNum = startRow; rowNum <= ws.Dimension.End.Row; rowNum++)
                {
                    var wsRow = ws.Cells[rowNum, 1, rowNum, ws.Dimension.End.Column];
                    DataRow row = tbl.Rows.Add();
                    foreach (var cell in wsRow)
                    {
                        row[cell.Start.Column - 1] = cell.Text;
                    }
                }
                return tbl;
            }
        }
    }
}
