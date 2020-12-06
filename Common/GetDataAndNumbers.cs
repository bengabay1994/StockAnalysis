
namespace StockAnalysis.Common
{
    using System;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using System.IO;
    using System.Windows.Forms;

    using OfficeOpenXml;

    using Exceptions;

    public static class GetDataAndNumbers
    {
        //private static int s_NumberOfDataColumns = 11;

        private static int s_NumberOfParameters = 6;

        private static int s_LastDataColumn = 12;

        private static int s_MaxStraightEmptyLines = 5;

        public static async Task<Tuple<Dictionary<StocksEnums.BigFiveNumbersDicKey, IList<string>>, Dictionary<StocksEnums.GrowthNumbersDicKey, IList<string>>>> GetStockDataAsync(string absoluteFilePath, string fileName = null, string folder = null)
        {
            string fn = fileName;
            string fold = folder;

            if (string.IsNullOrWhiteSpace(fn) || string.IsNullOrWhiteSpace(fold))
            {
                (fold, fn) = FileHandling.SplitToNameAndPath(absoluteFilePath);
            }

            var BigFive = await GetBigFiveNumbersAsync(absoluteFilePath, fold, fn).ConfigureAwait(false);

            var BigGrowths = await GetBigFiveGrowth(BigFive).ConfigureAwait(false);

            return new Tuple<Dictionary<StocksEnums.BigFiveNumbersDicKey, IList<string>>, Dictionary<StocksEnums.GrowthNumbersDicKey, IList<string>>>(BigFive, BigGrowths);
        }

        public static void ShowStockData(ref Dictionary<StocksEnums.BigFiveNumbersDicKey, IList<string>> BigFive, ref Dictionary<StocksEnums.GrowthNumbersDicKey, IList<string>> BigGrowths, TableLayoutPanel p1_LayoutPanel)
        {
            for (int row = 2; row <= 7; row++)
            {
                for (int column = 1; column <= 11; column++)
                {
                    string text = BigFive[(StocksEnums.BigFiveNumbersDicKey)row][column - 1];
                    p1_LayoutPanel.Controls.Find($"templ{row}{column}", false)[0].Text = text == null ? "N/A" : text;
                }
            }

            for (int row = 10; row <= 12; row++)
            {
                for (int column = 1; column <= 7; column++)
                {
                    if (column == 6)
                    {
                        continue;
                    }
                    string text = BigGrowths[(StocksEnums.GrowthNumbersDicKey)column][row - 10];
                    p1_LayoutPanel.Controls.Find($"templ{row}{column}", false)[0].Text = text == null ? "N/A" : text;
                }
            }
        }

        private static double? CalculateGrowth(double? oldVal, double? currentVal, int years)
        {
            if (oldVal == null || currentVal == null)
            {
                return null;
            }
            if (oldVal <= 0.0 && currentVal > 0.0)
            {
                return 999.9;
            }
            if (oldVal > 0.0 && currentVal <= 0.0)
            {
                return -999.9;
            }

            var gr = Math.Pow((double)currentVal / (double)oldVal, 1.0 / (double)years) - 1;

            return gr * 100;
        }

        private static async Task<Dictionary<StocksEnums.BigFiveNumbersDicKey, IList<string>>> GetBigFiveNumbersAsync(string filePath, string folder = null, string fileName = null)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            Dictionary<StocksEnums.BigFiveNumbersDicKey, IList<string>> BigFiveNumbers = new Dictionary<StocksEnums.BigFiveNumbersDicKey, IList<string>>();

