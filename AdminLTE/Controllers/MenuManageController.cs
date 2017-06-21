using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AdminLTE.Domain.Services;
using AdminLTE.Enum;
using AdminLTE.Filter;
using MvcBase.Controls;

namespace AdminLTE.Controllers
{
    public class MenuManageController : BaseController
    {
        private readonly IMainDBTool _dbTool;
        private readonly IMenuService _menuService;
        private readonly IPermissionService _permissionService;

        public MenuManageController(IMenuService menuService, IMainDBTool dbTool,IPermissionService permissionService)
        {
            this._menuService = menuService;
            this._dbTool = dbTool;
            this._permissionService = permissionService;
        }
        [PermissionValidate(PermissionType.Index)]
        public ActionResult Index()
        {
            var list = _menuService
                .List()
                .OrderBy(s => s.OrderID)
                .ToList();
            return View(list);
        }
        /// <summary>
        /// 添加或者编辑菜单
        /// </summary>
        /// <returns></returns>
        [PermissionValidate(PermissionType.Create,PermissionType.Edit)]
        public ActionResult Edit(string ID, string parentID)
        {
            var o_menu = _menuService.SingleAndInit(ID);
            if (!string.IsNullOrEmpty(parentID) && parentID != "0" && parentID != "undifined") o_menu.ParentID = parentID;
            ViewBag.Permissions = typeof(PermissionType).GetEnumListItem();
            if (string.IsNullOrEmpty(ID))
            {
                o_menu.IsEnable = true;
            }
            return View(o_menu);
        }
        /// <summary>
        /// 保存菜单
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [PermissionValidate(PermissionType.Edit,PermissionType.Create)]
        public ActionResult Save(string id)
        {
                        ReturnResult result = new ReturnResult();
            if(ModelState.IsValid)
            {
            try
            {
                var o_menu = _menuService.SingleAndInit(id);
                TryUpdateModel(o_menu, null, Request.Form.AllKeys);
                if (Request.Form.GetValues("AllowPermissions") != null)
                {
                    o_menu.AllowPermissions = Request.Form.GetValues("AllowPermissions").Select(s => s.ToInt()).Sum();
                }
                _menuService.Save(o_menu, id);
                _dbTool.Commit();
                _menuService.ClearCache();
                result.Status = 200;
                result.Message = "保存成功!";
            }
            catch (Exception ex)
            {
                result.Message = ex.ToString();
            }
                }
            else
            {
                result.Status = 500;
                result.Message = "保存失败!";
            }
            return Json(result);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        [HttpPost]
        [PermissionValidate(PermissionType.Delete)]
        public JsonResult Delete(string ID)
        {
            ReturnResult result = new ReturnResult();
            try
            {
                if (!string.IsNullOrEmpty(ID) && ID != "0" && ID!="undifined")
                { 
                _menuService.Delete(s => s.ParentID == ID);
                _permissionService.Delete(s => s.MenuID == ID || s.Menu.Parent.ID == ID);
                _menuService.Delete(ID);
                _dbTool.Commit();
                _menuService.ClearCache();
                result.Status = 200;
                result.Message = "删除成功";
                }
                else
                {
                    result.Status = 500;
                    result.Message = "菜单不存在，删除失败!";
                }
            }
            catch (Exception ex)
            {
                result.Message = ex.ToString();
            }
            return Json(result);
        }

        public JsonResult IsURLNullorEmpty(string URL,string ParentID)
        {
            if (ParentID == null || ParentID == "0" || !string.IsNullOrEmpty(URL))
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("链接地址不能为空！", JsonRequestBehavior.AllowGet);
            }
        }
    }
}