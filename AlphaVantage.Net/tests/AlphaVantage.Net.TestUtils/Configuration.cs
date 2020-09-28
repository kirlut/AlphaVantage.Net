using System;
using Microsoft.Extensions.Configuration;

namespace AlphaVantage.Net.TestUtils
{
    public static class ConfigProvider
    {
        public static IConfiguration Configuration { get; private set; }
        
        static ConfigProvider()
        {
            Configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("secrets.json", false, true)
                .Build();
        }
    }
}