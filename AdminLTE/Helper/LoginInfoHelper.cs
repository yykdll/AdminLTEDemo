using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using AdminLTE.Domain;
using AdminLTE.Domain.Services;
using MvcBase.Enum;
using MvcBase.Helper;

namespace AdminLTE.Helper
{
    /// <summary>
    /// 登陆信息
    /// </summary>
    public class LoginInfo
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public String ID { get; set; }

        public bool IsAdmin { get; set; }

        public string UserName { get; set; }
        public List<string> MenuIDs { get; set; }
        public Dictionary<string, int> Permissions { get; set; }
        //public List<>
    }



    /// <summary>
    /// 登陆信息工具
    /// </summary>
    public static class LoginInfoHelper
    {
        public const string LoginCacheName = "LoginInfo_";
        public static void Login(this Employee employee)
        {
            if (employee == null) return;

            //返回 Coookie 票证 
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, employee.ID.ToString(), DateTime.Now,
            DateTime.Now.AddMinutes(60), false, "manager", FormsAuthentication.FormsCookiePath);
            HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(ticket));
            cookie.Domain = System.Web.Security.FormsAuthentication.CookieDomain;
            if (ticket.IsPersistent) cookie.Expires = ticket.Expiration;
            System.Web.HttpContext.Current.Response.Cookies.Add(cookie);

            var loginInfo = employee.ToLoginInfo();
            loginInfo.SetCache();
        }

        public static LoginInfo ToLoginInfo(this Employee employee)
        {
            LoginInfo result = new LoginInfo()
            {
                ID = employee.ID,
                IsAdmin = employee.IsAdmin.GetValueOrDefault(false),
                UserName = employee.UserName,
                MenuIDs = new List<string>(),
                Permissions = new Dictionary<string, int>(),
            };

            if (!result.IsAdmin)
            {
                //var rids = (employee.RoleIDs + "").Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).Select(s => s.ToInt()).ToList();
                var menus = MvcBase.Unity.Get<IMenuService>().ListCache();
                //var roles = MvcCore.Unity.Get<ISysRoleService>().ListCache().Where(s => rids.Contains(s.ID));

                //#region 绑定角色

                //foreach (var role in roles)
                //{
                //    foreach (var detail in role.Details)
                //    {
                //        if (result.Roles.ContainsKey(detail.MenuID.GetValueOrDefault(0)))
                //        {
                //            //取合集
                //            List<int> newValue = new List<int>();
                //            newValue.AddRange(System.Web.Mvc.RoleHelper.SplitRole(result.Roles[detail.MenuID.GetValueOrDefault(0)]));
                //            newValue.AddRange(System.Web.Mvc.RoleHelper.SplitRole(detail.Roles));
                //            newValue = newValue.Distinct().ToList();
                //            result.Roles[detail.MenuID.GetValueOrDefault(0)] = newValue.Sum(s => s);
                //        }
                //        else result.Roles.Add(detail.MenuID.GetValueOrDefault(0), detail.Roles.GetValueOrDefault(0));
                //    }
                //}

                //#endregion

                #region 权限
                var permissions = MvcBase.Unity.Get<IPermissionService>().List().Where(m => m.UserID==employee.ID).ToList();
                var list_permission=new List<string>();
                foreach (var item in permissions)
                {
                        //取合集
                        list_permission.Add(item.MenuID);
                        list_permission = list_permission.Distinct().ToList();
                        result.Permissions[item.MenuID] = item.Permissions.GetValueOrDefault(0);
                }

                #endregion

                #region 绑定菜单

                foreach (var root in menus)
                {
                    bool hasChecked = false;
                    foreach (var item in root.Children.Where(s => list_permission.Contains(s.ID)))
                    {
                        result.MenuIDs.Add(item.ID);
                        hasChecked = true;
                    }
                    if (hasChecked) result.MenuIDs.Add(root.ID);
                }

                #endregion

            }
            return result;
        }

        private static void SetCache(this LoginInfo loginInfo)
        {
            CacheExtensions.SetCache(LoginCacheName + loginInfo.ID, loginInfo, CacheTimeType.ByMinutes, 10);
        }

        public static void Logout()
        {
            var id = HttpContext.Current.User.Identity.Name.ToGuid();
            CacheExtensions.ClearCache(LoginCacheName + id);
            FormsAuthentication.SignOut();
        }
        public static List<LoginInfo> LoginInfo()
        {
            List<LoginInfo> result = new List<LoginInfo>();
            foreach (var key in CacheExtensions.GetAllCache().Where(s => s.StartsWith(LoginCacheName)))
            {
                result.Add(CacheExtensions.GetCache<LoginInfo>(key));
            }
            return result;
        }

        public static LoginInfo LoginInfo(string ID)
        {
            string key = LoginCacheName + ID;
            //if (!System.Web.HttpContext.Current.Request.Headers.AllKeys.Contains(key))
            //{
            //    if (!CacheExtensions.CheckCache(key))
            //    {
            //        var employee = MvcCore.Unity.Get<IEmployeeService>().Single(ID);
            //        if (employee == null || employee.IsDisabled.GetValueOrDefault(false))
            //        {
            //            Logout();
            //            throw new UnauthorizedAccessException("用户信息失效！");
            //        }
            //        employee.ToLoginInfo().SetCache();
            //    }
            //    System.Web.HttpContext.Current.Request.Headers.Set(key, CacheExtensions.GetCache<LoginInfo>(key).ToJson());
            //}
            //return HttpContext.Current.Request.Headers.Get(key).FromJson<LoginInfo>();

            if (!CacheExtensions.CheckCache(key))
            {
                var employee = MvcBase.Unity.Get<IEmployeeService>().Single(ID);
                if (employee == null || employee.IsDisabled.GetValueOrDefault(false))
                {
                    Logout();
                    throw new UnauthorizedAccessException("用户信息失效！");
                }
                employee.ToLoginInfo().SetCache();
            }
            return CacheExtensions.GetCache<LoginInfo>(key);
        }

        public static LoginInfo Current()
        {
            var id = HttpContext.Current.User.Identity.Name.ToGuid();
            return LoginInfo(id.ToString());
        }
    }
}