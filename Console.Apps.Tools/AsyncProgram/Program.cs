using AsyncProgram.Samples;
using System;

namespace AsyncProgram
{
    public class Program
    {
        static void Main(string[] args)
        {
            //AsyncBreakfast ab = new AsyncBreakfast();
            //ab.BreakFastSynchronous();

            //TaskRunDemo();
            ThreadPoolingDemo tpd = new ThreadPoolingDemo();
            tpd.Demo();
            Console.Read();
        }

        public static void AsyncLocalDemo()
        {
            AsyncLocalDemo ald = new AsyncLocalDemo();
            var res = ald.AsyncWaitDemo().GetAwaiter().GetResult();
            Console.WriteLine(res);
        }

        public static void TaskRunDemo()
        {
            TaskRunDemo trd = new TaskRunDemo();

            var res = trd.Demo();
            Console.WriteLine(res);
        }
    }
}
