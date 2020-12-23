using Quartz;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace SchedularPoc.Jobs
{
    class HelloJob : IJob
	{
		public async Task Execute(IJobExecutionContext context)
		{
			JobKey key = context.JobDetail.Key;
            Console.WriteLine(key.Name);
			Debug.WriteLine("Greetings from HelloJob!");
			await Console.Out.WriteLineAsync("Ok");
		}

    }
}
