using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//代理模式
namespace ExercisePrj.Dsignmode
{
    public interface IImage
    {
        void Display();
    }
    public class RealImage:IImage
    {
        private string filename;
        public RealImage(string filename)
        {
            this.filename = filename;
            loadfile(filename);
        }
        public void Display()
        {
            Console.WriteLine("display:"+filename);
        }
        private void loadfile(string filename)
        {
            Console.WriteLine("loadfile");
        }
    }
    //代理类
    public class ProxyImage : IImage
    {

       private RealImage realImage;
        private String fileName;

        public ProxyImage(String fileName)
        {
            this.fileName = fileName;
        }

       public void Display()
        {
            if (realImage == null)
            {
                realImage = new RealImage(fileName);
            }
            realImage.Display();
        }
    }
}
