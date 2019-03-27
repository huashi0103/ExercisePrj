using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//建造者模式
namespace ExercisePrj.Dsignmode
{
    //构建类
    public class MealBuilder
    {
        public Meal prepareVegMeal()
        {
            Meal meal = new Meal();
            meal.addItem(new VegBurger());
            meal.addItem(new Coke());
            return meal;
        }

        public Meal prepareNonVegMeal()
        {
            Meal meal = new Meal();
            meal.addItem(new ChickenBurger());
            meal.addItem(new Pepsi());
            return meal;
        }
    }
    //实体接口
    public interface Item
    {
        string name { get;}
        float price { get; }
        IPacking packing();
    }
    //实体关联接口
    public interface IPacking
    {
        string pack();
    }
    //不同实体
    public class Wrapper:IPacking
    {
        public string pack()
        {
            return "Wrapper";
        }
    }
    public class Bottle:IPacking
    {
        public string pack()
        {
            return "Bottle";
        }
    }
    public abstract class Burger:Item
    {
        public IPacking packing()
        {
            return new Wrapper();
        }
        public abstract string name { get; }
        public abstract float price { get;  }
    }
    public abstract class ColdDrink:Item
    {
        public IPacking packing()
        {
            return new Bottle();
        }
        public abstract string name { get;  }
        public abstract float price { get; }
    }
    public class VegBurger:Burger
    {
        public override string name { get;  }
        public override float price { get; }
        public VegBurger()
        {
            name = "Veg Burger";
            price = 25.0f;
        }
    }
    public class ChickenBurger: Burger
    {
        public override string name { get; }
        public override float price { get; }
        public ChickenBurger()
        {
            name = "Chicken Burger";
            price = 50.0f;
        }
    }

    public class Coke : ColdDrink
    {
        public override string name { get; }
        public override float price { get; }
        public Coke()
        {
            name = "Coke";
            price = 30.0f;
        }
        
    }
    public class Pepsi : ColdDrink
    {
        public override string name { get; }
        public override float price { get; }
        public Pepsi()
        {
            name = "Pepsi";
            price = 35.0f;
        }

    }
    //不同的组合类
    public class Meal
    {
        private List<Item> Items = new List<Item>();
        public void addItem(Item item)
        {
            Items.Add(item);
        }
        public float getCost()
        {
            float cost = 0;
            foreach(var item in Items)
            {
                cost += item.price;
            }
            return cost;
        }
        public void ShowItems()
        {
            foreach(var item in Items)
            {
                Console.WriteLine("name={0},packing={1},price={2}", item.name, item.packing().pack(), item.price);
            }
        }
    }
}
