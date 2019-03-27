using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//单例模式
namespace ExercisePrj.Dsignmode
{
    public class Singleton
    {
        private Singleton() { }
        //private static Singleton m_Singleton;
        //private static readonly object lockvalue = new object();
        //public static Singleton GetInstance()
        //{
        //    //return m_Singleton ?? new Singleton();//不加锁 线程不安全
        //    if (m_Singleton == null)
        //    {
        //        lock (lockvalue)//枷锁//这里还可以加双锁，就是在里边判断是不是空
        //        {
        //            return new Singleton();
        //        }

        //    }
        //    return m_Singleton;
        //}

        public static readonly Singleton Instance = new Singleton();//据说这个是最流弊的写法··跟下边的写法是一个意思··
        //public static readonly Singleton Instance=null
        //static Singleton()
        //{
        //    Instance = new Singleton();
        //}
    }
}
