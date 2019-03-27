using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//装饰器模式
namespace ExercisePrj.Dsignmode
{
    //装饰抽象类
    public abstract class ShapeDecorator : IShape
    {
       protected IShape decoratedShape;

        public ShapeDecorator(IShape decoratedShape)
        {
            this.decoratedShape = decoratedShape;
        }

        public virtual void Draw()
        {
            decoratedShape.Draw();
        }
    }
    //装饰实现类
    public class RedShapeDecorator : ShapeDecorator
    {
   
       public RedShapeDecorator(IShape decoratedShape):base(decoratedShape)
        {
            
        }
       public override void Draw()
        {
            decoratedShape.Draw();
            setRedBorder(decoratedShape);
        }

        private void setRedBorder(IShape decoratedShape)
        {
            Console.WriteLine("Border Color: Red");
        }
    }
    //用扩展方法的方式直接实现
    public static class ShapeExpend
    {
        public static void SetColor(this IShape shape)
        {
            Console.WriteLine("border Color: Red"); 
        }
    }

}
