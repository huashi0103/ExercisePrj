using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ExercisePrj.Dsignmode
{
    public interface IStrategy
    {
         int DoOperation(int num1, int num2);
    }
    public class OperationAdd : IStrategy
    {
        
       public int DoOperation(int num1, int num2)
        {
            return num1 + num2;
        }
    }

    public class OperationSubstract : IStrategy
    {

        public int DoOperation(int num1, int num2)
        {
            return num1 - num2;
        }
    }
    public class OperationMultiply : IStrategy
    {

        public int DoOperation(int num1, int num2)
        {
             return num1 * num2;
        }
    }

    public class ContextEx
    {
        private IStrategy strategy;

        public ContextEx(IStrategy strategy)
        {
            this.strategy = strategy;
        }

        public int ExecuteStrategy(int num1, int num2)
        {
            return strategy.DoOperation(num1, num2);
        }

        //奇葩的写法
        private Dictionary<string, Func<int, int, int>> funcs = new Dictionary<string, Func<int, int, int>>();
        public int ExecuteStrategy(string name, int num1, int num2)
        {
            if(funcs.Count==0)
            {
                //反射写法
                var assembly = Assembly.GetExecutingAssembly();
                var types = assembly.GetTypes();
                foreach (var t in types)
                {
                    if (t.GetInterface("IStrategy") != null)
                    {
                        var instance = assembly.CreateInstance(t.FullName) as IStrategy;
                        funcs.Add(t.Name, instance.DoOperation);
                    }
                }
                //直接添加
                //funcs.Add("OperationAdd", new Func<int, int, int>((n1, n2) => { return n1 + n2; }));
                //funcs.Add("OperationSubstract", new Func<int, int, int>((n1, n2) => { return n1 - n2; }));
                //funcs.Add("OperationMultiply", new Func<int, int, int>((n1, n2) => { return n1 * n2; }));
            }
            return funcs[name](num1, num2);
        }


    }
}
