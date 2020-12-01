using System;
using System.Collections.Generic;
using System.Text;

namespace StockAnalysis.Exceptions
{
    public class MissConfigurationException : Exception
    {
        public MissConfigurationException() : base("Can't find configurations in settings")
        {
        }

        public MissConfigurationException(string confName) : base ($"Can't find {confName} configurations in settings")
        {
        }
    }
}
