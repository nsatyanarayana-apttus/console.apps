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

            AsyncLocalDemo ald = new AsyncLocalDemo();
            var res = ald.AsyncWaitDemo().GetAwaiter().GetResult();
            Console.WriteLine(res);
            Console.Read();
        }
    }
}
