using System.Web;
using System.Web.Optimization;

namespace AdminLTE
{
    public class BundleConfig
    {
        // 有关绑定的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            #region 资源绑定源码

            //bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
            //            "~/Scripts/jquery-{version}.js"));

            //bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
            //            "~/Scripts/jquery.validate*"));

            //// 使用要用于开发和学习的 Modernizr 的开发版本。然后，当你做好
            //// 生产准备时，请使用 http://modernizr.com 上的生成工具来仅选择所需的测试。
            //bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
            //            "~/Scripts/modernizr-*"));

            //bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
            //          "~/Scripts/bootstrap.js",
            //          "~/Scripts/respond.js"));

            //bundles.Add(new StyleBundle("~/Styles/css").Include(
            //          "~/Styles/bootstrap.css",
            //          "~/Styles/site.css"));

            #endregion

            foreach (var item in ResourceManager.Scriptes)
            {
                foreach (var subItem in item.Value)
                {
                    if (!subItem.Ignore)
                    {
                        bundles.Add(new ScriptBundle(subItem.VirtualPath).Include(subItem.Files));
                    }
                }
            }
            foreach (var item in ResourceManager.Styles)
            {
                foreach (var subItem in item.Value)
                {
                    if (!subItem.Ignore)
                    {
                        bundles.Add(new StyleBundle(subItem.VirtualPath).Include(subItem.Files));
                    }
                }
            }
        }
    }
}
namespace System.Web
{
    using MvcBase.Helper;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    /// <summary>
    /// JavaScript 文件列表
    /// </summary>
    public enum ScriptFiles
    {
        [Resource("~/Scripts/Jquery", "~/Content/js/jquery-2.2.3.min.js")]
        Jquery,

        [Resource("~/Scripts/Bootstrap", "~/Content/bootstrap/js/bootstrap.min.js")]
        Bootstrap,

        [Resource("~/Scripts/Respond", "~/Content/bootstrap/js/respond.min.js")]
        Respond,

        [Resource("~/Scripts/Html5shiv", "~/Content/bootstrap/js/html5shiv.min.js")]
        Html5shiv,

        [Resource("~/Scripts/Layer", "~/Content/layer/layer.js")]
        Layer,

        [Resource("~/Scripts/App", "~/Content/dist/js/app.min.js")]
        App,
        [Resource("~/Scripts/Dashboard2", "~/Content/dist/js/pages/dashboard2.js")]
        Dashboard2,

        [Resource("~/Scripts/JqueryValidate",
            "~/Content/js/jquery.validate.min.js", OrderID = 1
            ),
            Resource("~/Scripts/JqueryValidateUnobtrusive",
                "~/Content/js/jquery.validate.unobtrusive.js"
                , OrderID = 2),
            Resource("~/Scripts/JqueryUnobtrusiveAjax",
                "~/Content/js/jquery.unobtrusive-ajax.min.js"
                , OrderID = 3)
        ]
        JqueryValidate,
        [Resource("~/Scripts/ICheck", "~/Content/plugins/iCheck/icheck.min.js")]
        ICheck,
        [Resource("~/Scripts/JqueryVcode", "~/Content/js/jquery.vcode.js")]
        JqueryVcode,
        [Resource("~/Scripts/Slimscroll", "~/Content/plugins/slimScroll/jquery.slimscroll.min.js")]
        Slimscroll,
        [Resource("~/Scripts/FastClick", "~/Content/plugins/fastclick/fastclick.js")]
        FastClick,
        [Resource("~/Scripts/Common", "~/Content/js/common.js")]
        Common,
    }

    /// <summary>
    /// CSS 文件列表
    /// </summary>
    public enum StyleFiles
    {
        [Resource("~/Styles/Bootstrap", "~/Content/bootstrap/css/bootstrap.min.css")]
        Bootstrap,

        [Resource("~/Styles/FontAwesome", "~/Content/font-awesome-4.7.0/css/font-awesome.min.css")]
        FontAwesome,

        [Resource("~/Styles/Ionicons", "~/Content/css/ionicons.min.css")]
        Ionicons,

        [Resource("~/Styles/JqueryJvectormap", "~/Content/plugins/jvectormap/jquery-jvectormap-1.2.2.css")]
        JqueryJvectormap,

        [Resource("~/Styles/AdminLTE", "~/Content/dist/css/AdminLTE.min.css")]
        AdminLTE,

        [Resource("~/Styles/AllSkins", "~/Content/dist/css/skins/_all-skins.min.css")]
        AllSkins,
        [Resource("~/Styles/ICheck", "~/Content/plugins/iCheck/all.css")]
        ICheck,
        [Resource("~/Styles/Style", "~/Content/css/style.css")]
        Style,
        
    }

