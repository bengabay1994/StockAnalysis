
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
            Roic = 1,
            BookValue = 2,
            Eps = 3,
            Revenue = 4,
            FreeCashFlow = 5,
            OperatingCashFlow = 7,
        }

    }
}
