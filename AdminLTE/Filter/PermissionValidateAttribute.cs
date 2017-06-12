using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcBase.Controls;

namespace AdminLTE.Filter
{
    public class PermissionValidateAttribute : ActionFilterAttribute, IActionFilter
    {
        public Enum.PermissionType[] Permission { get; set; }
        public PermissionValidateAttribute(params Enum.PermissionType[] Role) { this.Permission = Permission; }

        public override void OnActionExecuting(System.Web.Mvc.ActionExecutingContext filterContext)
        {
            bool result = false;

            if (this.Permission == null || this.Permission.Length == 0) result = true;
            else
            {
                foreach (var r in Permission)
                {
                    result = filterContext.RequestContext.RouteData.CheckRole(r);
                    if (result) break;
                }
            }

            #region 处理判断结果

            if (result)
                base.OnActionExecuting(filterContext);
            else
            {
                if (filterContext.HttpContext.Request.IsAjaxRequest())
                {
                    filterContext.Result = new JsonResult() { Data = new ReturnResult() { Status = 403, Message = "权限不足！" } };
                }
                else
                {
                    if (filterContext.HttpContext.Request.UrlReferrer != null)
                        filterContext.Result = new RedirectResult(filterContext.HttpContext.Request.UrlReferrer.ToString());
                    else
                        filterContext.Result = new RedirectResult("/");
                }
            }

            #endregion
        }
    }
}