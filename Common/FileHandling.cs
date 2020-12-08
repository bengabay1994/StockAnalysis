
namespace StockAnalysis.Common
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Threading;
    using System.Collections.Generic;
    using System.Data;
    using System.Drawing;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    using OfficeOpenXml;
    using OfficeOpenXml.Style;

    using Exceptions;

    public static class FileHandling
    {
        private static string s_ExcelSuffix = ".xlsx";

        private static string s_CsvSuffix = ".csv";

        public static async Task ConvertCsvToXlsxAsync(string pathToFile, string fileName)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            string excelFileName = fileName.Replace(s_CsvSuffix, s_ExcelSuffix);

            string csvFileName = fileName.Replace(s_ExcelSuffix, s_CsvSuffix);

            if (!File.Exists(string.Join("\\", pathToFile, csvFileName)))
            {
                throw new MissingFileException(pathToFile, csvFileName);
            }

            if (File.Exists(string.Join("\\",pathToFile, excelFileName)))
            {
                File.Delete(string.Join("\\", pathToFile, excelFileName));
            }

            IList<string> lineToWrite = await ReadCsvFileAsync(pathToFile, csvFileName).ConfigureAwait(false);

            await WriteToExcelAsync(lineToWrite, pathToFile, excelFileName).ConfigureAwait(false);

        }

        public static Tuple<string, string> SplitToNameAndPath(string absolutePath)
        {
            int splitIndex = absolutePath.LastIndexOf("\\");
            int fileNameLength = absolutePath.Length - 1 - splitIndex;
            int folderPathLength = splitIndex;
            string fileName = absolutePath.Substring(splitIndex + 1, fileNameLength);
            string folderPath = absolutePath.Substring(0, folderPathLength);
            return new Tuple<string, string>(folderPath, fileName);
        }

        public static async Task CreateFavStockExcelAsync()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            string folderPath = Properties.Settings.Default.FavoritStocksExcelLocation;
            string fileName = Properties.Settings.Default.FavoriteStocksExcelName;
            string fileFullPath = string.Join("\\", folderPath, fileName);

            if (string.IsNullOrEmpty(folderPath))
            {
                throw new MissConfigurationException(nameof(Properties.Settings.Default.FavoritStocksExcelLocation));
            }
            if(File.Exists(fileFullPath))
            {
                return;
            }
            
            var excelFileInfo = new FileInfo(fileFullPath);

            using (ExcelPackage excelPackage = new ExcelPackage(excelFileInfo))
            {
                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("Sheet1");

                worksheet.Cells["A1:A2"].Merge = true;
                worksheet.Cells["B1:B2"].Merge = true;
                worksheet.Cells["C1:E1"].Merge = true;
                worksheet.Cells["F1:H1"].Merge = true;
                worksheet.Cells["I1:K1"].Merge = true;
                worksheet.Cells["L1:N1"].Merge = true;
                worksheet.Cells["O1:Q1"].Merge = true;
                worksheet.Cells["R1:R2"].Merge = true;
                worksheet.Cells["S1:S2"].Merge = true;
                worksheet.Cells["T1:T2"].Merge = true;
                worksheet.Cells["U1:U2"].Merge = true;
                worksheet.Cells["V1:V2"].Merge = true;

                List<string> modelThickBorderRanges = new List<string>()
                {
                    "A1:A10000", "A1:A2", "B1:B10000", "B1:B2", "C1:E10000", "C1:E2",
                    "F1:H10000", "F1:H2", "I1:K10000", "I1:K2", "L1:N10000", "L1:N2",
                    "O1:Q10000", "O1:Q2", "R1:R10000", "R1:R2", "S1:S10000", "S1:S2",
                    "T1:T10000", "T1:T2", "U1:U10000", "U1:U2", "V1:V10000", "V1:V2",
                };

                List<string> modelBottomMediumBorderRanges = new List<string>()
                {
                    "C1:E1", "F1:H1", "I1:K1", "L1:N1", "O1:Q1"
                };

                WriteToCenterCell("Business Name", 1, 1, worksheet);
                WriteToCenterCell("Symbol", 1, 2, worksheet);
                WriteToCenterCell("ROIC", 1, 3, worksheet);
                WriteToCenterCell("Equity", 1, 6, worksheet);
                WriteToCenterCell("EPS", 1, 9, worksheet);
                WriteToCenterCell("Sales", 1, 12, worksheet);
                WriteToCenterCell("Cash or Operationg Cash", 1, 15, worksheet);
                WriteToCenterCell("Intrinsic Value", 1, 18, worksheet);
                WriteToCenterCell("MOS Price", 1, 19, worksheet);
                WriteToCenterCell("Price Of Stock", 1, 20, worksheet);
                WriteToCenterCell("Last Update", 1, 21, worksheet);
                WriteToCenterCell("Is Cash", 1, 22, worksheet);

                int[] years = {10, 5, 1};

                for(int col = 3; col < 18; col++)
                {
                    WriteToCenterCell(years[col % 3], 2, col, worksheet);
                }

                ReSizeInitCells(worksheet);

                // Formating

                worksheet.Cells["U3:U10000"].Style.Numberformat.Format = "dd-mm-yyyy";
                worksheet.Cells["C3:Q10000"].Style.Numberformat.Format = "0.00%";

                var rngForColorCondition = worksheet.Cells["C3:Q10000"];
                var condition3 = worksheet.ConditionalFormatting.AddExpression(rngForColorCondition);
                condition3.Style.Fill.PatternType = ExcelFillStyle.Solid;
                condition3.Style.Fill.BackgroundColor.Color = Color.Transparent;
                condition3.Formula = "IF(ISBLANK(C3),1,0)";

                var condition = worksheet.ConditionalFormatting.AddExpression(rngForColorCondition);
                condition.Style.Fill.PatternType = ExcelFillStyle.Solid;
                condition.Style.Fill.BackgroundColor.Color = ColorTranslator.FromHtml("#00FF00");
                condition.Formula = "IF(C3>=0.1,1,0)";

                var condition2 = worksheet.ConditionalFormatting.AddExpression(rngForColorCondition);
                condition2.Style.Fill.PatternType = ExcelFillStyle.Solid;
                condition2.Style.Fill.BackgroundColor.Color = Color.Red;
                condition2.Formula = "IF(AND(C3<0.1,NOT(ISBLANK(C3))),1,0)";

                // borders

                rngForColorCondition.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                rngForColorCondition.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                rngForColorCondition.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                rngForColorCondition.Style.Border.Left.Style = ExcelBorderStyle.Thin;

                foreach (var range in modelThickBorderRanges)
                {
                    var modelTable = worksheet.Cells[range];
                    modelTable.Style.Border.BorderAround(ExcelBorderStyle.Thick);
                }

                foreach (var range in modelBottomMediumBorderRanges)
                {
                    var modelTable = worksheet.Cells[range];
                    modelTable.Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                }

                worksheet.Cells[2, 3].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                worksheet.Cells[2, 4].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                worksheet.Cells[2, 6].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                worksheet.Cells[2, 7].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                worksheet.Cells[2, 9].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                worksheet.Cells[2, 10].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                worksheet.Cells[2, 12].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                worksheet.Cells[2, 13].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                worksheet.Cells[2, 15].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                worksheet.Cells[2, 16].Style.Border.Right.Style = ExcelBorderStyle.Medium;

                worksheet.Cells["A1:V10000"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet.Cells["A1:V10000"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                await excelPackage.SaveAsAsync(excelFileInfo);
            }
        }

        private static int FindLineToSaveIn(string symbol)
        {
            if (string.IsNullOrWhiteSpace(Properties.Settings.Default.FavoritStocksExcelLocation))
            {
                throw new MissConfigurationException(nameof(Properties.Settings.Default.FavoritStocksExcelLocation));
            }
            string filePath = string.Join("\\", Properties.Settings.Default.FavoritStocksExcelLocation, Properties.Settings.Default.FavoriteStocksExcelName);

            FileInfo excelFileInfo = new FileInfo(filePath);

            string fileName, folder;

            (folder, fileName) = SplitToNameAndPath(filePath);

            if (!excelFileInfo.Exists)
            {
                throw new MissingFileException(folder, fileName);
            }

            using (ExcelPackage excelPackage = new ExcelPackage(excelFileInfo))
            {
                var ws = excelPackage.Workbook.Worksheets[0];

                int line = 1;

                while (true)
                {
                    if(string.IsNullOrWhiteSpace(ws.Cells[line, 2].Value.ToString()) || string.Equals(symbol, ws.Cells[line, 2].Value.ToString(), StringComparison.OrdinalIgnoreCase))
                    {
                        return line;
                    }

                    line++;
                }
            }
        }

        private static async Task WriteToExcelAsync(IList<string> linesToWrite, string pathToFile, string fileName)
        {
            var excelFileInfo = new FileInfo(string.Join("\\",pathToFile, fileName));

            using (ExcelPackage excelPackage = new ExcelPackage(excelFileInfo))
            {
                int row = 1, colum = 1;
                ExcelWorksheet workSheet;
                if (excelPackage.Workbook.Worksheets.Count == 0)
                {
                    workSheet = excelPackage.Workbook.Worksheets.Add("Sheet1");
                }
                else
                {
                    workSheet = excelPackage.Workbook.Worksheets[0];
                }
                foreach (string line in linesToWrite)
                {
                    IList<string> words = line.Split(",");
                    foreach (var word in words)
                    {
                        float num;
                        bool isConverted = float.TryParse(word, out num);
                        if (isConverted)
                        {
                            workSheet.Cells[row, colum].Value = num;
                        }
                        else
                        {
                            workSheet.Cells[row, colum].Value = word;
                        }
                        colum++;
                    }
                    colum = 1;
                    row++;
                }

                await excelPackage.SaveAsAsync(excelFileInfo).ConfigureAwait(false);
            }
        }

        // may throw file Could not be Found.
        private static async Task<IList<string>> ReadCsvFileAsync(string pathToFile, string fileName)
        {
            string fileText;

            using (StreamReader streamReader = new StreamReader(string.Join("\\",pathToFile, fileName)))
            {
                fileText = await streamReader.ReadToEndAsync().ConfigureAwait(false);
            }

            IList<string> lines = fileText.Split("\n");

            lines = lines.Select(line => Regex.Replace(line, @"\r|\n", "")).ToList();

            return lines = lines.Select(line => Regex.Replace(line, @"""([-0-9]*),([0-9]*)""", @"$1$2")).ToList();
        }

        public static void DownloadKeyRatioFile(string stockSymbol)
        {
            WebBrowser browser = new WebBrowser();
            browser.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(Browser_DocumentCompleted);
            browser.ScriptErrorsSuppressed = true;
            browser.Navigate(new Uri(CreateDownloadCSVLink(stockSymbol)));
        }

        private static void Browser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            WebBrowser browser = (WebBrowser)sender;

            if (browser.Url == e.Url)
            {
                var doc = browser.Document;
                if (doc.Title.Contains("Page Not Found", StringComparison.OrdinalIgnoreCase))
                {
                    MessageBox.Show("Stock Symbol doesn't exist, please check your symbol");
                    return;
                }
                var col = doc.GetElementById("financeWrap");
                HtmlElement downloadButton = doc.CreateElement("a");
                downloadButton.SetAttribute("className", "large_button");
                downloadButton.SetAttribute("href", "javascript:exportKeyStat2CSV();");
                col.AppendChild(downloadButton);
                downloadButton.InvokeMember("Click");
            }
            else 
            {
                MessageBox.Show("Failed to Get the data for the specific stock, due to a connection error.");
            }
        }

        private static string CreateDownloadCSVLink(string stockSymbol)
        {
            return $"https://financials.morningstar.com/ratios/r.html?t={stockSymbol}&region=usa&culture=en-US";
        }

        private static void WriteToCenterCell(object text, int row, int col, ExcelWorksheet worksheet)
        {
            worksheet.Cells[row, col].Value = text;
            worksheet.Cells[row, col].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            worksheet.Cells[row, col].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
        }

        private static void ReSizeInitCells(ExcelWorksheet workSheet)
        {
            workSheet.Column(1).Width = 27.86;
            workSheet.Column(2).Width = 13.57;
            workSheet.Column(18).Width = 13.57;
            workSheet.Column(18).Width = 13.57;
            workSheet.Column(19).Width = 10.71;
            workSheet.Column(20).Width = 15;
            workSheet.Column(21).Width = 15;
        }
    }
}
