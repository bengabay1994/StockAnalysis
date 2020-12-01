using System;
using System.Collections.Generic;
using System.Text;

namespace StockAnalysis.Extensions
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    using Microsoft.Extensions.Configuration;

    using Exceptions;

    public static class ConfigurationRootExt
    {
        private static readonly JsonSerializerSettings s_setting = new JsonSerializerSettings()
        {
            ContractResolver = (IContractResolver)new CamelCasePropertyNamesContractResolver()
        };

        public static T Read<T>(this IConfigurationRoot config)
        {
            string? key = typeof(T).Name;

            return JsonConvert.DeserializeObject<T>(config[key] ?? throw new MissConfigurationException(key), s_setting);
        }
    }
}
