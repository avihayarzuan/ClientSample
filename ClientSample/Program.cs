using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;


namespace ClientSample
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("hello\n");
            //Console.WriteLine("Press any key to exit.");

            IPEndPoint ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8000);
            TcpClient client = new TcpClient();
            client.Connect(ep);
            Console.WriteLine("You are connected");
            Console.Write("a for option one: ");
            string check = Console.ReadLine();
            //int.TryParse(check, out int res);
            if (check.Equals("a"))
            {

            using (NetworkStream stream = client.GetStream())
            using (BinaryReader reader = new BinaryReader(stream))
            using (BinaryWriter writer = new BinaryWriter(stream))
            {
                // Send data to server
                Console.Write("Please enter a number: ");
                string num = Console.ReadLine();
                Console.Write(num + "\n");
                writer.Write(num);
                // Get result from server
                string result = reader.ReadString();
                Console.WriteLine(result);
            }
            client.Close();

            } else
            {

            using (NetworkStream stream = client.GetStream())
            using (BinaryReader reader = new BinaryReader(stream))
            using (BinaryWriter writer = new BinaryWriter(stream))
            {
            while(true)
                {
                Console.Write("\n");
                string result = reader.ReadString();
                Console.WriteLine(result);
                }
            }
            client.Close();
            }

            Console.ReadKey();
        }
    }
}
