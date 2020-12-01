using System;
using System.Collections.Generic;
using System.Text;

namespace StockAnalysis.Common
{
    using Config;

    public class ConfigHelper
    {
        public static int GetConfigLineNumber(string configName)
        {
            switch (configName)
            {
                case nameof(SettingsConfig):
                    return 2;
                default:
                    throw new ArgumentException($"{configName} is not a valid config parameter");
            }
        }
    }
}
