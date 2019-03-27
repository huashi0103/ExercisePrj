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

        //逆序栈元素
        public static int GetAndRemoveLastElement(Stack<int> stack)
        {
            int result = stack.Pop();
            if (stack.Count == 0)
            {
                return result;
            }
            else
            {
                int last = GetAndRemoveLastElement(stack);
                stack.Push(result);
                return last;
            }
        }
        public static void Reverse(Stack<int> stack)
        {
            if (stack.Count == 0)
            {
                return;
            }
            else
            {
                int i = GetAndRemoveLastElement(stack);
                Reverse(stack);
                stack.Push(i);
            }
        }

        //猫狗队列
        public class Pet {
            protected string type;
           
            public Pet(string type) {
                this.type = type;
            }
            public string GetPetType()
            {
                return type;
            }
        }
        public class Dog : Pet {
            public Dog(string type):base(type)
            {
                type = "dog";      
            }
        }
        public class Cat : Pet
        {
            public Cat(string type) : base(type)
            {
                type = "cat";
            }

        }
        public class PetEnterQueue {
            private Pet pet;
            private long count;
            public PetEnterQueue(Pet pet, long count)
            {
                this.pet = pet;
                this.count = count;
            }
            public Pet GetPet()
            {
                return this.pet;
            }
            public long GetCount()
            {
                return this.count;
            }
            public string GetEnterPetType()
            {
                return pet.GetPetType();
            }
        }
        public class DogCatQueue {
            private Queue<PetEnterQueue> dogQ;
            private Queue<PetEnterQueue> catQ;
            private long count=0;
            public DogCatQueue() {
                this.dogQ = new Queue<PetEnterQueue>();
                this.catQ = new Queue<PetEnterQueue>();
            }

            public void Add(Pet pet) {
                if (pet.GetPetType() == "dog")
                {
                    this.dogQ.Enqueue(new PetEnterQueue(pet, this.count++));
                }
                else if (pet.GetPetType() == "cat")
                {
                    this.catQ.Enqueue(new PetEnterQueue(pet, this.count++));
                }
                else
                {
                    throw new Exception("not dog or cat");
                }
            }
            public Pet PollAll()
            {
                if (!(dogQ.Count == 0) && !(catQ.Count == 0))
                {
                    return (dogQ.Peek().GetCount() > catQ.Peek().GetCount()) ? dogQ.Dequeue().GetPet() : catQ.Dequeue().GetPet();
                }
                else if (!(dogQ.Count == 0))
                {
                    return dogQ.Dequeue().GetPet();
                }
                else if (!(catQ.Count == 0))
                {
                    return catQ.Dequeue().GetPet();
                }
                else
                {
                    throw new Exception("queue is empty");
                }
            }
            public Pet PollDog()
            {
                if (dogQ.Count != 0)
                {
                    return dogQ.Dequeue().GetPet();
                }
                else {
                    throw new Exception("no dog");
                }

            }
            public Pet PollCat()
            {
                if (catQ.Count != 0)
                {
                    return catQ.Dequeue().GetPet();
                }
                else
                {
                    throw new Exception("no cat");
                }

            }


        }

        //用一个栈排序另一个栈
        public static void SortStackByStack(Stack<int> stack) {
            Stack<int> help = new Stack<int>();
            while (stack.Count == 0)
            {
                var cur = stack.Pop();
                while (help.Count != 0 && help.Peek() > cur)
                {
                    stack.Push(help.Pop());

                }
                help.Push(cur);

            }
            while (help.Count != 0)
            {
                stack.Push(help.Pop());
            }
        }


    } 
}
