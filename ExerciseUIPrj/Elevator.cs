using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Concurrent;

namespace ExerciseUIPrj
{
    class Elevator
    {
        List<ElevatorStat> Floors = new List<ElevatorStat>();
        public int CurrentFloor = 0;

        ConcurrentBag<ElevatorStat> up = new ConcurrentBag<ElevatorStat>();
        ConcurrentBag<ElevatorStat> down = new ConcurrentBag<ElevatorStat>();


        public void Init( int floorcount)
        {
            for (int i = 0; i < floorcount; i++)
            {
                var elevatorstat = new ElevatorStat(i);
                elevatorstat.SetEvent += Elevatorstat_SetEvent;
                Floors.Add(elevatorstat);
            }
        }
        private void Elevatorstat_SetEvent(int arg1, int arg2, ElevatorStat arg3)
        {

            if (arg2 == 0)//down
            {
                up.Add(arg3);
            }
            else//up
            {
                down.Add(arg3);
            }
        }

        public bool Removeup(int floor)
        {
            var remove = up.FirstOrDefault(e => e.Floor == floor);
            var res = up.TryTake(out remove);
            return res;
        }
        public bool Removedown(int floor)
        {
            var remove = down.FirstOrDefault(e => e.Floor == floor);
            var res = down.TryTake(out remove);
            return res;
        }


    }

    class ElevatorStat
    {
        public int Floor { get; }
        public bool Down
        {
            get
            { return down; }
            set
            {
                down = value;
                if(down)SetEvent?.Invoke(Floor, 0, this);
            }
        }
        public bool Up {
            get { return up; }
            set
            {
                up = value;
                if(up)SetEvent?.Invoke(Floor, 1, this);
            }
        }
        private bool up = false;
        private bool down = false;
        public event Action<int, int, ElevatorStat> SetEvent;

        public ElevatorStat(int floor)
        {
            Floor = floor;
            up = false;
           down= false;
        }


    }


}
