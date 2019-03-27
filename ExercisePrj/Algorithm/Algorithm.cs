using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExercisePrj.Algorithm
{
    class Algorithm
    {
        //带有getmin功能的栈
        public class MyStack
        {
            private Stack<int> stackData;
            private Stack<int> stackMin;
            public MyStack()
            {
                stackData = new Stack<int>();
                stackMin = new Stack<int>();
            }
            public void Push(int value)
            {
                //方案一
                if (stackMin.Count == 0)
                {
                    stackMin.Push(value);
                }
                else if(value<=GetMin())
                {
                    stackMin.Push(value);
                }
                //方案二
                //if (stackMin.Count == 0)
                //{
                //    stackMin.Push(value);
                //}
                //else if (value < GetMin())
                //{
                //    stackMin.Push(value);
                //}
                //else
                //{
                //    var min = GetMin();
                //    stackMin.Push(min);

                //}


                stackData.Push(value);
            }
            public int Pop()
            {
                if (stackData.Count == 0)
                {
                    throw new Exception("stack is empty!");
                }
                //方案一
                int val = stackData.Pop();
                if (val == GetMin())
                {
                    stackMin.Pop();
                }
                //方案二//总是
                //var res=stackMin.Pop();
                return val;
            }
            public int GetMin()
            {
                if (stackMin.Count == 0)
                {
                    throw new Exception("stack is empty");
                }
                return stackMin.Peek();
            }
        }

        //用两个栈组成队列
        public class TwoStacksQueue
        {
            Stack<int> stackPush;
            Stack<int> stackPop;
            public TwoStacksQueue()
            {
                stackPush = new Stack<int>();
                stackPop = new Stack<int>();
            }
            public void Add(int pushInt)
            {
                stackPush.Push(pushInt);
            }

            public int Poll()
            {
                if (stackPop.Count == 0 && stackPush.Count == 0)
                {
                    throw new Exception("queue is empty");
                }
                else if(stackPop.Count==0) {
                    while (!(stackPush.Count == 0))
                    {
                        stackPop.Push(stackPush.Pop());
                    }   
                }
                return stackPop.Pop();

            }

            public int Peek()
            {
                if (stackPop.Count == 0 && stackPush.Count == 0)
                {
                    throw new Exception("queue is empty");
                }
                else if (stackPop.Count == 0)
                {
                    while (!(stackPush.Count == 0))
                    {
                        stackPop.Push(stackPush.Pop());
                    }
                }
                return stackPop.Peek();

            }
        }

        
    }
}
