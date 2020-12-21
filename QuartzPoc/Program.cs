using Quartz;
using Quartz.Impl;
using System;
using System.Threading.Tasks;

namespace QuartzPoc
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Program prog = new Program();
            await prog.WithCronDemo();
        }

        public async Task WithCronDemo()
        {
            Console.WriteLine("Hello Schedular!");
            // construct a scheduler factory
            StdSchedulerFactory factory = new StdSchedulerFactory();

            // get a scheduler
            IScheduler scheduler = await factory.GetScheduler();
            await scheduler.Start();

            // define the job and tie it to our HelloJob class
            IJobDetail job = JobBuilder.Create<HelloJob>()
                .WithIdentity("myJob", "group1")
                .Build();

            ITrigger trigger = TriggerBuilder.Create()
                    .WithIdentity("trigger3", "group1")
                    .WithCronSchedule("0/5 * * * * ?")
                    .ForJob("myJob", "group1")
                    .Build();

            await scheduler.ScheduleJob(job, trigger);
            Console.ReadLine();
        }

        public async Task NormalSchedularDemo()
        {
            Console.WriteLine("Hello Schedular!");
            // construct a scheduler factory
            StdSchedulerFactory factory = new StdSchedulerFactory();

            // get a scheduler
            IScheduler scheduler = await factory.GetScheduler();
            await scheduler.Start();
            // define the job and tie it to our HelloJob class
            IJobDetail job = JobBuilder.Create<HelloJob>()
                .WithIdentity("myJob", "group1")
                .Build();

            // Trigger the job to run now, and then every 40 seconds
            ITrigger trigger = TriggerBuilder.Create()
              .WithIdentity("myTrigger", "group1")
              .StartNow()
              .WithSimpleSchedule(x => x
                  .WithIntervalInSeconds(10)
                  .WithRepeatCount(3))
              .Build();

            await scheduler.ScheduleJob(job, trigger);
            Console.ReadLine();
        }
    }
}
