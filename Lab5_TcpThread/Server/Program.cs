using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            //if (args.length != 1)
            //    return;
                //throw new ArgumentException("Parameter(s): <Port>");
            int serverPort = 9000;// Int32.Parse(args[0]);
            TcpListener listener = new TcpListener(IPAddress.Any, serverPort);
            ILogger logger = new ConsoleLogger();
            listener.Start();
            while (true)
            {
                try
                {
                    Socket client = listener.AcceptSocket();
                    IProtocol protocol = new EchoProtocol(client, logger);
                    Thread thread = new Thread(new ThreadStart(protocol.HandleClient));
                    thread.Start();
                    logger.WriteEntry("Created and Start thread = " + thread.GetHashCode());
                }
                catch(System.IO.IOException e)
                {
                    logger.WriteEntry("Error: " + e.Message);
                }
            }
        }
    }
}
