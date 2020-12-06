
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

    using Extensions;
    using Exceptions;

    public static class FileHandeling
    {
        private static string s_ExcelSuffix = ".xlsx";

        private static string s_CsvSuffix = ".csv";

        public static async Task ConvertCsvToXlsxAsync(string pathToFile, string fileName)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            string excelFileName = fileName.Replace(s_CsvSuffix, s_ExcelSuffix);

            if(File.Exists(string.Join("\\",pathToFile, excelFileName)))
            {
                File.Delete(string.Join("\\", pathToFile, excelFileName));
            }

            IList<string> lineToWrite = await ReadCsvFileAsync(pathToFile, fileName).ConfigureAwait(false);

            await WriteToExcelAsync(lineToWrite, pathToFile, excelFileName).ConfigureAwait(false);

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
    }
}
