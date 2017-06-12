using System.Web;
using System.Web.Mvc;
using AdminLTE.Helper;
using MvcBase.Controls;

namespace AdminLTE.Filter
{
    public class AuthorityLoginAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            //不是Get方法过来的请求，都不能跨站操作。防止管理员登录本站后去访问一些带有攻击本站代码的网站
            if (httpContext.Request.HttpMethod.ToUpper() != "GET")
            {
                if (httpContext.Request.Url.Authority != httpContext.Request.UrlReferrer.Authority)
                {
                    //throw new Exception("不允许跨站操作");
                    return false;
                }
            }


            if (LoginInfoHelper.Current()!=null)
            {
                return true;
            }
            return false;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            //如果异步请求，返回json数据
            if (filterContext.RequestContext.HttpContext.Request.IsAjaxRequest())
            {
                ReturnResult result = new ReturnResult();
                result.Status = 403;
                result.Message = "你还没有登录或登录超时,请重新登录！";
                JsonResult jr = new JsonResult();
                jr.ContentType = "text/json";

                jr.Data = result;
                filterContext.Result = jr;
            }
            else
            {
                filterContext.Controller.ViewBag.Message = "你还没有登录或登录超时,请重新登录！";
                filterContext.Result = new RedirectResult("/login/index?ReturnUrl=" + filterContext.RequestContext.HttpContext.Request.Url);
            }


            //base.HandleUnauthorizedRequest(filterContext);
        }
    }
}