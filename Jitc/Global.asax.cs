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
        public static IScheduler Scheduler;


        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            ISchedulerFactory sf = new StdSchedulerFactory();
            Scheduler = sf.GetScheduler();

            Scheduler.Start();
        }

        protected void Application_End(object sender, EventArgs e)
        {
            Scheduler.Shutdown();
        }





    }
}
