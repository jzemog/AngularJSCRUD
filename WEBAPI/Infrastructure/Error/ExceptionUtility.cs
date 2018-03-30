using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Infrastructure.Error
{
    public class ExceptionUtility
    {
        public static void Log(Exception exc)
        {
            string logFile = "C:/AngularCRUDErrorLog.txt";
            logFile = HttpContext.Current.Server.MapPath(logFile);

            StreamWriter sw = new StreamWriter(logFile, true);

            sw.WriteLine("********** {0} **********", DateTime.Now);

            if (exc.InnerException != null)
            {
                sw.Write("Inner Exception Type: ");
                sw.WriteLine(exc.InnerException.GetType().ToString());
                sw.Write("Inner Error: ");
                sw.WriteLine(exc.InnerException.Message);
                sw.Write("Inner Source: ");
                sw.WriteLine(exc.InnerException.Source);
                if (exc.InnerException.StackTrace != null)
                {
                    sw.WriteLine("Inner Stack Trace: ");
                    sw.WriteLine(exc.InnerException.StackTrace);
                }
            }
            sw.Write("Exception Type: ");
            sw.WriteLine(exc.GetType().ToString());
            sw.WriteLine("Error: " + exc.Message);
            sw.WriteLine("Source: " + exc.TargetSite);
            sw.WriteLine("Stack Trace: ");
            if (exc.StackTrace != null)
            {
                sw.WriteLine(exc.StackTrace);
                sw.WriteLine();
            }
            sw.Close();
        }
    }
}