
namespace StockAnalysis.Exceptions
{
    using System;

    public class MissingFileException : Exception
    {
        public MissingFileException() : base("File is missing")
        {
        }

        public MissingFileException(string fileName, string folder) : base($"File {fileName} is missing in folder {folder}")
        {
        }
    }
}
