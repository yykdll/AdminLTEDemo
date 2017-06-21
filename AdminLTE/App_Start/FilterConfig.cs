using System;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace AdminLTE
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new AdminLTEHandleErrorAttribute());
        }

        public class AdminLTEHandleErrorAttribute : HandleErrorAttribute
        {
            private object _mutext = new object();

            public override void OnException(ExceptionContext context)
            {
                base.OnException(context);

                var e = context.Exception;
                lock (_mutext)
                {
                    string dir = HttpContext.Current.Server.MapPath("/Log");
                    if (!Directory.Exists(dir))
                    {
                        Directory.CreateDirectory(dir);
                    }

                    string path = string.Format("{0}/{1}.txt", dir, DateTime.Now.ToString("yyyyMMdd"));
                    StreamWriter sw = new StreamWriter(path, true);
                    sw.WriteLine(DateTime.Now.ToString());
                    sw.WriteLine(HttpContext.Current.Request.Url.ToString());
                    sw.WriteLine(e.Message);
                    sw.WriteLine(e.StackTrace);
                    sw.WriteLine("=====================================");
                    sw.Close();
                }
            }

        }
    }
}