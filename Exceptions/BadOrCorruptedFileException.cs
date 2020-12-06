
namespace StockAnalysis.Exceptions
{
    using System;

    public class BadOrCorruptedFileException : Exception
    {
        public BadOrCorruptedFileException() : base($"The file is wrong or Currupted")
        {
        }

        public BadOrCorruptedFileException(string msg) : base($"The file is wrong or Currupted, {msg}")
        {
        }
    }
}
