using AlphaVantage.Net.Common.Parsing;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace AlphaVantage.Net.Common.Overview
{
    public abstract class OverviewParserBase<TOverview> : IAlphaVantageJsonDocumentParser<TOverview>
        where TOverview : OverviewBase
    {
        public abstract TOverview CreateOverviewInstance();

        public abstract Action<TOverview, string>? GetOverviewParsingDelegate(string fieldName);

        public TOverview ParseApiResponse(JsonDocument jsonDocument)
        {
            var result = CreateOverviewInstance();

            try
            {
               EnrichOverviewData(result, jsonDocument);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }

            return result;
        }

        private void EnrichOverviewData(TOverview overview, JsonDocument overviewFields)
        {
            foreach (var fieldJson in overviewFields.RootElement.EnumerateObject())
            {
                GetOverviewParsingDelegate(fieldJson.Name)?.Invoke(overview, fieldJson.Value.GetString());
            }
        }


    }
}
