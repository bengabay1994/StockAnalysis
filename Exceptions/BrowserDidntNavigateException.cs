
namespace StockAnalysis.Exceptions
{
    using System;

    public class BrowserDidntNavigateException : Exception
    {
        public BrowserDidntNavigateException()
        {
        }

        public BrowserDidntNavigateException(string browserUrl, string argsUrl) :
            base($"The browser control is in: {browserUrl}, but should be in: {argsUrl}")
        {
        }
    }
}
