using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExercisePrj.Dsignmode
{
    //中介类
    public class ChatRoom
    {
        public static void ShowMessage(User user, string msg)
        {
            Console.WriteLine(new DateTime().ToString()+"["+ user.Name + "] : " + msg);
        }
    }
    public class User
    {
        public string Name { get; set; }

        public User(string name)
        {
            Name = name;
        }

        public void SendMessage(String message)
        {
            ChatRoom.ShowMessage(this, message);
        }
    }


}
