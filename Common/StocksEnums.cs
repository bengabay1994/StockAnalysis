
namespace StockAnalysis.Common
{
    public static class StocksEnums
    {
        public enum Pages
        {
            CalculateStockData = 1,
            CalculateIntrinsicValue = 2,
            Automate = 3,
            UpdateExcel = 4,
            Settings = 5,
            About = 6
        }

        public enum BigFiveNumbersDicKey
        {
            Revenue = 2,
            Eps = 3,
            BookValue = 4,
            FreeCashFlow = 5,
            OperatingCashFlow = 6,
            Roic = 7
        }

        public enum GrowthNumbersDicKey
        {
            Roic = 0,
            BookValue = 1,
            Eps = 2,
            Revenue = 3,
            FreeCashFlow = 4,
            OperatingCashFlow = 6,
        }

        public enum GrowthAndAverageLength 
        {
            One = 2,
            Five = 6,
            Nine = 9,
            LatestTen = 10,
            LatestFive = 5,
            LatestOne = 1
        }
    }
}
