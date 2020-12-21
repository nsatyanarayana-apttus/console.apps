using Quartz;
using System;
using System.Threading.Tasks;

namespace QuartzPoc
{
    class HelloJob : IJob
	{
		public async Task Execute(IJobExecutionContext context)
		{
			JobKey key = context.JobDetail.Key;
            Console.WriteLine(key.Name);
			await Console.Out.WriteLineAsync("Greetings from HelloJob!");
		}

    }
}
