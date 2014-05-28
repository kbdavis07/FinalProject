using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml;
using System.Data;
using System.Data.Entity;
using System.Net;
using System.Web.Mvc;
using Jitc.Models;
using Jitc.Controllers;
using System.Xml.Linq;


namespace Jitc.Jobs
{
    public class XmlJob : IJob
    {
        public void Execute(IJobExecutionContext context)
            {

                Elmah.ErrorSignal.FromCurrentContext().Raise(new ApplicationException("Hello from XmlJob"));

                var timenow = DateTime.Now;

                Elmah.ErrorSignal.FromCurrentContext().Raise(new ApplicationException("It is:" + timenow));
            
            try 
                    {
                     
                     //XmlTextWriter xmlwriter = new XmlTextWriter(HttpContext.Server.MapPath("~/App_Data/Employee.xml"), Encoding.UTF8);

                        var billingData = XDocument.Load(HttpContext.Current.Server.MapPath("~/App_Data/Employee.xml"));


                        billingData.Root.Add(new XElement("item", new XElement("id", "ID"),

                            new XElement("customer", "Customer"),
                            new XElement("type", "Type"),
                            new XElement("hours", "5")));


                        billingData.Save(HttpContext.Current.Server.MapPath("~/App_Data/Billings.xml"));


                        //xmlwriter.Formatting = Formatting.Indented;
                        //xmlwriter.WriteStartDocument();
                        //xmlwriter.WriteStartElement("Employees");
                        //xmlwriter.WriteStartElement("Employee");
                        //xmlwriter.WriteAttributeString("type", "Permanent");
                        //xmlwriter.WriteElementString("ID", "100");
                        //xmlwriter.WriteElementString("FirstName", "From Controler");
                        //xmlwriter.WriteElementString("LastName", "Babu");
                        //xmlwriter.WriteElementString("Dept", "IT");
                        //xmlwriter.WriteEndElement();
                        //xmlwriter.WriteEndElement();
                        //xmlwriter.WriteEndDocument();
                        //xmlwriter.Flush();
                        //xmlwriter.Close();

                    }



                    catch (SchedulerException se)
                    {
                        string schedulerError = se.ToString();
                        Elmah.ErrorSignal.FromCurrentContext().Raise(new ApplicationException(schedulerError));
                        Elmah.ErrorSignal.FromCurrentContext().Raise(new ApplicationException("Error From XmlJob"));
                    }  
            
            
            }
   }

 
}