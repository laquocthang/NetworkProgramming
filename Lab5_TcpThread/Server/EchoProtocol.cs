using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Collections;
using System.Threading;


namespace Server
{
    public class EchoProtocol : IProtocol
    {
        public const int BUFF_SIZE = 32;
        private Socket clientSocket;
        private ILogger logger;

        public EchoProtocol(Socket clientSocket, ILogger logger)
        {
            this.clientSocket= clientSocket;
            this.logger = logger;
        }
        public void HandleClient()
        {
            ArrayList entry = new ArrayList();
            entry.Add("Client address and port = " + clientSocket.RemoteEndPoint);
            entry.Add("Thread " + Thread.CurrentThread.GetHashCode());
            try
            {
                int recvMessSize;
                int totalBytesEchoed = 0;
                byte[] recvBuffer = new byte[BUFF_SIZE];
                try
                {
                    string recv = "";
                    while ((recvMessSize = clientSocket.Receive(recvBuffer, 0, recvBuffer.Length, SocketFlags.None)) > 0)
                    {
                        clientSocket.Send(recvBuffer, 0, recvMessSize, SocketFlags.None);
                        totalBytesEchoed += recvMessSize;
                        recv = Encoding.ASCII.GetString(recvBuffer, 0, 32);
                        logger.WriteEntry(clientSocket.RemoteEndPoint + ": " + recv);
                    }
                }
                catch (SocketException e)
                {
                    entry.Add(e.ErrorCode + ": " + e.Message);
                }
                entry.Add("Client finish; echoed " + totalBytesEchoed + " bytes.");
            }
            catch (SocketException e)
            {
                entry.Add(e.ErrorCode + ": " + e.Message);                
            }
            clientSocket.Close();
            logger.WriteEntry(entry);
        }
    }
}