            FileInfo fileInfo = new FileInfo(filePath);
            if (!fileInfo.Exists)
            {
                throw new MissingFileException(fileName, folder);
            }
            using (ExcelPackage excelPackage = new ExcelPackage(fileInfo))
            {
                var sheet = excelPackage.Workbook.Worksheets[0];

                int foundCat = 0;

                int RevenueLineNumber = 0, EpsLineNumber = 0, BookValueLineNumber = 0, FreeCashFlowLineNumber = 0, OperatingCashFlowLineNumber = 0, RoicLineNumber = 0;

                int StraightEmptyLines = 0, line = 1;
                while (StraightEmptyLines < s_MaxStraightEmptyLines)
                {
                    line++;
                    string? cellValue = (string)sheet.Cells[line, 1].Value;

                    if (cellValue == null)
                    {
                        StraightEmptyLines++;
                        continue;
                    }
                    if (RevenueLineNumber == 0 && cellValue.StartsWith("Revenue"))
                    {
                        foundCat++;
                        RevenueLineNumber = line;
                    }
                    else if (EpsLineNumber == 0 && cellValue.StartsWith("Earnings Per Share"))
                    {
                        foundCat++;
                        EpsLineNumber = line;
                    }
                    else if (BookValueLineNumber == 0 && cellValue.StartsWith("Book Value Per Share"))
                    {
                        foundCat++;
                        BookValueLineNumber = line;
                    }
                    else if (OperatingCashFlowLineNumber == 0 && cellValue.StartsWith("Operating Cash Flow"))
                    {
                        foundCat++;
                        OperatingCashFlowLineNumber = line;
                    }
                    else if (FreeCashFlowLineNumber == 0 && cellValue.StartsWith("Free Cash Flow"))
                    {
                        foundCat++;
                        FreeCashFlowLineNumber = line;
                    }
                    else if (RoicLineNumber == 0 && cellValue.StartsWith("Return on Invested Capital"))
                    {
                        foundCat++;
                        RoicLineNumber = line;
                    }
                    StraightEmptyLines = 0;
                }

                if (foundCat < s_NumberOfParameters)
                {
                    throw new MissingCategoryException();
                }

                IList<string?> Revenues = await ParameterValues(RevenueLineNumber, sheet).ConfigureAwait(false);
                IList<string?> Eps = await ParameterValues(EpsLineNumber, sheet).ConfigureAwait(false);
                IList<string?> BookValues = await ParameterValues(BookValueLineNumber, sheet).ConfigureAwait(false);
                IList<string?> OperatingCashFlow = await ParameterValues(OperatingCashFlowLineNumber, sheet).ConfigureAwait(false);
                IList<string?> FreeCashFlow = await ParameterValues(FreeCashFlowLineNumber, sheet).ConfigureAwait(false);
                IList<string?> Roic = await ParameterValues(RoicLineNumber, sheet).ConfigureAwait(false);

                BigFiveNumbers.Add(StocksEnums.BigFiveNumbersDicKey.Revenue, Revenues);
                BigFiveNumbers.Add(StocksEnums.BigFiveNumbersDicKey.Eps, Eps);
                BigFiveNumbers.Add(StocksEnums.BigFiveNumbersDicKey.BookValue, BookValues);
                BigFiveNumbers.Add(StocksEnums.BigFiveNumbersDicKey.OperatingCashFlow, OperatingCashFlow);
                BigFiveNumbers.Add(StocksEnums.BigFiveNumbersDicKey.FreeCashFlow, FreeCashFlow);
                BigFiveNumbers.Add(StocksEnums.BigFiveNumbersDicKey.Roic, Roic);
            }

            return BigFiveNumbers;
        }

        private static Task<IList<string?>> ParameterValues(int lineNumber, ExcelWorksheet sheet)
        {
            return Task.Run(() =>
            {
                if (lineNumber < 1)
                {
                    return null;
                }

                List<string?> parameterValues = new List<string?>();

                int count = 2;

                while (count <= s_LastDataColumn)
                {
                    var check = sheet.Cells[lineNumber, count++].Value;
                    if (check == null)
                    {
                        parameterValues.Add(null);
                        continue;
                    }
                    string checks = check.ToString();
                    double val = 0.0;
                    if (double.TryParse(checks, out val))
                    {
                        parameterValues.Add(val.ToString("0.##"));
                    }
                    else
                    {
                        throw new BadOrCorruptedFileException($"found a non double value in line: {lineNumber}");
                    }

                }

                return (IList<string?>)parameterValues;
            });
        }

