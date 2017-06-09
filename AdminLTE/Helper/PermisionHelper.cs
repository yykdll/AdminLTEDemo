using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Routing;
using AdminLTE.Helper;

namespace System.Web.Mvc
{
    public static class PermisionHelper
    {
        public static bool IntPare(this int single, int? plus)
        {
            if (!plus.HasValue) return false;
            return single == (plus.Value & single);
        }

        /// <summary>
        /// 判断当前控制器下用户是否有对应的权限
        /// <para>True : 有</para>
        /// <para>False : 没有</para>
        /// </summary>
        /// <param name="RouteData">当前目录信息</param>
        /// <param name="permission">要判断的权限点</param>
        /// <returns></returns>
        public static bool CheckRole(this RouteData RouteData, AdminLTE.Enum.PermissionType permission)
        {
            var loginInfo = LoginInfoHelper.Current();
            if (loginInfo.IsAdmin) return true;

            //var areaName = RouteData.DataTokens["area"] + "";
            var controllerName = RouteData.Values["controller"].ToString().ToLower() + "";
            var menuCache = MvcBase.Unity.Get<AdminLTE.Domain.Services.IMenuService>().ListCache();
            string menuID=string.Empty;
            foreach (var menu in menuCache)
            {
                menuID=menu.Children.SingleAndInit(m => m.Url.ToLower().Contains(controllerName)).ID;
                if (!string.IsNullOrEmpty(menuID))
                    break;
            }

            if (loginInfo.MenuIDs.Contains(menuID))
                return ((int)permission).IntPare(loginInfo.Permissions[menuID]);
            return false;
        }

        /// <summary>
        /// 判断当前控制器下用户是否有对应的权限
        /// <para>True : 有</para>
        /// <para>False : 没有</para>
        /// </summary>
        /// <param name="Html">视图辅助类</param>
        /// <param name="permission">要判断的权限点</param>
        /// <returns></returns>
        public static bool CheckRole(this HtmlHelper Html, AdminLTE.Enum.PermissionType permission)
        {
            return Html.ViewContext.RequestContext.RouteData.CheckRole(permission);
        }

        /// <summary>
        /// 判断当前控制器下用户是否有对应的权限
        /// <para>True : 有</para>
        /// <para>False : 没有</para>
        /// </summary>
        /// <param name="controller">控制器</param>
        /// <param name="permission">要判断的权限点</param>
        /// <returns></returns>
        public static bool CheckRole(this Controller controller, AdminLTE.Enum.PermissionType permission)
        {
            return controller.RouteData.CheckRole(permission);
        }
    }
}