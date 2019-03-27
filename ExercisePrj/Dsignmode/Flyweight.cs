using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//享元模式
namespace ExercisePrj.Dsignmode
{
    //享元模式的工厂类
    public class ShapeFlyweight
    {
        private static readonly Dictionary<string, ShapeEx> circleMap = new Dictionary<string, ShapeEx>();

        public static ShapeEx getCircle(string color)
        {
            CircleEx circle;

            if (!circleMap.Keys.Contains(color))
            {
                circle = new CircleEx(color);
                circleMap.Add(color, circle);
                Console.WriteLine("Creating circle of color : " + color);
                return circle;
            }
            else
            {
                circle=(CircleEx)circleMap[color];
            }
            return circle;
        }
    }
}
