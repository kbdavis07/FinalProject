using Common.Logging;
using Jitc.Models;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Jitc.Controllers;
using Quartz.Impl;
using System.Threading;

namespace Jitc.Jobs
{
    public class Test1 : IJob
    {

        public string Name
        {
            get { throw new NotImplementedException(); }
        }

        public virtual void Execute(IJobExecutionContext context) 
        {

            Elmah.ErrorSignal.FromCurrentContext().Raise(new ApplicationException("------- Initializing ----------------------"));

            // First we must get a reference to a scheduler
            ISchedulerFactory sf = new StdSchedulerFactory();
            IScheduler sched = sf.GetScheduler();

            Elmah.ErrorSignal.FromCurrentContext().Raise(new ApplicationException("------- Initialization Complete -----------"));


            // computer a time that is on the next round minute
            DateTimeOffset runTime = DateBuilder.EvenMinuteDate(DateTimeOffset.UtcNow);

            Elmah.ErrorSignal.FromCurrentContext().Raise(new ApplicationException("------- Scheduling Job  -------------------"));

            // define the job and tie it to our HelloJob class
            IJobDetail job = JobBuilder.Create<XmlJob>()
                .WithIdentity("job1", "group1")
                .Build();

            // Trigger the job to run on the next round minute
            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("trigger1", "group1")
                .StartAt(runTime)
                .Build();

            // Tell quartz to schedule the job using our trigger
            sched.ScheduleJob(job, trigger);
   Elmah.ErrorSignal.FromCurrentContext().Raise(new ApplicationException(string.Format("{0} will run at: {1}", job.Key, runTime.ToString("r"))));

            // Start up the scheduler (nothing can actually run until the 
            // scheduler has been started)
            sched.Start();
           Elmah.ErrorSignal.FromCurrentContext().Raise(new ApplicationException("------- Started Scheduler -----------------"));

            // wait long enough so that the scheduler as an opportunity to 
            // run the job!
         Elmah.ErrorSignal.FromCurrentContext().Raise(new ApplicationException("------- Waiting 65 seconds... -------------"));

            // wait 65 seconds to show jobs
            Thread.Sleep(TimeSpan.FromSeconds(65));

            // shut down the scheduler
           Elmah.ErrorSignal.FromCurrentContext().Raise(new ApplicationException("------- Shutting Down ---------------------"));
            sched.Shutdown(true);
           Elmah.ErrorSignal.FromCurrentContext().Raise(new ApplicationException("------- Shutdown Complete -----------------"));
        }
    }
}


