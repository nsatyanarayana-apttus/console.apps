using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncProgram.Samples
{
    public class AsyncLocalDemo
    {
        public AsyncLocal<string> AsyncLocal = new AsyncLocal<string>();

        public async Task<string> AsyncWaitDemo()
        {
            Console.WriteLine("---Entered Method AsyncWaitDemo");
            AsyncLocal.Value = "Set in method AsyncWaitDemo";
            Console.WriteLine("     "+AsyncLocal.Value);
            var res = await Method1();
            Console.WriteLine("     " + AsyncLocal.Value);
            //Console.WriteLine("---Exit Method AsyncWaitDemo");
            return res;
        }

        public async Task<string> Method1()
        {
            Console.WriteLine("----------Entered Method Method1");
            Console.WriteLine("             " + AsyncLocal.Value);
            AsyncLocal.Value = "Set in method Method1";
            Console.WriteLine("             " + AsyncLocal.Value);
            var res = await Method2();
            Console.WriteLine("             " + AsyncLocal.Value);
            //Console.WriteLine("---             Exit Method Method1");
            return res;
        }

        public async Task<string> Method2()
        {
            Console.WriteLine("-----------------Entered Method Method2");
            Console.WriteLine("                     " + AsyncLocal.Value);
            AsyncLocal.Value = "Set in method Method2";
            Console.WriteLine("                     " + AsyncLocal.Value);
            //Console.WriteLine("---Exit Method Method2");
            return await Task.FromResult<string>("Hello world");
        }
    }
}
