using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Server
{
    public class ConsoleLogger : ILogger
    {
        private static Mutex mutex = new Mutex();
        public void WriteEntry(ArrayList entry)
        {
            mutex.WaitOne();
            IEnumerator line = entry.GetEnumerator();
            while (line.MoveNext())
            {
                Console.WriteLine(line.Current);
            }
            mutex.ReleaseMutex();
        }


        public void WriteEntry(string entry)
        {
            mutex.WaitOne();
            Console.WriteLine(entry);
            mutex.ReleaseMutex();
        }
    }
}
