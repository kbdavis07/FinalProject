﻿using Jitc.Jobs;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace Jitc.Controllers
{
    public class JobsController : Controller
    {
        
        // GET: Jobs
        public ActionResult Index()
        {

            StartScheduler();
            return View();
        }

        // GET: Jobs/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Jobs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Jobs/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Jobs/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Jobs/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Jobs/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Jobs/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }



        public void StartScheduler()

        {

           

            //// Grab the Scheduler instance from the Factory 
            IScheduler sched = StdSchedulerFactory.GetDefaultScheduler();

            //// and start it off
            //sched.Start();


            //Elmah.ErrorSignal.FromCurrentContext().Raise(new ApplicationException("-- Initialization Complete --"));


            //// computer a time that is on the next round minute
            //DateTimeOffset runTime = DateBuilder.EvenMinuteDate(DateTimeOffset.Now.ToLocalTime());

            Elmah.ErrorSignal.FromCurrentContext().Raise(new ApplicationException("-- Scheduling Job --"));

            // define the job and tie it to our HelloJob class
            IJobDetail job = JobBuilder.Create<XmlJob>()
                .WithIdentity("job1", "group1")
                .Build();

            // Trigger the job to run on the next round minute
            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("trigger1", "group1")
                .StartNow()
                .Build();

            // Tell quartz to schedule the job using our trigger
            sched.ScheduleJob(job, trigger);

            // Start up the scheduler (nothing can actually run until the 
            // scheduler has been started)
            //sched.Start();
           Elmah.ErrorSignal.FromCurrentContext().Raise(new ApplicationException("-- Started Scheduler --"));

            // wait long enough so that the scheduler as an opportunity to 
            // run the job!
         Elmah.ErrorSignal.FromCurrentContext().Raise(new ApplicationException("-- Waiting 60 seconds..? --"));

            // wait 65 seconds to show jobs
            Thread.Sleep(TimeSpan.FromSeconds(10));

            Elmah.ErrorSignal.FromCurrentContext().Raise(new ApplicationException("-- 50 seconds"));

            Thread.Sleep(TimeSpan.FromSeconds(50));

        }



    }
}