        private static async Task<Dictionary<StocksEnums.GrowthNumbersDicKey, IList<string>>> GetBigFiveGrowth(Dictionary<StocksEnums.BigFiveNumbersDicKey, IList<string>> BigFiveNumbers)
        {
            Dictionary<StocksEnums.GrowthNumbersDicKey, IList<string>> BigFiveGrowth = new Dictionary<StocksEnums.GrowthNumbersDicKey, IList<string>>();

            IList<string?> Revenues = await GetAllGrowth(BigFiveNumbers[StocksEnums.BigFiveNumbersDicKey.Revenue]).ConfigureAwait(false);
            IList<string?> Eps = await GetAllGrowth(BigFiveNumbers[StocksEnums.BigFiveNumbersDicKey.Eps]).ConfigureAwait(false);
            IList<string?> BookValues = await GetAllGrowth(BigFiveNumbers[StocksEnums.BigFiveNumbersDicKey.BookValue]).ConfigureAwait(false);
            IList<string?> OperatingCashFlow = await GetAllGrowth(BigFiveNumbers[StocksEnums.BigFiveNumbersDicKey.OperatingCashFlow]).ConfigureAwait(false);
            IList<string?> FreeCashFlow = await GetAllGrowth(BigFiveNumbers[StocksEnums.BigFiveNumbersDicKey.FreeCashFlow]).ConfigureAwait(false);
            IList<string?> Roic = await GetAllAverage(BigFiveNumbers[StocksEnums.BigFiveNumbersDicKey.Roic]).ConfigureAwait(false);

            BigFiveGrowth.Add(StocksEnums.GrowthNumbersDicKey.Revenue, Revenues);
            BigFiveGrowth.Add(StocksEnums.GrowthNumbersDicKey.Eps, Eps);
            BigFiveGrowth.Add(StocksEnums.GrowthNumbersDicKey.BookValue, BookValues);
            BigFiveGrowth.Add(StocksEnums.GrowthNumbersDicKey.OperatingCashFlow, OperatingCashFlow);
            BigFiveGrowth.Add(StocksEnums.GrowthNumbersDicKey.FreeCashFlow, FreeCashFlow);
            BigFiveGrowth.Add(StocksEnums.GrowthNumbersDicKey.Roic, Roic);

            return BigFiveGrowth;
        }

        private static Task<IList<string?>> GetAllAverage(IList<string> Numbers)
        {
            return Task.Run(() =>
            {
                var saveLastElem = Numbers[^1];
                Numbers.RemoveAt(Numbers.Count - 1);
                List<string?> Avg = new List<string?>();
                List<string> midAvgList = Numbers as List<string>;
                midAvgList = midAvgList.GetRange(5, 5);

                double tmp;

                double? totalAvg = 0.0;
                int count = 0;
                foreach (var num in Numbers)
                {
                    if (double.TryParse(num, out tmp))
                    {
                        count++;
                        totalAvg += tmp;
                    }
                }
                if (count > 0)
                {
                    totalAvg /= count;
                }
                else
                {
                    totalAvg = null;
                }

                Avg.Add(totalAvg == null ? null : ((double)totalAvg).ToString("0.##"));

                double? midAvg = 0.0;
                count = 0;
                foreach (var num in midAvgList)
                {
                    if (double.TryParse(num, out tmp))
                    {
                        count++;
                        midAvg += tmp;
                    }
                }
                if (count > 0)
                {
                    midAvg /= count;
                }
                else
                {
                    midAvg = null;
                }

                Avg.Add(midAvg == null ? null : ((double)midAvg).ToString("0.##"));

                double? lastRoic;
                if (double.TryParse(Numbers[^2], out tmp))
                {
                    lastRoic = tmp;
                }
                else
                {
                    lastRoic = null;
                }

                Avg.Add(lastRoic == null ? null : ((double)lastRoic).ToString("0.##"));

                Numbers.Add(saveLastElem);
                return (IList<string?>)Avg;
            });
        }

        private static Task<IList<string?>> GetAllGrowth(IList<string> Numbers)
        {
            return Task.Run(() =>
            {
                List<string?> Growths = new List<string?>();
                double? oldVal9;
                double? oldVal5;
                double? oldVal1;
                double? currVal;
                double tmp;

                if (!double.TryParse(Numbers[0], out tmp))
                {
                    oldVal9 = null;
                }
                else
                {
                    oldVal9 = tmp;
                }
                if (!double.TryParse(Numbers[4], out tmp))
                {
                    oldVal5 = null;
                }
                else
                {
                    oldVal5 = tmp;
                }
                if (!double.TryParse(Numbers[^3], out tmp))
                {
                    oldVal1 = null;
                }
                else
                {
                    oldVal1 = tmp;
                }
                if (!double.TryParse(Numbers[^2], out tmp))
                {
                    currVal = null;
                }
                else
                {
                    currVal = tmp;
                }

                var growth9 = CalculateGrowth(oldVal9, currVal, 9);
                var growth5 = CalculateGrowth(oldVal5, currVal, 5);
                var growth1 = CalculateGrowth(oldVal1, currVal, 1);

                Growths.Add(growth9 == null ? null : ((double)growth9).ToString("0.##"));
                Growths.Add(growth5 == null ? null : ((double)growth5).ToString("0.##"));
                Growths.Add(growth1 == null ? null : ((double)growth1).ToString("0.##"));

                return (IList<string?>)Growths;
            });
        }
    }
}
