using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExercisePrj.Dsignmode
{
    public class Memento
    {
        public string State { get; }
        public Memento(string state)
        {
            State = state;
        }
    }
    public class Originator
    {
        public string State { get; set; }

        public Memento SaveStateToMemento()
        {
            return new Memento(State);
        }

        public void GetStateFromMemento(Memento Memento)
        {
            State = Memento.State;
        }
    }
    public class CareTaker
    {
        private List<Memento> mementoList = new List<Memento>();

        public void Add(Memento state)
        {
            mementoList.Add(state);
        }

        public Memento Get(int index)
        {
            return mementoList[index];
        }
    }

}
