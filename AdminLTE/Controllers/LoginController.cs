using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AdminLTE.Domain.Services;
using AdminLTE.Helper;
using MvcBase.Controls;

namespace AdminLTE.Controllers
{
    public class LoginController : Controller
    {
        
        private readonly IMainDBTool _dbTool;
        private IEmployeeService _employeeService;

        public LoginController(IEmployeeService employeeService, IMainDBTool dbTool)
        {
            this._employeeService = employeeService;
            this._dbTool = dbTool;
        }
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                return Redirect("/");
            }
            return View();
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="LoginName"></param>
        /// <param name="LoginPassword"></param>
        /// <param name="ValidateCode"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Login(string LoginName, string LoginPassword, string ValidateCode)
        {
            ReturnResult result = new ReturnResult();
            try
            {
                string code = Session["ValidateCode"] + "";
                Session.Remove("ValidateCode");
                if (string.IsNullOrEmpty(ValidateCode) ||
                    !code.Equals(ValidateCode, StringComparison.CurrentCultureIgnoreCase))
                {
                    result.Status = 500;
                    result.Message = "验证码错误！";
                    return Json(result);
                }
                if (string.IsNullOrEmpty(LoginName))
                {
                    result.Status = 500;
                    result.Message = "用户名不能为空！";
                    return Json(result);
                }
                if (string.IsNullOrEmpty(LoginPassword))
                {
                    result.Status = 500;
                    result.Message = "密码不能为空！";
                    return Json(result);
                }
                //Password = Password.EncodePassword();
                var model = _employeeService.Single(s => s.LoginName == LoginName && s.LoginPassword == LoginPassword);
                if (model == null)
                {
                    result.Status = 500;
                    result.Message = "用户名或密码错误！";
                    return Json(result);
                }
                if (model.IsDisabled.GetValueOrDefault(false))
                {
                    result.Status = 500;
                    result.Message = "用户已被禁用！请联系管理员！";
                    return Json(result);
                } 
                model.LastLoginTime = DateTime.Now;
                _employeeService.Save(model, model.ID);
                _dbTool.Commit();
                model.Login();
                result.Status = 200;
                result.Message = "登录成功";
            }
            catch (Exception ex)
            {
                result.Status = 500;
                result.Message = ex.ToString();
            }
            return Json(result);
        }

        public ActionResult Logout()
        {
            LoginInfoHelper.Logout();
            return Redirect("/login/index");
        }
    }
}