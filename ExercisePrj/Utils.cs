using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExercisePrj
{
    public class Log
    {
        public void L(string msg)
        {
            Console.WriteLine(msg);
        }
        public void L(string format, params string[] data)
        {
            Console.WriteLine(string.Format(format,data));
        }
        public void P(Exception ex, string format, params string[] data)
        {
            var msg = string.Format(format, data);
            Console.WriteLine(string.Format("{0}:{1},{1}", msg, ex.Message, ex.StackTrace));
        }
    }
    
}
