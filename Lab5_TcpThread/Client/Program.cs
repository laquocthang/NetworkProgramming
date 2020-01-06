using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            //IPEndPoint ipepServer = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9000);
            //Socket clientSocket = new Socket(AddressFamily.InterNetwork,SocketType.Stream,ProtocolType.Tcp);
            //clientSocket.Connect(ipepServer);
            TcpClient cl = new TcpClient("127.0.0.1",9000);
            Socket client = cl.Client;
            string content = "";
            byte[] buffIn,buffOut;
            Thread th = new Thread(delegate()
            {
                //string str = "";
                try
                {
                    while (true)
                    {
                        buffOut = new byte[128];
                        client.Receive(buffOut, 0, 64, SocketFlags.None);
                        Console.WriteLine(Encoding.ASCII.GetString(buffOut, 0, 32));
                    }
                }
                catch (SocketException e)
                {
                    Console.WriteLine("Disconected!");
                }
            });
            th.Start();
            while (true)
            {
                content = Console.ReadLine();
                buffIn = Encoding.ASCII.GetBytes(content);
                client.Send(buffIn, 0, buffIn.Length, SocketFlags.None);
            }
        }
    }
}
