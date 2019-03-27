using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//解释器模式
namespace ExercisePrj.Dsignmode
{
    public interface Expression
    {
         bool Interpret(string context);
    }
    public class TerminalExpression : Expression
    {
       private string data;

        public TerminalExpression(string data)
        {
            this.data = data;
        }

       public  bool Interpret(string context)
        {
            if (context.Contains(data))
            {
                return true;
            }
            return false;
        }
    }
    public class OrExpression : Expression
    {
        private Expression expr1 = null;
        private Expression expr2 = null;
        public OrExpression(Expression expr1, Expression expr2)
        {
            this.expr1 = expr1;
            this.expr2 = expr2;
        }
        public bool Interpret(String context)
        {
            return expr1.Interpret(context) || expr2.Interpret(context);
        }
    }
    public class AndExpression : Expression
    {
        private Expression expr1 = null;
        private Expression expr2 = null;

        public AndExpression(Expression expr1, Expression expr2)
        {
            this.expr1 = expr1;
            this.expr2 = expr2;
        }
        public bool Interpret(String context)
        {
            return expr1.Interpret(context) && expr2.Interpret(context);
        }
        }
}
