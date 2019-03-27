using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExercisePrj.Dsignmode
{
    public abstract class Game
    {
        public  abstract void Initialize();
        public  abstract void StartPlay();
        public abstract void EndPlay();

        //模板
        public void play()
        {

            //初始化游戏
            Initialize();
            //开始游戏
            StartPlay();
            //结束游戏
            EndPlay();
        }
    }
    public class Cricket : Game
    {
        public override void EndPlay()
        {
            Console.WriteLine("Cricket Game Finished!");
        }

        public override void Initialize()
        {
            Console.WriteLine("Cricket Game Initialized! Start playing.");
        }


        public override void StartPlay()
        {
            Console.WriteLine("Cricket Game Started. Enjoy the game!");
        }
    }
}
