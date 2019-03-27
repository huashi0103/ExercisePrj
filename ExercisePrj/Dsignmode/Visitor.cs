using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExercisePrj.Dsignmode
{

    public interface IComputerPartVisitor
    {
        void Visit(Computer computer);
        void Visit(Mouse mouse);
        void Visit(Keyboard keyboard);
        void Visit(Monitor monitor);
    }

    public interface IComputerPart
    {
        void Accept(IComputerPartVisitor computerPartVisitor);
    }

    public class Keyboard  : IComputerPart
    {

       public void Accept(IComputerPartVisitor computerPartVisitor)
        {
            computerPartVisitor.Visit(this);
        }
    }
    public class Monitor  : IComputerPart
    {

       public void Accept(IComputerPartVisitor computerPartVisitor)
        {
            computerPartVisitor.Visit(this);
        }
}
    public class Mouse  : IComputerPart
    {
       public void Accept(IComputerPartVisitor computerPartVisitor)
        {
            computerPartVisitor.Visit(this);
        }
    }
    public class Computer : IComputerPart
    {
        IComputerPart [] parts;
        public Computer()
        {
            parts = new IComputerPart[] { new Mouse(), new Keyboard(), new Monitor() };
        }
        public void Accept(IComputerPartVisitor computerPartVisitor)
        {
            for (int i = 0; i < parts.Length; i++)
            {
                parts[i].Accept(computerPartVisitor);
            }
            computerPartVisitor.Visit(this);
        }
    }

    public class ComputerPartDisplayVisitor : IComputerPartVisitor
    {
        public void Visit(Computer computer)
        {
            Console.WriteLine("Displaying Computer.");
        }
        public void Visit(Mouse mouse)
        {
            Console.WriteLine("Displaying Mouse.");
        }
        public void Visit(Keyboard keyboard)
        {
            Console.WriteLine("Displaying Keyboard.");
        }
        public void Visit(Monitor monitor)
        {
            Console.WriteLine("Displaying Monitor.");
        }
    }
}
