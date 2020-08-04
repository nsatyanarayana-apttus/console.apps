using Apttus.OpenTracingTelemetry;
using System;

namespace ServiceA.Service
{
    public class MailBox : IRunnable
    {
        private string ID;
        public MailBox(string id)
        {
            ID = id;
        }

        public void Run()
        {
            int hashcode = (int)ApttusGlobalTracer.Current?.GetHashCode();
            Console.WriteLine("Executing in runnable code:"+ hashcode);
        }
    }
}
