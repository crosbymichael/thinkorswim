using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThinkOrSwim
{
    public class Quote
    {
        public string Symbol { get; protected set; }
        public string Type { get; protected set; }
        public double Value { get; protected set; }

        internal Quote(string symbol, string type, double value)
        {
            this.Symbol = symbol;
            this.Value = value;
            this.Type = type;
        }
    }
}
