using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThinkOrSwim
{
    public enum QuoteType
    {
        Last,
        Bid,
        Ask
    }

    public class Client : IDisposable
    {
        Feed feed;

        public Client() : this(10)
        {

        }

        public Client(int heartbeat)
        {
            this.feed = new Feed(heartbeat);
        }

        public void Add(string symbol, QuoteType quoteType)
        {
            this.feed.Add(symbol, quoteType.ToString());
        }

        public void Remove(string symbol, QuoteType quoteType)
        {
            this.feed.Remove(symbol, quoteType.ToString());
        }

        public IEnumerable<Quote> Quotes()
        {
            return this.feed;
        }

        public void Dispose()
        {
            this.feed.Stop();
        }
    }
}
