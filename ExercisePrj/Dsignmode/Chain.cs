using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//责任链模式
namespace ExercisePrj.Dsignmode
{
    public abstract class AbstractLogger
    {
        public static int INFO = 1;
        public static int DEBUG = 2;
        public static int ERROR = 3;
        protected int level;
        //责任链中的下一个对象
        protected AbstractLogger nextLogger;
        public void SetNextLogger(AbstractLogger next)
        {
            nextLogger = next;
        }
        public void LogMessage(int level,string message)
        {
            if(this.level<=level)
            {
                Write(message);
            }
            if(nextLogger!=null)
            {
                nextLogger.LogMessage(level, message);
            }
        }
        protected abstract void Write(string message);
    }
    public class ConsoleLogger : AbstractLogger
    {

       public ConsoleLogger(int level)
        {
            this.level = level;
        }

       protected  override void Write(string message)
        {
            Console.WriteLine("Standard Console::Logger: " + message);
        }
    }
    public class ErrorLogger : AbstractLogger
    {

       public ErrorLogger(int level)
        {
            this.level = level;
        }

       protected override void Write(String message)
        {
            Console.WriteLine("Error Console::Logger: " + message);
        }
    }
    public class FileLogger : AbstractLogger
    {
       public FileLogger(int level)
        {
            this.level = level;
        }

       protected override void Write(String message)
        {
            Console.WriteLine("File::Logger: " + message);
        }
    }
}
