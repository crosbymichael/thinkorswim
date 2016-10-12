using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThinkOrSwim
{
    class Queue : IEnumerator, IEnumerator<Quote>
    {
        BlockingCollection<Quote> queue = new BlockingCollection<Quote>(new ConcurrentQueue<Quote>());
        Quote current;

        internal Queue()
        {
            
        }

        internal void Disconnect()
        {
            this.queue.CompleteAdding();
        }

        internal void Push(Quote quote)
        {
            this.queue.Add(quote);
        }

        public Quote Current
        {
            get
            {
                return this.current;
            }
        }

        object IEnumerator.Current
        {
            get
            {
                return this.current;
            }
        }

        public void Dispose()
        {
            this.queue.Dispose();
        }

        public bool MoveNext()
        {
            if (this.queue.IsCompleted)
            {
                return false;
            }
            this.current = this.queue.Take();
            return true;
        }

        public void Reset()
        {
     
        }
    }
}
