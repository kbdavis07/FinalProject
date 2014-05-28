using Common.Logging;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Jitc.Jobs
{
    public class Test1 : IJob
    {

        private static ILog logging = LogManager.GetLogger(typeof(Test1));

        public void Execute(IJobExecutionContext context)
        {
            var mydata = context.MergedJobDataMap["data"];

            logging.InfoFormat("Hello from job {0}", mydata);
        }

    }
}



