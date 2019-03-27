using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//抽象工厂模式
namespace ExercisePrj.Dsignmode
{
    //抽象工厂类
    public  abstract class AbstractFactory
    {
       public  abstract IShape GetShape(string shape);
       public  abstract IColor GetColor(string color);
    }
    //工厂类子类
    public class ShapeFactoryEx:AbstractFactory
    {
        public override IShape GetShape(string shape)
        {
            return ShapeFactory.CtreateShape(shape);//偷个懒
        }
        public override IColor GetColor(string color)
        { return null; }
    }
    public class ColorFactory : AbstractFactory
    {
        public override IShape GetShape(string shape)
        {
            return null;
        }
        public override IColor GetColor(string color)
        {
            if(color=="blue")
            {
                return new Blue();
            }
            else if (color=="red")
            {
                return new Red();
            }
            return null;
        }
    }
    //工厂创造器
    public  class FactoryProducer 
    {
        public static AbstractFactory getFactory( string SType)
        {
            if(SType=="shape")
            {
                return new ShapeFactoryEx();
            }
            else if(SType=="color")
            {
                return new ColorFactory();
            }
            return null;
        }
    }
    public  interface IColor
    {
        void Fill();
    }
    public class Blue:IColor
    {
        public void Fill()
        {
            Console.WriteLine("Blue");
        }
    }
    public class Red : IColor
    {
        public void Fill()
        {
            Console.WriteLine("Red");
        }
    }

}
