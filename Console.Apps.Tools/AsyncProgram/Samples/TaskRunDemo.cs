using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncProgram.Samples
{
    public class TaskRunDemo
    {
        public AsyncLocal<string> AsyncLocal = new AsyncLocal<string>();

        public string Demo()
        {
            AsyncLocal.Value = "set in demo";

            return Task.Run(() => {
                Console.WriteLine("Value :"+ AsyncLocal.Value);
                return "Hello world";
            }).Result;
        }
    }
}
