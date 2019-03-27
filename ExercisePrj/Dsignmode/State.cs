using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExercisePrj.Dsignmode
{
    public class Context
    {
        public State State { get; set; }

        public Context()
        {
            State = null;
        }
    }
    public interface State
    {
         void DoAction(Context context);
    }

    public class StartState : State
    {
       public void DoAction(Context context)
        {
            Console.WriteLine("Player is in start state");
            context.State = this;
        }

        public override string ToString()
        {
            return "Start State";
        }
    }
    public class StopState : State
    {

       public void DoAction(Context context)
        {
            Console.WriteLine("Player is in stop state");
            context.State = this;
        }

        public override string  ToString()
        {
            return "Stop State";
        }
    }
}
