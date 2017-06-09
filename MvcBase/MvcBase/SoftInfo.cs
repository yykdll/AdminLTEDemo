using System;
using System.ComponentModel;
using System.Configuration;
using System.Web;

namespace MvcBase
{
    public static class SoftInfo
    {
        [DisplayName("系统名称")]
        public static string FrameWorkName
        {
            get
            {
                return "渊博通用框架V1.0";
            }
        }

        public static string SoftName
        {
            get
            {
                return ConfigurationManager.AppSettings["SoftName"];
            }
        }

        [DisplayName("当前使用缓存数")]
        public static string CurrentCache
        {
            get
            {
                return HttpRuntime.Cache.Count.ToString();
            }
        }

        [DisplayName("可用缓存大小")]
        public static string AvailableCache
        {
            get
            {
                return (HttpRuntime.Cache.EffectivePrivateBytesLimit / 1048576L).ToString() + "MB";
            }
        }

        [DisplayName("服务器操作系统")]
        public static string ServerOS
        {
            get
            {
                return ServerInfo.ServerOS;
            }
        }

        [DisplayName("服务器运行时间")]
        public static string SystemRunTime
        {
            get
            {
                return ServerInfo.SystemRunTime;
            }
        }

        [DisplayName("应用运行时间")]
        public static string AppRunTime
        {
            get
            {
                int tickCount = Environment.TickCount;
                if (tickCount < 0)
                    tickCount += int.MaxValue;
                TimeSpan timeSpan = TimeSpan.FromSeconds((double)(tickCount / 1000));
                return timeSpan.Days.ToString() + "天 " + timeSpan.Hours.ToString() + "小时 " + timeSpan.Minutes.ToString() + "分 " + timeSpan.Seconds.ToString() + "秒";
            }
        }

        [DisplayName("物理内存使用")]
        public static string AppRunMemony
        {
            get
            {
                return ServerInfo.AppRunMemony;
            }
        }

        [DisplayName("虚拟内存使用")]
        public static string AppRunVirtualMemony
        {
            get
            {
                return ServerInfo.AppRunVirtualMemony;
            }
        }

        [DisplayName("IIS版本号")]
        public static string String_0
        {
            get
            {
                return ServerInfo.String_0;
            }
        }

        [DisplayName("物理路径")]
        public static string PhysicalApplicationPath
        {
            get
            {
                return ServerInfo.PhysicalApplicationPath;
            }
        }

        [DisplayName("系统所在文件夹")]
        public static string SystemPath
        {
            get
            {
                return ServerInfo.SystemPath;
            }
        }

        [DisplayName("脚本超时时间")]
        public static string TimeOut
        {
            get
            {
                return ServerInfo.TimeOut;
            }
        }

        [DisplayName("服务器语言")]
        public static string ServerLanguage
        {
            get
            {
                return ServerInfo.Language;
            }
        }

        [DisplayName("Asp.net版本号")]
        public static string AspnetVer
        {
            get
            {
                return ServerInfo.AspnetVer;
            }
        }

        [DisplayName("CPU总数")]
        public static string CpuNum
        {
            get
            {
                return ServerInfo.CpuNum;
            }
        }

        [DisplayName("CPU类型")]
        public static string CpuType
        {
            get
            {
                return ServerInfo.CpuType;
            }
        }

        [DisplayName("Asp.net所占内存")]
        public static string MemoryNet
        {
            get
            {
                return ServerInfo.MemoryNet;
            }
        }

        [DisplayName("Asp.net所占CPU")]
        public static string CpuNet
        {
            get
            {
                return ServerInfo.CpuNet;
            }
        }

        [DisplayName("当前Session数量")]
        public static string SessionNum
        {
            get
            {
                return ServerInfo.SessionNum;
            }
        }

        [DisplayName("当前SessionID")]
        public static string SessionID
        {
            get
            {
                return ServerInfo.SessionID;
            }
        }

        [DisplayName("当前系统用户名")]
        public static string SystemUser
        {
            get
            {
                return ServerInfo.SystemUser;
            }
        }
    }
}
