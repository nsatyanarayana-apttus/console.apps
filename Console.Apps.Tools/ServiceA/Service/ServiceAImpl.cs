using Apttus.OpenTracingTelemetry;
using OpenTracing;
using ServiceA.Service;
using System;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace ServiceB.Service
{
    public class ServiceAImpl : IServiceA
    {
        private IApttusOpenTracer Tracer;
        private IWebActorService WebActorService;

        protected static readonly WaitCallback Executor = t => { ((IRunnable)t).Run(); };

        public ServiceAImpl(IApttusOpenTracer tracer, IWebActorService webactorservice)
        {
            Tracer = tracer;
            WebActorService = webactorservice;
        }

        public async Task<string> GetServiceAMessageTest1Async(string id)
        {

            using (Tracer.BuildActiveSpan("GetServiceAMessageAsync-ActiveSpan2-" + id,true))
            {
                Tracer.ActiveSpan.Log("ServiceAImpl before calling inner method");
                await Task.Delay(TimeSpan.FromSeconds(5));
                using (Tracer.BuildActiveSpan("GetServiceAMessageAsync-ActiveSpan3-" + id,true))
                {
                    await Task.Delay(TimeSpan.FromSeconds(5));
                }
                Tracer.ActiveSpan.Log("ServiceAImpl before calling inner method but within");
            }

            Tracer.ActiveSpan.Log("Out side Using Blocks -" + id);
            return "Waited";
        }

        public async Task<string> GetServiceAMessageTest2Async(string id)
        {

            using (Tracer.BuildActiveSpan("GetServiceAMessageAsync-ActiveSpan2-" + id,true))
            {
                Tracer.ActiveSpan.Log("ServiceAImpl before calling inner method");
                await Task.Delay(TimeSpan.FromSeconds(5));
                using (Tracer.BuildActiveSpan("GetServiceAMessageAsync-ActiveSpan3-" + id,true))
                {
                    await InnerMethod1(id);
                }
                Tracer.ActiveSpan.Log("ServiceAImpl before calling inner method but within");
            }

            Tracer.ActiveSpan.Log("Out side Using Blocks -" + id);
            return "Waited";
        }

        public async Task<string> GetServiceAMessageTest3Async(string id)
        {

            using (Tracer.BuildActiveSpan("GetServiceAMessageAsync-ActiveSpan2-" + id,true))
            {
                Tracer.ActiveSpan.Log("ServiceAImpl before calling inner method");
                await Task.Delay(TimeSpan.FromSeconds(5));
                using (Tracer.BuildActiveSpan("GetServiceAMessageAsync-ActiveSpan3-" + id,true))
                {
                    await InnerMethod3(id);
                }
                Tracer.ActiveSpan.Log("ServiceAImpl before calling inner method but within");
            }

            Tracer.ActiveSpan.Log("Out side Using Blocks -" + id);
            return "Waited";
        }

        public Task<string> ProcessUsingThreadPool(string id)
        {
            IRunnable runnable = new MailBox(id);
            ThreadPool.QueueUserWorkItem(Executor, runnable);
            return Task.FromResult<string>("done");
        }

        private async Task<string> InnerMethod1(string id)
        {

            using (Tracer.BuildActiveSpan("InnerMethod1--ActiveSpan4-" + id,true))
            {
                await Task.Delay(TimeSpan.FromSeconds(5));
                using (Tracer.BuildActiveSpan("InnerMethod1-ActiveSpan5-" + id,true))
                {
                    await InnerMethod2(id);
                }
                Tracer.ActiveSpan.Log("InnerMethod1 method");
            }

            Tracer.ActiveSpan.Log("Out side Using Blocks -" + id);
            return "Waited";
        }

        private async Task<string> InnerMethod2(string id)
        {

            using (Tracer.BuildActiveSpan("InnerMethod2-ActiveSpan6-" + id,true))
            {
                await Task.Delay(TimeSpan.FromSeconds(5));
                int hashcode = Tracer.GetHashCode();
                await WebActorService.Ask<string>("Sending to actor system :"+ hashcode);
                Tracer.ActiveSpan.Log("InnerMethod2 method");
            }

            Tracer.ActiveSpan.Log("Out side Using Blocks -" + id);
            return "Waited";
        }

        private async Task<string> InnerMethod3(string id)
        {

            using (Tracer.BuildActiveSpan("InnerMethod2-ActiveSpan6-" + id,true))
            {
                await Task.Delay(TimeSpan.FromSeconds(5));
                int hashcode = Tracer.GetHashCode();
                await WebActorService.Ask<int>(hashcode);
                Tracer.ActiveSpan.Log("InnerMethod2 method");
            }

            Tracer.ActiveSpan.Log("Out side Using Blocks -" + id);
            return "Waited";
        }

        public Task<string> SendToSockerServer(string id)
        {

            int hashcode = (int)ApttusGlobalTracer.Current?.GetHashCode();
            TcpClient client = new TcpClient();
            //Connect to the server
            client.Connect("localhost", 5053);

            String str = "Hello world";

            //Get the network stream
            NetworkStream stream = client.GetStream();
            //Converting string to byte array
            byte[] bytesToSend = System.Text.Encoding.ASCII.GetBytes(str);
            //Sending the byte array to the server
            client.Client.Send(bytesToSend);
            return Task.FromResult<string>(id);
        }
    }
}
