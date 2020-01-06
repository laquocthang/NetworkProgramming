using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server;
using System.Net;
using System.Net.Sockets;


namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Employee emp1 = new Employee();//1, "Nguyen", "Van A", 12, 3500000);
            //Console.WriteLine(emp1);
            TcpClient client;
            try
            {
                client = new TcpClient("127.0.0.1", 9050);
                
            }
            catch (SocketException)
            {
                Console.WriteLine("Khong ket noi duoc den server");
                return;
            }
            NetworkStream ns = client.GetStream();
            string answer;
            do
            {
                emp1.WriteData();
                byte[] packetSize = new byte[2];
                Console.WriteLine("Kich thuoc goi tin: ", emp1.Size);
                packetSize = BitConverter.GetBytes(emp1.Size);
                ns.Write(packetSize, 0, 2);
                ns.Write(emp1.DataInByte, 0, emp1.Size);
                Console.Write("Ban co muon nhap them ko?(Press NO to exit): ");
                answer = Console.ReadLine();
                ns.Write(Encoding.ASCII.GetBytes(answer), 0, answer.Length);
            } while (answer.ToUpper() != "NO");
            ns.Flush();
            ns.Close();
            client.Close();
        }
    }
}
