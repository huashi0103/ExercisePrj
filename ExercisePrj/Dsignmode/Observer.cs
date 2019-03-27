using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//观察者模式
namespace ExercisePrj.Dsignmode
{

    public class Subject: IObservable<Subject>
    {
        public int State {get; set;}
        public Subject(int state)
        {
            State = state;
        }
        private List<IObserver<Subject>> observers = new List<IObserver<Subject>>();

        public IDisposable Subscribe(IObserver<Subject> observer)
        {
            if (!observers.Contains(observer))
                observers.Add(observer);
            return new Unsubscriber(observers, observer);
        }
        private class Unsubscriber : IDisposable
        {
            private List<IObserver<Subject>> _observers;
            private IObserver<Subject> _observer;

            public Unsubscriber(List<IObserver<Subject>> observers, IObserver<Subject> observer)
            {
                this._observers = observers;
                this._observer = observer;
            }

            public void Dispose()
            {
                if (_observer != null && _observers.Contains(_observer))
                    _observers.Remove(_observer);
            }
        }

        public void TrackLocation(Subject ob)
        {
            Console.WriteLine("start");
            foreach (var observer in observers)
            {
                if (ob==null)
                    observer.OnError(new Exception("unknowExeption"));
                else
                    observer.OnNext(ob);
            }
        }

        public void EndTransmission()
        {
            foreach (var observer in observers.ToArray())
                if (observers.Contains(observer))
                    observer.OnCompleted();

            observers.Clear();
        }

    }


    public class BinaryObserver : IObserver<Subject>
    {
        public void OnCompleted()
        {
            Console.WriteLine("complete");
        }

        public void OnError(Exception error)
        {
            Console.WriteLine(error.Message);
        }

        public void OnNext(Subject value)
        {
            Console.WriteLine("Binary String: " + Convert.ToString(value.State, 2));
        }
    }
    public class OctalObserver : IObserver<Subject>
    {
        public void OnCompleted()
        {
            Console.WriteLine("complete");
        }

        public void OnError(Exception error)
        {
            Console.WriteLine(error.Message);
        }

        public void OnNext(Subject value)
        {
            Console.WriteLine("Octal String: " + Convert.ToString(value.State, 8));
        }

    }
    public class HexaObserver : IObserver<Subject>
    {
        public void OnCompleted()
        {
            Console.WriteLine("complete");
        }

        public void OnError(Exception error)
        {
            Console.WriteLine(error.Message);
        }

        public void OnNext(Subject value)
        {
            Console.WriteLine("Hex String: " + Convert.ToString(value.State,16));
        }
    }
}
