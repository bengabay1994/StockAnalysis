

namespace StockAnalysis.Exceptions
{
    using System;

    public class MissingCategoryException : Exception
    {
        public MissingCategoryException() : base($"The file is missing at least one category, that may be a wrong file.")
        {
        }
    }
}
