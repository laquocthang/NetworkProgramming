using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Server
{
    public class FileLogger : ILogger
    {
        private static Mutex mutex = new Mutex();
        private StreamWriter theOutput;

        public FileLogger(String fileName)
        {
            theOutput = new StreamWriter(fileName,true);
        }
        public void WriteEntry(ArrayList entry)
        {
            mutex.WaitOne();
            IEnumerator line = entry.GetEnumerator();
            while (line.MoveNext())
            {
                theOutput.WriteLine(line.Current);
            }
            theOutput.Flush();
            mutex.ReleaseMutex();
        }

        public void WriteEntry(string entry)
        {
            mutex.WaitOne();
            theOutput.WriteLine(entry);
            theOutput.Flush();
            mutex.ReleaseMutex();
        }
    }
}
