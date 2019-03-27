using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//命令模式
namespace ExercisePrj.Dsignmode
{
    public interface IOrder
    {
        void Execute();
    }
    public class Stock
    {
        private string name = "ABC";
        private int quantity = 10;

        public void Buy()
        {
            Console.WriteLine("Stock name:{0},quantity:{1},bought",name,quantity);
        }
        public void Sell()
        {
            Console.WriteLine("Stock name:{0},quantity:{1}sold", name, quantity);
        }
    }
    //请求类
    public class BuyStock : IOrder
    {
       private Stock abcStock;

        public BuyStock(Stock abcStock)
        {
            this.abcStock = abcStock;
        }

        public void Execute()
        {
            abcStock.Buy();
        }
    }
    //继承接口的实体
    public class SellStock : IOrder
    {
       private Stock abcStock;

        public SellStock(Stock abcStock)
        {
            this.abcStock = abcStock;
        }

        public void Execute()
        {
            abcStock.Sell();
        }
    }

    //命令调用类
    public class Broker
    {
        private List<IOrder> orderList = new List<IOrder>();

        public void takeOrder(IOrder order)
        {
            orderList.Add(order);
        }

        public void placeOrders()
        {
            foreach (IOrder order in orderList)
            {
                order.Execute();
            }
            orderList.Clear();
        }
    }

}
