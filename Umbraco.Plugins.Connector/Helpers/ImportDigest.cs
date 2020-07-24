namespace Umbraco.Plugins.Connector.Helpers
{
    using OfficeOpenXml;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Umbraco.Plugins.Connector.Models;

    public class ImportDigest
    {
        private int rowCount = -1;//-1 because it has headers
        public int RowsImported { get { return rowCount; } }

        public int ColumnsImported { get { return Workbook.Worksheets[0].Dimension.End.Column; } }

        public List<ExcelCell> Sheet { get; set; }

        public ValidationErrorList ErrorList { get; }

        /// <summary>
        /// Workbook to process the Import
        /// </summary>
        public ExcelWorkbook Workbook { get; private set; }

        public ImportDigest()
        {
            ErrorList = new ValidationErrorList();
            Sheet = new List<ExcelCell>();
        }

        public ImportDigest(Stream file) : this()
        {
            this.GetData(file);
        }

        /// <summary>
        /// Gets the data from the FileStream (loaded from the Excel File)
        /// </summary>
        /// <param name="file">the Stream with the data</param>
        /// <returns>Returns a complete Workbook object</returns>
        /// <see cref="https://stackoverflow.com/questions/560435/read-excel-file-from-a-stream"/>
        public ExcelWorkbook GetData(Stream file)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                file.CopyTo(ms);
                Workbook = new ExcelPackage(ms).Workbook;
                return Workbook;
            }
        }

        /// <summary>
        /// Import the data from the Spreadsheet
        /// </summary>
        /// <returns>Returns a list of JObjects</returns>
        public virtual List<ExcelCell> Import(bool ignoreRowWithEmptyCells = false)
        {
            if (Workbook == null) throw new MissingMemberException("Missing Workbook");

            var sheet = Workbook.Worksheets.FirstOrDefault();

            if (sheet == null) throw new MissingMemberException("No sheet found");

            for (int i = sheet.Dimension.Start.Row; i <= sheet.Dimension.End.Row; i++)
            {
                for (int j = sheet.Dimension.Start.Column; j <= sheet.Dimension.End.Column; j++)
                {
                    ExcelCell cell = null;
                    if (ignoreRowWithEmptyCells)
                    {

                        if (sheet.Cells[i, j].Value != null && !string.IsNullOrEmpty(sheet.Cells[i, j].Value.ToString()))
                        {
                            continue;
                        }
                        else
                        {
                            cell = new ExcelCell { CellName = sheet.Cells[i, j].Address, CellValue = sheet.Cells[i, j].Value };
                        }
                    }
                    else
                    {
                        cell = new ExcelCell { CellName = sheet.Cells[i, j].Address, CellValue = sheet.Cells[i, j].Value };
                    }

                    try
                    {
                        Sheet.Add(cell);
                    }
                    catch (Exception ex)
                    {
                        ErrorList.AddError(new ValidationErrorItem
                        {
                            Location = sheet.Cells[i, j].Address,
                            Row = i,
                            Exception = ex
                        });
                    }
                }
                rowCount++;
            }

            return Sheet;
        }
    }

}
