using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Common.Logging;



namespace Jitc
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public static ISchedulerFactory Factory;
        public static IScheduler Scheduler;


        protected void Application_Start()
        {

            Elmah.ErrorSignal.FromCurrentContext().Raise(new ApplicationException("Application Start"));
  
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

           
                ISchedulerFactory sf = new StdSchedulerFactory();
                Scheduler = sf.GetScheduler();

                Scheduler.Start();
                Elmah.ErrorSignal.FromCurrentContext().Raise(new ApplicationException("Scheduler Started Successfully"));
          

          
               //string schedulerError = se.ToString();
               //Elmah.ErrorSignal.FromCurrentContext().Raise(new ApplicationException(schedulerError));
           
        
        
        }

        protected void Application_End(object sender, EventArgs e)
        {

            Elmah.ErrorSignal.FromCurrentContext().Raise(new ApplicationException("Application End"));

          try
          {
              Scheduler.Shutdown();
              Elmah.ErrorSignal.FromCurrentContext().Raise(new ApplicationException("Scheduler ShutDown Successfully"));
          }

          catch (SchedulerException se)
          {
              string schedulerError = se.ToString();
              Elmah.ErrorSignal.FromCurrentContext().Raise(new ApplicationException(schedulerError));
          }  
        
        }


       protected void Application_Error(object sender, EventArgs e)
       {
           Elmah.ErrorSignal.FromCurrentContext().Raise(new ApplicationException("Application Error:" + e));
       }


       //protected void Application_BeginRequest(object sender, EventArgs e)
       //{

       //    Elmah.ErrorSignal.FromCurrentContext().Raise(new ApplicationException("BeginRequest"));
       //}

       // protected void Application_EndRequest(object sender, EventArgs e)
       //{
       //    Elmah.ErrorSignal.FromCurrentContext().Raise(new ApplicationException("EndRequest"));
       //}


    }
}
