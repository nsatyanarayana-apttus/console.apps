using AsyncProgram.Samples;
using System;
using System.Collections.Generic;
using System.Text;

namespace AsyncProgram
{
    public class Program
    {
        static void Main(string[] args)
        {
            AsyncBreakfast ab = new AsyncBreakfast();
            //ab.BreakFastSynchronous();
            Console.Read();
        }
    }
}
