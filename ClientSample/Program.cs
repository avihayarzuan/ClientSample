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
            NetworkStream stream = client.GetStream();

            Task task1 = new Task(() =>
            {
                //using (BinaryReader reader = new BinaryReader(stream))
                using (BinaryWriter writer = new BinaryWriter(stream))
                {
                    while (true)
                    {
                        
                        // Send data to server
                        Console.Write("\n");
                        string line = Console.ReadLine();
                        writer.Write(line);
                        writer.Flush();
                    }
                }
            });

            Task task2 = new Task(() =>
            {
                //using (NetworkStream stream = client.GetStream())
                using (BinaryReader reader = new BinaryReader(stream))
                //using (BinaryWriter writer = new BinaryWriter(stream))
                {
                    while (true)
                    {
                        Console.Write("\n");
                        string result = reader.ReadString();
                        Console.WriteLine(result);
                        //reader.Dispose();
                    }
                }
            });

            task1.Start();
            task2.Start();
            task1.Wait();
            
        }
    }
}

//JObject closeObj = new JObject
//{
//    ["CommandEnum"] = 1,
//    ["path"] = "fghjd"
//};
//string str = closeObj.ToString();
//Console.WriteLine(str);

//            JObject g = JObject.Parse(str);
//int x;
//int.TryParse(g["CommandEnum"].ToString(), out x);
////g["CommandEnum"];
//Console.WriteLine(x);
//            Console.WriteLine(g["path"].ToString());