    [AttributeUsageAttribute(AttributeTargets.Field, AllowMultiple = true)]
    public class ResourceAttribute : Attribute
    {
        public ResourceAttribute(string VirtualPath, params string[] Files)
        {
            this.VirtualPath = VirtualPath;
            this.Files = Files;
            this.Ignore = Ignore;
        }
        public ResourceAttribute(bool Ignore, string VirtualPath, params string[] Files)
        {
            this.VirtualPath = VirtualPath;
            this.Files = Files;
            this.Ignore = Ignore;
        }
        public string VirtualPath { get; set; }
        public string[] Files { get; set; }
        public bool Ignore { get; set; }

        public int OrderID { get; set; }
    }

    /// <summary>
    /// 资源管理
    /// JavaScript,CSS
    /// </summary>
    public static class ResourceManager
    {
        /// <summary>
        /// 绑定过的 JavaScript
        /// </summary>
        public static Dictionary<int, ResourceAttribute[]> Scriptes
        {
            get
            {
                string cacheName = "WebScripts";
                if (!CacheExtensions.CheckCache(cacheName))
                {
                    Dictionary<int, ResourceAttribute[]> result = new Dictionary<int, ResourceAttribute[]>();
                    var list = typeof(ScriptFiles).GetEnumListItem<ResourceAttribute>();
                    foreach (var item in list)
                    {
                        if (item.Attribute != null && item.Attribute.Length > 0)
                        {
                            result.Add(item.IntValue, item.Attribute.OrderBy(s => s.OrderID).ToArray());
                        }

                    }
                    CacheExtensions.SetCache(cacheName, result);
                }
                return CacheExtensions.GetCache<Dictionary<int, ResourceAttribute[]>>(cacheName);
            }
        }
        /// <summary>
        /// 绑定过的 CSS
        /// </summary>
        public static Dictionary<int, ResourceAttribute[]> Styles
        {
            get
            {
                string cacheName = "WebStyles";
                if (!CacheExtensions.CheckCache(cacheName))
                {
                    Dictionary<int, ResourceAttribute[]> result = new Dictionary<int, ResourceAttribute[]>();
                    var list = typeof(StyleFiles).GetEnumListItem<ResourceAttribute>();
                    foreach (var item in list)
                    {
                        result.Add(item.IntValue, item.Attribute.OrderBy(s => s.OrderID).ToArray());
                    }
                    CacheExtensions.SetCache(cacheName, result);
                }
                return CacheExtensions.GetCache<Dictionary<int, ResourceAttribute[]>>(cacheName);
            }
        }

        #region 渲染 JavaScript

        /// <summary>
        /// 加载 JavaScript
        /// </summary>
        /// <param name="Html"></param>
        /// <param name="scripts">要加载的脚本</param>
        /// <returns></returns>
        public static IHtmlString RenderScripts(this HtmlHelper Html, params ScriptFiles[] scripts)
        {
            var files = ResourceManager.Scriptes;
            List<string> names = new List<string>();

            if (scripts != null && scripts.Length > 0)
            {
                foreach (var item in scripts)
                {
                    if (files.ContainsKey((int)item))
                    {
                        foreach (var subItem in files[(int)item])
                        {
                            if (subItem.Ignore)
                                names.AddRange(subItem.Files);
                            else
                                names.Add(subItem.VirtualPath);
                        }
                    }

                }
            }
            else
            {
                foreach (var kv in files)
                {
                    foreach (var subItem in kv.Value)
                    {
                        if (subItem.Ignore)
                            names.AddRange(subItem.Files);
                        else
                            names.Add(subItem.VirtualPath);
                    }
                }
            }
            return System.Web.Optimization.Scripts.Render(names.ToArray());
        }
        public static IHtmlString RenderScripts(this HtmlHelper Html, string path)
        {
            return Web.Optimization.Scripts.Render(path);
        }
        public static IHtmlString RenderScripts(this HtmlHelper Html, string[] paths)
        {
            return Web.Optimization.Scripts.Render(paths);
        }

        #endregion

        #region 渲染 CSS

        public static IHtmlString RenderStyles(this HtmlHelper Html, params StyleFiles[] scripts)
        {
            var files = ResourceManager.Styles;

            List<string> names = new List<string>();

            if (scripts != null && scripts.Length > 0)
            {
                foreach (var item in scripts)
                {
                    if (files.ContainsKey((int)item))
                    {
                        foreach (var subItem in files[(int)item])
                        {
                            if (subItem.Ignore)
                                names.AddRange(subItem.Files);
                            else
                                names.Add(subItem.VirtualPath);
                        }
                    }
                }
            }
            else
            {
                foreach (var kv in files)
                {
                    foreach (var subItem in kv.Value)
                    {
                        if (subItem.Ignore)
                            names.AddRange(subItem.Files);
                        else
                            names.Add(subItem.VirtualPath);
                    }
                }
            }
            return System.Web.Optimization.Styles.Render(names.ToArray());
        }
        public static IHtmlString RenderStyles(this HtmlHelper Html, string path)
        {
            return Web.Optimization.Styles.Render(path);
        }
        public static IHtmlString RenderStyles(this HtmlHelper Html, string[] paths)
        {
            return Web.Optimization.Styles.Render(paths);
        }

        #endregion
    }
}