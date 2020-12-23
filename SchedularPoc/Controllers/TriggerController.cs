using Microsoft.AspNetCore.Mvc;
using Quartz;
using Quartz.Impl;
using SchedularPoc.Jobs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SchedularPoc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TriggerController : ControllerBase
    {
        private IScheduler scheduler;
        private string groupname = "triggergroup";
        public TriggerController()
        {
            StdSchedulerFactory factory = new StdSchedulerFactory();

            // get a scheduler
            IScheduler result = factory.GetScheduler().Result;
            scheduler = result;
            scheduler.Start();
        }

        [HttpGet("{name}")]
        public async Task<IActionResult> GetAsync(string name)
        {
            JobKey key = new JobKey(name, groupname);
            bool isExist = await scheduler.CheckExists(key);
            if (isExist)
            {
                IJobDetail job = await scheduler.GetJobDetail(key);
                //return Ok(job.Description+job.Key.ToString());
                return Ok(job.ToString());
            }
            else
            {
                return NotFound("Job does not exist with given key:" + name);
            }
        }

        [HttpDelete("{name}")]
        public async Task<IActionResult> DeleteAsync(string name)
        {
            JobKey key = new JobKey(name, groupname);
            bool isExist = await scheduler.CheckExists(key);
            if (isExist)
            {
                await scheduler.DeleteJob(key);
                return Ok("Job Deleted");
            }
            else
            {
                return NotFound("Job does not exist with given key:" + name);
            }
        }

        [HttpPost("createjob/{name}")]
        public async Task<IActionResult> PostAsync(string name, Dictionary<string, string> dataMap)
        {
            JobKey key = new JobKey(name, groupname);
            bool isExist = await scheduler.CheckExists(key);
            if (!isExist)
            {
                // define the job and tie it to our HelloJob class
                IJobDetail job = JobBuilder.Create<TriggerJob>()
                    .WithIdentity(name, groupname)
                    .Build();

                // Trigger the job to run now, and then every 40 seconds
                ITrigger trigger = TriggerBuilder.Create()
                  .WithIdentity("myTrigger2", groupname)
                  .StartNow()
                  .WithSimpleSchedule(x => x
                      .WithIntervalInSeconds(5)
                      .WithRepeatCount(100))
                  .Build();

                await scheduler.ScheduleJob(job, trigger);
                return Ok("Job Created");
            }
            else
            {
                return Ok("Job already exist with given key:" + name);
            }


        }
    }
}
