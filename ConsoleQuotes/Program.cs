using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThinkOrSwim;

namespace ConsoleQuotes
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new Client();
            client.Add("SPX", QuoteType.Last);

            foreach(var quote in client.Quotes())
            {
                Console.WriteLine("{0} {1}: ${2}", quote.Symbol, quote.Type, quote.Value);
            }
        }
    }
}
