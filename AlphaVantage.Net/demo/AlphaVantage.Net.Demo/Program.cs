using System;
using System.Threading.Tasks;

namespace AlphaVantage.Net.Demo
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await Demo.CoreDemo();
            await Demo.StocksDemo();
            await Demo.ForexDemo();
            await Demo.CryptoDemo();
            await Demo.TechIndicatorsDemo();
            
            Console.WriteLine("Hello World!");
        }
    }
}