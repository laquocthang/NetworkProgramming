using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            TcpListener server = new TcpListener(IPAddress.Any, 9050);
            server.Start();
            TcpClient client = server.AcceptTcpClient();
            NetworkStream ns = client.GetStream();
            String answer = "";
            do
            {
                byte[] size = new byte[2];
                int recv = ns.Read(size, 0, 2);
                int packSize = BitConverter.ToInt16(size, 0);
                Console.WriteLine("Kich thuoc goi tin = " + packSize);
                byte[] data = new byte[1024];
                recv = ns.Read(data, 0, packSize);
                Employee emp1 = new Employee(data);
                Console.WriteLine(emp1);
                WriteToFile(emp1);
                byte[] ansByte = new byte[3];
                recv = ns.Read(ansByte, 0, 3);
                answer = Encoding.ASCII.GetString(ansByte, 0, 2);
            } while (answer.ToUpper() != "NO");
            ns.Close();
            client.Close();
            server.Stop();
            return;
        }
        static void WriteToFile(Employee employee)
        {
            using (TextWriter write = new StreamWriter(@"input.txt", true))
            {
                write.WriteLine(employee.ToString());
                write.Close();
            }
        }
    }
}
