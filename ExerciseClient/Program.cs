using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Pipes;
using System.IO;

namespace ExerciseClient
{
    class Program
    {
        static void Main(string[] args)
        {
            NamedPipeClientStream client = new NamedPipeClientStream("testpip");

            client.Connect();
            //StreamReader sr = new StreamReader(client);
            //Console.WriteLine(sr.ReadToEnd());
            string msg = "client test msg";
            var bytes = Encoding.Default.GetBytes(msg);
            client.Write(bytes, 0, bytes.Length);
            Console.ReadLine();
            client.Close();
            
        }
    }
}
