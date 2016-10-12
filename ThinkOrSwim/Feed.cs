using Microsoft.Win32;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Security.Cryptography;

namespace ThinkOrSwim
{
    class Feed : IRTDUpdateEvent, IEnumerable<Quote>
    {
        readonly string registryKey = @"HKEY_LOCAL_MACHINE\SOFTWARE\Classes\Tos.RTD\CLSID";
        IRTDServer server;
        Queue queue = new Queue();
        Topics topics = new Topics();

        internal Feed(int heartbeat)
        {
            var rtd = Type.GetTypeFromCLSID(
                new Guid(Registry.GetValue(registryKey, "", null).ToString()));
            this.server = (IRTDServer)Activator.CreateInstance(rtd);
            this.HeartbeatInterval = heartbeat;
            this.server.ServerStart(this);
        }

        internal void Stop()
        {
            this.server.ServerTerminate();
        }

        internal void Add(string symbol, string type)
        {
            var objects = new object[] { type, symbol };
            var id = getHash(symbol, type);
            this.topics.Add(id, symbol, type);
            this.server.ConnectData(id, objects, true);
        }

        internal void Remove(string symbol, string type)
        {
            var id = getHash(symbol, type);
            this.server.DisconnectData(id);
        }

        Int16 getHash(string symbol, string type)
        {
            var value = string.Format("{0}:{1}", symbol, type);
            using (var h = MD5.Create())
            {
                return Math.Abs(BitConverter.ToInt16(
                    h.ComputeHash(Encoding.UTF8.GetBytes(value)), 0));
            }
        }

        public int HeartbeatInterval
        {
            get; set;
        }

        public void Disconnect()
        {
            this.queue.Disconnect();
        }

        public void UpdateNotify()
        {
            var refresh = server.RefreshData(this.topics.Count());
            if (refresh.Length > 0)
            {
                for (int i = 0; i < refresh.Length / 2; i++)
                {
                    var id = (int)refresh[0, i];
                    var data = this.topics.Get(id);
                    this.queue.Push(new Quote(
                        data.Item1,
                        data.Item2, 
                        double.Parse(refresh[1, i].ToString())));
                }
            }
        }

        public IEnumerator<Quote> GetEnumerator()
        {
            return this.queue;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.queue;
        }
    }
}
