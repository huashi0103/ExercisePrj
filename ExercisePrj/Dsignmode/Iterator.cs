using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExercisePrj.Dsignmode
{
    public class IteratorEx : IEnumerable //<IteratorEx>
    {
        public string Name;
        private List<IteratorEx> list = new List<IteratorEx>();

        //public IEnumerator<IteratorEx> GetEnumerator()
        //{
        //    foreach (var l in list)
        //    {
        //        yield return l;
        //    }
        //}

        public void SetList(List<IteratorEx> data)
        {
            list = data;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            foreach (var l in list)
            {
                yield return l;
            }
            //return new IteratorExEnum(list.ToArray());
        }
    }
    public class IteratorExEnum : IEnumerator
    {
        private IteratorEx[] list;
        private int position = -1;
        public IteratorExEnum(IteratorEx[] data)
        {
            list = data;
        }
        public object Current
        {
            get
            {
                try
                {
                    return list[position];
                }
                catch (IndexOutOfRangeException)
                {
                    throw new InvalidOperationException();
                }
            }
        }

        public bool MoveNext()
        {
            position++;
            return position < list.Length;
        }

        public void Reset()
        {
            position = -1;
        }
    }

}
