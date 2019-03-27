using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//桥接模式
namespace ExercisePrj.Dsignmode
{
    //桥接接口
    public interface IDrawAPI
    {
        void DrawCircle(int radius, int x, int y);
    }
    //具体实现
    public class GreenCircle:IDrawAPI
    {
        public void DrawCircle(int radius,int x,int y)
        {
            Console.WriteLine("draw green circle");
        }
    }
    public class RedCircle : IDrawAPI
    {
        public void DrawCircle(int radius, int x, int y)
        {
            Console.WriteLine("draw red circle,x{0},y{1}",x,y);
        }
    }
    //实体抽象基类
    public abstract class ShapeEx
    {
        protected IDrawAPI drawAPI;
        protected ShapeEx(IDrawAPI drawAPI)
        {
            this.drawAPI = drawAPI;
        }
        public abstract void Draw();
    }
    //继承实体实现类
    public class CircleEx : ShapeEx
    {
        public int x { get; set; }
        public int y { get; set; }
        public int radius { get; set; }
        private string color;
        //演示实现享元模式的构造函数
        public CircleEx(string color):base(null)
        {
            this.color = color;
            drawAPI = new RedCircle();
        }
        public CircleEx(int x, int y, int radius,IDrawAPI drawapi ):base(drawapi)
        {
            this.x = x;
            this.y = y;
            this.radius = radius;
        }

        public override void Draw()
        {
            drawAPI.DrawCircle(radius, x, y);
        }
    } 

}
