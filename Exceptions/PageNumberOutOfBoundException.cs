
namespace StockAnalysis.Exceptions
{
    using System;

    public class PageNumberOutOfBoundException : Exception
    {
        public PageNumberOutOfBoundException()
        {
        }

        public PageNumberOutOfBoundException(int pageNumber) : base($"Page number: {pageNumber} is out of Bounds")
        {
        }
    }
}
