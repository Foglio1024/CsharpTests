using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadSynchronizationTest
{
    class Program
    {
        // no lock - add only : 30M/s
        // lock - add only : 13M/s
        // lock - add+get : 500k/s

        static List<int> list = new List<int>();
        static object l = new object();
        static int count = 0;
        static int last = 0;
        static void Main(string[] args)
        {
            var t = new System.Timers.Timer(100);
            t.Elapsed += T_Elapsed;
            var t1 = new Thread(new ThreadStart(() =>
            {
                int i = 0;
                while (true)
                {
                    lock (l)
                    {
                        list.Add(++i);
                        count++;
                        last = i;
                    }
                }
            }));

            t1.Start();
            t.Start();
            //Thread.Sleep(2000);
            while (true)
            {
                lock (l)
                {
                    var v = list.FirstOrDefault(x => x == 100000);
                }
            }
        }

        private static void T_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            Console.Write($"\rlast added: {last} @ {(count*10).ToString("#,#", CultureInfo.InvariantCulture)} items/sec");
            count = 0;
        }
    }
}
