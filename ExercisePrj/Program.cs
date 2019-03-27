using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using ExercisePrj.Text;
using ExercisePrj.Dsignmode;
using System.Diagnostics;

using System.Runtime.InteropServices;
using System.Threading;

namespace ExercisePrj
{
    class Program
    {
        static void w(object msg)
        {
            Console.WriteLine(msg);
        }
        static void Main(string[] args)
        {
            //PipTest();


        }

        static void  PipTest()
        {
            Thread thread = new Thread(() =>
            {
                PipServer pip = new PipServer("TEST_PIP");
                pip.ReceiveEvent += s =>
                {
                    w(string.Format("receive:{0}",s));
                };
                pip.Listen();
            });
            thread.Start();

            bool send = true;
            int count = 0;
            AutoResetEvent monitor = new AutoResetEvent(false);
            Thread client = new Thread(() =>
            {
                PipClient ct = new PipClient("TEST_PIP");
                while (send)
                {
                    string msg = string.Format("这是第{0}条数据", count);
                    w(msg);
                    ct.Send(msg);
                    count++;
                    if (monitor.WaitOne(1000))
                    {
                        break;
                    }
                }
            });
            client.Start();
            while (true)
            {
                var input = Console.ReadLine();
                if (input == "q" || input == "Q")
                {
                    send = false;
                    monitor.Set();
                    break;
                }
            }
        }

        static void Officemonitor()
        {
            OfficeMonitor om = new OfficeMonitor();
            bool stop = false;
            Thread thread = new Thread(new ThreadStart(() => {
                while (!stop)
                {
                    bool flag = false;
                    var res = om.GetFilesEx();
                    if (res == null)
                    {
                        Thread.Sleep(1000);
                        continue;
                    }

                    foreach (var r in res)
                    {
                        w(r);
                    }
                    if (res.Count == 0)
                        w("none");
                    var pros = Process.GetProcessesByName("WINWORD");
                    w(pros.Length);
                    Thread.Sleep(1000);
                    w("------");
                }
            }));
            thread.Start();

            var key = Console.ReadLine();
            if (key == "q")
            {
                stop = true;
            }
            else
            {
                stop = stop ? false : true;
                if (!stop)
                {
                    thread.Start();
                }
                key = Console.ReadLine();
            }
        }




    }
}
