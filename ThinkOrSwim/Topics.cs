using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThinkOrSwim
{
    class Topics
    {
        ConcurrentDictionary<Int64, Tuple<string, string>> data = new ConcurrentDictionary<Int64, Tuple<string, string>>();

        internal Topics()
        {

        }

        public int Count()
        {
            return this.data.Count();
        }

        public Tuple<string, string> Get(int id)
        {
            return this.data[id];
        }

        public void Add(Int16 id, string symbol, string type)
        {
            this.data[id] = new Tuple<string, string>(symbol, type);
        }
    }
}
