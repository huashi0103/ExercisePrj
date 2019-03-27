using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExercisePrj.Dsignmode
{
    public abstract class AbstractCustomer
    {
        public abstract bool IsNull();
        public abstract string Name { get; }
    }
    public class RealCustomer : AbstractCustomer
    {
        public override string Name { get; }

        public RealCustomer(string name)
        {
            Name = name;
        }
        public override bool IsNull()
        {
            return false;
        }
    }
    public class NullCustomer : AbstractCustomer
    {
            public override string Name { get { return "Not Available in Customer Database"; } }
            public override bool IsNull()
            {
                return true;
            }
    }
    public class CustomerFactory
    {
        public static  string[] names = {"Rob", "Joe", "Julie"};
         public static AbstractCustomer getCustomer(string name)
        {
            if(names.Contains(name))
            {
                return new RealCustomer(name);
            }
            return new NullCustomer();
        }
    }
}
