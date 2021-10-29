using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;

namespace Problem01 {
    unsafe class Program {
        static readonly int processorCount = System.Environment.ProcessorCount;
        static byte[] Data_Global = new byte[1000000000];
        static long Sum_Global = 0;

        static int ReadData() {
            int returnData = 0;
            FileStream fs = new FileStream("Problem01.dat", FileMode.Open);
            BinaryFormatter bf = new BinaryFormatter();

            try {
                Data_Global = (byte[]) bf.Deserialize(fs);
            }
            catch (SerializationException se) {
                Console.WriteLine("Read Failed:" + se.Message);
                returnData = 1;
            }
            finally {
                fs.Close();
            }
            return returnData;
        }

        static void sum(int from, int to) {
            long in_sum = 0;
            fixed(byte* data_global = Data_Global)
            for (int i = from ; i < to ; i++) {
                byte num = data_global[i];
                data_global[i] = 0;
                if (num % 2 == 0) {
                    in_sum -= num;
                }
                else if (num % 3 == 0) {
                    in_sum += num * 2;
                }
                else if (num % 5 == 0) {
                    in_sum += num / 2;
                }
                else if (num % 7 == 0) {
                    in_sum += num / 3;
                }
            }
            Sum_Global += in_sum;
        }

        static bool isThreadWorking(Thread[] threads) {
            bool result = false;
            for (int i = 0 ; i < threads.Length ; i++) {
                if (threads[i].IsAlive) {
                    result = true;
                    break;
                }
            }
            return result;
        }

        static void Main(string[] args) {
            Stopwatch sw = new Stopwatch();
            int y;
            /* Read data from file */
            Console.Write("Data read...");
            y = ReadData();
            if (y == 0) {
                Console.WriteLine("Complete.");
            }
            else {
                Console.WriteLine("Read Failed!");
            }

            /* Create Thread */
            Console.WriteLine("Processer Count = " + processorCount);
            int mainTo = (int)((1 / (float)(processorCount)) * 1000000000);            
            Console.WriteLine("Main Thread process from " + 0 + " to " + mainTo);

            Thread[] threads = new Thread[processorCount - 1];
            for (int i = 0 ; i < processorCount - 1 ; i++) {
                int from = (int)(((float)(i + 1) / (float)(processorCount)) * 1000000000);
                int to = (int)(((float)(i + 2) / (float)(processorCount)) * 1000000000);
                Console.WriteLine("Thread " + (i + 1) + " process from " + from + " to " + to);
                threads[i] = new Thread(() => sum(from, to));
            }

            /* Start */
            Console.Write("\n\nWorking...");
            // long initial_time = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            
            sw.Start();

            for (int i = 0 ; i < threads.Length ; i++) {
                threads[i].Start();
            }
            sum(0, mainTo);

            while(isThreadWorking(threads)) {}

            sw.Stop();
            Console.WriteLine("Done.");
            // long used_time = DateTimeOffset.Now.ToUnixTimeMilliseconds() - initial_time;
            /* Result */
            Console.WriteLine("Summation result: {0}", Sum_Global);
            Console.WriteLine("Time used: " + sw.ElapsedMilliseconds.ToString() + "ms");
            
        }
    }
}