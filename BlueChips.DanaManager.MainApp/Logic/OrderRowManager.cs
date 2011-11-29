using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BlueChips.DanaManager.MainApp.Models;
using BlueChips.DanaManager.MainApp.Libs;
using Excel;
using System.Data;

namespace BlueChips.DanaManager.MainApp.Logic
{
    public class OrderRowManager
    {
        public List<OrderRow> ParseExcelFile(string fileName) {

            var extension = System.IO.Path.GetExtension(fileName).ToLower();

            IExcelDataReader excelReader = null;
            try {
                using (var stream = System.IO.File.OpenRead(fileName)) {
                    if (extension.Contains("xlsx")) {
                        excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                    } else if (extension.Contains("xls")) {
                        excelReader = ExcelReaderFactory.CreateBinaryReader(stream);
                    }

                    excelReader.IsFirstRowAsColumnNames = true;

                    if (!String.IsNullOrWhiteSpace(excelReader.ExceptionMessage)) {
                        throw new PublicMessageException(excelReader.ExceptionMessage, excelReader.ExceptionMessage);
                    }

                    var dataSet = excelReader.AsDataSet();

                    if (dataSet.Tables.Count == 0) {
                        throw new PublicMessageException("No table present", "File excel vuoto");
                    }

                    var table = dataSet.Tables[0];

                    CheckColumns(table);

                    var rows = table.Rows.Cast<DataRow>().Select(row => new OrderRow {
                        ItemId = row["Item"] as string,
                        OrderId = row["Ordine"] as string,
                        Description = row["Descrizione"] as string,
                        Qty = row["Qty"].AsInt(0).Value,
                        ExpectedDate = row["Dt_CONS"].ConvertToDateTime("dd/MM/yyyy"),
                        DdtCode = row["DDT"] as string,
                        DdtDate = row["DDT_DATE"].AsDateTime("dd/MM/yyyy"),
                        DdtQty = (row["DDT_QTY"] as string).AsInt()
                    });

                    var result = rows.Where(r => r.OrderId == null || r.OrderId.ToUpper() != "PLANNED").ToList();
                    return result;
                }
            } catch (PublicMessageException) {
                throw;
            } catch(Exception exception) {
                throw new PublicMessageException("Error in importing excel data","Errore nell'importazione del file DANA",exception);
            }
        }   

        private void CheckColumns(DataTable table)
        {
            string[] columns = { "Item", "Ordine", "Descrizione", "Qty", "Dt_CONS", "DDT", "DDT_DATE", "DDT_QTY" };
            var missingColumns = columns.Where(col => !table.Columns.Contains(col));
            if (missingColumns.Any()) { 
                var columnsList = String.Join(", ",missingColumns);
                throw new PublicMessageException("Missing columns: " + columnsList, "Colonne non trovate: " + columnsList);
            }
        }
    }
}
