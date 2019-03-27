using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
//工厂模式
namespace ExercisePrj.Dsignmode
{
    public class ShapeFactory
    {
     
        public static  IShape CtreateShape(string shape)
        {
            if (shape == "Line")
            {
                return new Line();
            }
            else if (shape == "Circle")
            {
                return new Circle();
            }
            return null;
            

        }
       public static  IShape CtreateWithReflection(string shape)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            var ishape = assembly.CreateInstance("ExercisePrj.Dsignmode."+shape);
            return ishape as IShape;
        }
    }
    public interface IShape
    {
        void Draw();
    }
    public class Line: IShape
    {
        public void Draw()//隐式封闭实现,子类可以隐藏不能重写，类调用会执行这个
        {
            Console.WriteLine("draw line");
        }
        void IShape.Draw()//显示实现，接口调用会执行这个
        {
            Console.WriteLine("IShape.DrawLine");
        }
    }

    public class Circle:IShape
    {
        public void Draw()
        {
            Console.WriteLine("draw Circle");
        }
    }

}
