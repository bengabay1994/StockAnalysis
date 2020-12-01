
namespace StockAnalysis.DataHandling
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Newtonsoft.Json;
    using System.IO;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Configuration.Json;
    using Newtonsoft.Json.Serialization;
    using System.Text.RegularExpressions;
    using System.Collections.Generic;

    using Extensions;
    using Common;

    public class JsonHandling
    {
        private readonly IConfigurationBuilder m_ConfigurationBuilder;

        private readonly IConfigurationRoot m_ConfigurationRoot;

        private readonly string m_JsonFileAbsolutePath = $"..\\Parameters\\.local.settings.json";

        public JsonHandling()
        {
            m_ConfigurationBuilder = new ConfigurationBuilder();

            m_JsonFileAbsolutePath = Path.GetFullPath(m_JsonFileAbsolutePath);

            m_ConfigurationRoot = m_ConfigurationBuilder.Build();
        }

        public T ReadConfiguration<T>()
        {
            return m_ConfigurationRoot.Read<T>();
        }

        public async Task WriteConfigurationAsync<T>(T config, bool isLast)
        {
            string configToWrite = JsonConvert.SerializeObject(config);

            configToWrite = Regex.Replace(configToWrite, "\"", "'");

            configToWrite = String.Concat("\t\"", nameof(T), "\": \"{", configToWrite, "}\"", isLast ? "" : ",");

            string copyFile;

            using (StreamReader reader = new StreamReader(m_JsonFileAbsolutePath)) 
            {
                copyFile = reader.ReadToEnd();
            }

            IList<string> linesToWrite = copyFile.Split("\n");

            linesToWrite = linesToWrite.Select(line => Regex.Replace(line, @"\n|\r", "")).ToList();

            linesToWrite = linesToWrite.Select(line => Regex.Replace(line, "^}\"^{", "'")).ToList();

            linesToWrite[ConfigHelper.GetConfigLineNumber(nameof(T))] = configToWrite;

            await File.WriteAllLinesAsync(m_JsonFileAbsolutePath, linesToWrite).ConfigureAwait(false);
        }
    }
}
