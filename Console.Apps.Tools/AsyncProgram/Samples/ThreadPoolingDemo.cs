using System;
using System.Threading;

namespace AsyncProgram.Samples
{
    public class ThreadPoolingDemo
    {
        public void Demo()
        {
            Console.WriteLine("Thread Pool Execution");

            ProcessWithThreadPoolMethod();

            //Console.WriteLine("Thread Execution");

            //mywatch.Start();
            //ProcessWithThreadMethod();
            //mywatch.Stop();

        }

        private void ProcessWithThreadPoolMethod()
        {
            for (int i = 0; i <= 3; i++)
            {
                ThreadPool.QueueUserWorkItem(new WaitCallback(Process), i);
            }
        }

        private void ProcessWithThreadMethod()
        {
            for (int i = 0; i <= 10; i++)
            {
                Thread obj = new Thread(Process);
                obj.Start();
            }
        }

        private void Process(object callback)
        {
            Console.WriteLine("printing from process :"+ callback);
        }
    }
}
