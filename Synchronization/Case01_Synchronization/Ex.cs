using System;
using System.Diagnostics;
using System.Threading;


//========Ex-00========//
// namespace Case02
// {
//     class Program
//     {
//         private static int sum = 0;

//         static void plus()
//         {
//             int i;
//             for (i = 1; i <10000001;i++)
//                 sum+=i;

//         }
//         static void minus()
//         {
//             int i;
//             for (i = 0 ; i<10000000;i++)
//             {
//                 sum -= i;
//             }
//         }
//         static void Main(string[] args)
//         {
//             Stopwatch sw = new Stopwatch();
//             Console.WriteLine("Start...");
//             sw.Start();
//             plus();
//             minus();
//             sw.Stop();
//             Console.WriteLine("sum = {0}",sum);
//             Console.WriteLine("Time used : "+sw.ElapsedMilliseconds.ToString()+"ms");

//         }
//     }
// }


// ========Ex-01========//
// namespace Case02
// {
//     class Program
//     {
//         private static int sum = 0;

//         static void plus()
//         {
//             int i;
//             for (i = 1; i <10000001;i++)
//             sum+=i;
//         }
//         static void minus()
//         {
//             int i;
//             for (i = 0 ; i<10000000;i++)
//                     sum -= i;
//         }
//         static void Main(string[] args)
//         {
//             Thread P = new Thread(new ThreadStart(plus));
//             Thread M = new Thread(new ThreadStart(minus));

//             Stopwatch sw = new Stopwatch();
//             Console.WriteLine("Start...");
//             sw.Start();

//             P.Start();
//             M.Start();

//             P.Join();
//             M.Join();


//             sw.Stop();
//             Console.WriteLine("sum = {0}",sum);
//             Console.WriteLine("EX01 Time used : "+sw.ElapsedMilliseconds.ToString()+"ms");
//         }
//     }
// }

////========Ex-02========//
// namespace Case02
// {
//     class Program
//     {
//         private static int sum = 0;
//         private static object _Lock = new object();

//         static void plus()
//         {
//             int i;
//             for (i = 1; i <10000001;i++)
//                 lock(_Lock)
//                 {
//                     sum+=i;
//                 }
//         }
//         static void minus()
//         {
//             int i;
//             for (i = 0 ; i<10000000;i++)
//                 lock(_Lock)
//                     {
//                         sum -= i;
//                     }
//         }
//         static void Main(string[] args)
//         {
//             Thread P = new Thread(new ThreadStart(plus));
//             Thread M = new Thread(new ThreadStart(minus));

//             Stopwatch sw = new Stopwatch();
//             Console.WriteLine("Start...");
//             sw.Start();

//             P.Start();
//             M.Start();

//             P.Join();
//             M.Join();

//             // plus();
//             // minus();
//             sw.Stop();
//             Console.WriteLine("sum = {0}",sum);
//             Console.WriteLine("EX02 Time used : "+sw.ElapsedMilliseconds.ToString()+"ms");
//         }
//     }
// }

//========Ex-03========//
// namespace Case02
// {
//     class Program
//     {
//         private static int sum = 0;
//         private static object _Lock = new object();


//         static void plus()
//         {
//             int i;
//             lock(_Lock)
//                 {
//                     for (i = 1; i <10000001;i++)
//                         sum+=i;
//                 }


//         }
//         static void minus()
//         {
//             int i;
//             lock(_Lock)
//             {
//                 for (i = 0 ; i<10000000;i++)
//                     sum -= i;
//             }
//         }
//         static void Main(string[] args)
//         {
//             Thread P = new Thread(new ThreadStart(plus));
//             Thread M = new Thread(new ThreadStart(minus));

//             Stopwatch sw = new Stopwatch();
//             Console.WriteLine("Start...");
//             sw.Start();

//             P.Start();
//             M.Start();

//             P.Join();
//             M.Join();


//             // plus();
//             // minus();
//             sw.Stop();
//             Console.WriteLine("sum = {0}",sum);
//             Console.WriteLine("EX03 Time used : "+sw.ElapsedMilliseconds.ToString()+"ms");
//         }
//     }
// }


////========Ex-04========//
// namespace Case02 
// {
//     class Program {
//         private static string x = "";
//         private static int exitflag = 0;

//         static void ThReadX(object i) {
//             string xx;
//             while(exitflag==0) {
//                 if (x == "") {
//                     Console.Write("Input: ");
//                     xx = Console.ReadLine();
//                     x = xx;
//                 }

//             }
//         }

//         static void ThWriteX() {           
//             while (exitflag == 0) {
//                 if (x != "") {
//                     if (x == "exit") {
//                         Console.WriteLine("X = {0}", x);
//                         exitflag = 1;
//                     }
//                     else {
//                         Console.WriteLine("X = {0}", x);
//                         x = "";
//                     }
//                 }
//             }
//         }

//         static void Main(string[] args) {
//             Thread A = new Thread(ThReadX);
//             Thread B = new Thread(ThWriteX);

//             A.Start();
//             B.Start();
//         }
//     }
// }


////========Ex-05========//
namespace Case02
{
 class Program {
        private static string x = "";
        private static int exitflag = 0;
        private static int updateFlag = 0;
        private static object _Lock = new object();
        static void ThReadX() {
            string xx;
            while (exitflag == 0) {
                lock (_Lock) {
                    Console.Write("Input: ");
                    xx = Console.ReadLine();
                    if (xx == "exit") {
                        exitflag = 1;
                    }
                    x = xx;
                }
            }     
        }
        static void ThWriteX(object i) {
            while(exitflag == 0) {
                lock (_Lock) {
                    if (x != "exit" && x != "") {
                        Console.WriteLine("***Thread {0} : x = {1}***", i, x);
                        x = "";
                    }
                }
            }
            Console.WriteLine("---Thread {0} exit---", i);
        }
        static void Main(string[] args) {
            Thread A = new Thread(ThReadX);
            Thread B = new Thread(ThWriteX);
            Thread C = new Thread(ThWriteX);
            Thread D = new Thread(ThWriteX);
            A.Start();
            B.Start(1);
            C.Start(2);
            D.Start(3);
        }
    }
}