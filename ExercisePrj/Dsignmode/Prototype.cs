using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
//原型模式
namespace ExercisePrj.Dsignmode
{
    //
    public class ApplianceCach
    {
        private static Dictionary<string, Appliance> ApplianceMap = new Dictionary<string, Appliance>();
        public static Appliance GetApplicance(string shapeId)
        {
            Appliance cachAppliance = ApplianceMap[shapeId];
            return (Appliance)cachAppliance.Clone();
        }
        public static void loadCache()
        {
            Fridge fridge = new Fridge();
            fridge.ID = "1";
            ApplianceMap.Add(fridge.ID, fridge);

            Television tv = new Television();
            tv.ID = "2";
            ApplianceMap.Add(tv.ID, tv);

        }
    }
    public class Description
    {
        public string desc;
        public string descid;
    }
    [Serializable]
    public abstract class Appliance:ICloneable
    {
        protected string type;
        private string id;
        public string Type { get { return type; } }
        public string ID { get { return id; } set { id = value; } }
        public Description Desc { get; set; }
        public abstract void DoWork();
       public  object Clone()
        {
            return this.MemberwiseClone();
            //object obj = null;
            ////将对象序列化成内存中的二进制流  
            //BinaryFormatter inputFormatter = new BinaryFormatter();
            //MemoryStream inputStream;
            //using (inputStream = new MemoryStream())
            //{
            //    inputFormatter.Serialize(inputStream, this);
            //}
            ////将二进制流反序列化为对象  
            //using (MemoryStream outputStream = new MemoryStream(inputStream.ToArray()))
            //{
            //    BinaryFormatter outputFormatter = new BinaryFormatter();
            //    obj = outputFormatter.Deserialize(outputStream);
            //}
            //return obj;

        }
    }

    public class Fridge :Appliance
    {
        public Fridge()
        {
            type = "Fridge";
            Desc = new Description();
            Desc.desc = "冰箱";
        }
        public override void DoWork()
        {
            Console.WriteLine("do some fridge job");
        }

    }
    public class Television:Appliance
    {
        public Television()
        {
            type = "Television";
            Desc = new Description();
            Desc.desc = "电视";
        }
        public override void DoWork()
        {
            Console.WriteLine("do some Television job");
        }
    }
}
