
namespace StockAnalysis.Exceptions
{
    using System;

    public class MissingFileException : Exception
    {
        public MissingFileException() : base("File is missing")
        {
        }

        public MissingFileException(string folder, string fileName) : base($"File {fileName} is missing in folder {folder}")
        {
        }
    }
}
