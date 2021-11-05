using System;
using System.Threading;

namespace OS_Problem_02
{
    class Thread_safe_buffer
    {
        static int[] TSBuffer = new int[10];
        static int Front = 0;
        static int Back = 0;
        static int Count = 0;
        static Boolean exit = false;
        static object enQueueLock = new Object();
        static object deQueueLock = new Object();
        static void EnQueue(int eq)
        {      
            lock (enQueueLock) {
                do {
                    if (exit) {
                        return;
                    }
                    // Console.WriteLine("Enqueue | Front : {0}, Back : {1}", Front, Back);
                    // Console.WriteLine("[{0}]", string.Join(", ", TSBuffer));
                } while(Count == 10);
                TSBuffer[Back] = eq;
                Count += 1;
                Back++;
                Back %= 10;
            }
        }

        static int DeQueue()
        {   
            lock (deQueueLock) {
                do {
                    if (exit) {
                        return -1;
                    }
                    // Console.WriteLine("Dequeue | Front : {0}, Back : {1}", Front, Back);
                    // Console.WriteLine("[{0}]", string.Join(", ", TSBuffer));
                } while(Count == 0);
                int x = 0;
                x = TSBuffer[Front];
                Front++;
                Front %= 10;
                Count -= 1;
                return x;
            }
        }

        static void th01()
        {
            int i;

            for (i = 1; i < 51; i++)
            {   
                if (exit) {
                    return;
                }

                EnQueue(i);
                Thread.Sleep(5);
            }
        }

        static void th011()
        {
            int i;

            for (i = 100; i < 151; i++)
            {
                if (exit) {
                    return;
                }

                EnQueue(i);
                Thread.Sleep(5);
            }
        }


        static void th02(object t)
        {
            int i;
            int j;

            for (i=0; i< 60; i++)
            {
                if (exit) {
                    return;
                }

                j = DeQueue();
                if (j == -1) {
                    return;
                }
                Console.WriteLine("j={0}, thread:{1}", j, t);
                Thread.Sleep(100);
            }
        }
        static void Main(string[] args)
        {
            Thread t1 = new Thread(th01);
            Thread t11 = new Thread(th011);
            Thread t2 = new Thread(th02);
            Thread t21 = new Thread(th02);
            Thread t22 = new Thread(th02);

            t1.Start();
            t11.Start();
            t2.Start(1);
            t21.Start(2);
            t22.Start(3);
            
            Console.Read();
            exit = true;
            // Console.WriteLine("[{0}]", string.Join(", ", TSBuffer));
        }
    }
}
