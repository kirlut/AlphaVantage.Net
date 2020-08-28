using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace AlphaVantage.Net.Core.Parsing
{
    public static class MetaDataExtractor
    {
        private const string MetaDataToken = "Meta Data";
        
        public static Dictionary<string, string> ExtractMetaData(this JsonDocument jsonDocument)
        {
            var result = new Dictionary<string, string>();

            var jsonProperties = jsonDocument.RootElement.EnumerateObject();
            if (jsonProperties.Any(property => property.Name.Contains(MetaDataToken)) == false) return result;
            
            var metaDataProperty = jsonProperties.FirstOrDefault(property => property.Name.Contains(MetaDataToken));

            if (metaDataProperty.Name.Contains(MetaDataToken) == false) return result;

            foreach (var metaData in metaDataProperty.Value.EnumerateObject())
            {
                result.Add(metaData.Name, metaData.Value.GetString());
            }
            
            return result;
        }
    }
}