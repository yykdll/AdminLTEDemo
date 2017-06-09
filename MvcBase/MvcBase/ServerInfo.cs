using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Web;

namespace MvcBase
{
  public static class ServerInfo
  {
    private static Process process_0;

    public static string SystemRunTime
    {
      get
      {
        int tickCount = Environment.TickCount;
        if (tickCount < 0)
          tickCount += int.MaxValue;
        TimeSpan timeSpan = TimeSpan.FromSeconds((double) (tickCount / 1000));
        return timeSpan.Days.ToString() + "天 " + timeSpan.Hours.ToString() + "小时 " + timeSpan.Minutes.ToString() + "分 " + timeSpan.Seconds.ToString() + "秒";
      }
    }

    public static string ServerOS
    {
      get
      {
        return Environment.OSVersion.VersionString;
      }
    }

    public static string AppRunMemony
    {
      get
      {
        return string.Format("{0}M", (object) (ServerInfo.process_0.WorkingSet64 / 1024L / 1024L));
      }
    }

    public static string AppRunVirtualMemony
    {
      get
      {
        return string.Format("{0}M", (object) (ServerInfo.process_0.VirtualMemorySize64 / 1024L / 1024L));
      }
    }

    public static string String_0
    {
      get
      {
        return HttpContext.Current.Request.ServerVariables["Server_SoftWare"].ToString();
      }
    }

    public static string PhysicalApplicationPath
    {
      get
      {
        return HttpContext.Current.Request.PhysicalApplicationPath;
      }
    }

    public static string SystemPath
    {
      get
      {
        return Environment.SystemDirectory.ToString();
      }
    }

    public static string TimeOut
    {
      get
      {
        return (HttpContext.Current.Server.ScriptTimeout / 1000).ToString() + "秒";
      }
    }

    public static string Language
    {
      get
      {
        return CultureInfo.InstalledUICulture.EnglishName;
      }
    }

    public static string AspnetVer
    {
      get
      {
        return Environment.Version.Major.ToString() + "." + (object) Environment.Version.Minor + (object) Environment.Version.Build + "." + (object) Environment.Version.Revision;
      }
    }

    public static string CpuNum
    {
      get
      {
        return Environment.GetEnvironmentVariable("NUMBER_OF_PROCESSORS").ToString();
      }
    }

    public static string CpuType
    {
      get
      {
        return Environment.GetEnvironmentVariable("PROCESSOR_IDENTIFIER").ToString();
      }
    }

    public static string MemoryNet
    {
      get
      {
        return ((double) Process.GetCurrentProcess().WorkingSet64 / 1048576.0).ToString("N2") + "M";
      }
    }

    public static string CpuNet
    {
      get
      {
        return Process.GetCurrentProcess().TotalProcessorTime.TotalSeconds.ToString("N0");
      }
    }

    public static string SessionNum
    {
      get
      {
        return HttpContext.Current.Session.Contents.Count.ToString();
      }
    }

    public static string SessionID
    {
      get
      {
        return HttpContext.Current.Session.Contents.SessionID;
      }
    }

    public static string SystemUser
    {
      get
      {
        return Environment.UserName;
      }
    }
  }
}
