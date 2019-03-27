using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//外观模式
namespace ExercisePrj.Dsignmode
{
    //外观类
    public class ShapeMaker
    {
        private IShape circle;
        private IShape line;

        public ShapeMaker()
        {
            circle = new Circle();
            line = new Line();
        }

        public void drawCircle()
        {
            circle.Draw();
        }
        public void drawLine()
        {
            line.Draw();
        }
    }
}
