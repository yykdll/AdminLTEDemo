using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AdminLTE.Domain;
using AdminLTE.Domain.Services;
using AdminLTE.Helper;
using MvcBase.Controls;

namespace AdminLTE.Controllers
{
    public class UserManageController : Controller
    {
        private readonly IMainDBTool _dbTool;
        private readonly IEmployeeService _employeeService;
        private readonly IPermissionService _permissionService;
        private readonly IMenuService _menuService;

        public UserManageController(IEmployeeService employeeService, IMainDBTool dbTool, IPermissionService permissionService, IMenuService menuService)
        {
            this._employeeService = employeeService;
            this._dbTool = dbTool;
            this._permissionService = permissionService;
            this._menuService = menuService;
        }

        /// <summary>
        /// 列表
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            var list_employee = _employeeService.ListAllEmployees();
            return View(list_employee);
        }

        /// <summary>
        /// 添加或者编辑用户
        /// </summary>
        /// <returns></returns>
        public ActionResult Edit(string id)
        {
            var o_employee = _employeeService.SingleAndInit(id);
            return View(o_employee);
        }

        /// <summary>
        /// 添加或者编辑用户
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Save(string id)
        {
            ReturnResult result = new ReturnResult();
            try
            {
                var o_employee = _employeeService.SingleAndInit(id);
                TryUpdateModel(o_employee, null, Request.Form.AllKeys);
                _employeeService.Save(o_employee,id);
                _dbTool.Commit();
                result.Status = 200;
                result.Message = "保存成功!";
            }
            catch (Exception ex)
            {
                result.Message = ex.ToString();
            }
            return Json(result);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Delete(string ID)
        {
            ReturnResult result = new ReturnResult();
            try
            {
                if (!string.IsNullOrEmpty(ID) && ID != "0" && ID != "undifined")
                {
                    _employeeService.Delete(s => s.ID == ID);
                    _permissionService.Delete(m=>m.UserID==ID);
                    _dbTool.Commit();
                    result.Status = 200;
                    result.Message = "删除成功";
                }
                else
                {
                    result.Status = 500;
                    result.Message = "用户不存在，删除失败!";
                }
            }
            catch (Exception ex)
            {
                result.Message = ex.ToString();
            }
            return Json(result);
        }

        /// <summary>
        /// 禁用
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Disable(string ID)
        {
            ReturnResult result = new ReturnResult();
            try
            {
                if (!string.IsNullOrEmpty(ID) && ID != "0" && ID != "undifined")
                {
                    var o_employee=_employeeService.Single(s => s.ID == ID);
                    if (o_employee == null)
                    {
                        result.Status = 500;
                        result.Message = "用户不存在！";
                    }
                    o_employee.IsDisabled = true;
                    _employeeService.Update(o_employee);
                    _dbTool.Commit();
                    result.Status = 200;
                    result.Message = "禁用成功";
                }
                else
                {
                    result.Status = 500;
                    result.Message = "用户不存在，禁用失败!";
                }
            }
            catch (Exception ex)
            {
                result.Message = ex.ToString();
            }
            return Json(result);
        }

        /// <summary>
        /// 禁用
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Enable(string ID)
        {
            ReturnResult result = new ReturnResult();
            try
            {
                if (!string.IsNullOrEmpty(ID) && ID != "0" && ID != "undifined")
                {
                    var o_employee = _employeeService.Single(s => s.ID == ID);
                    if (o_employee == null)
                    {
                        result.Status = 500;
                        result.Message = "用户不存在！";
                    }
                    o_employee.IsDisabled = false;
                    _employeeService.Update(o_employee);
                    _dbTool.Commit();
                    result.Status = 200;
                    result.Message = "启用成功";
                }
                else
                {
                    result.Status = 500;
                    result.Message = "用户不存在，启用失败!";
                }
            }
            catch (Exception ex)
            {
                result.Message = ex.ToString();
            }
            return Json(result);
        }

        /// <summary>
        /// 编辑权限
        /// </summary>
        /// <returns></returns>
        public ActionResult EditPermission(string userID)
        {
            var o_employee = _employeeService.Single(userID);
            if (o_employee==null) return null;
            ViewBag.Menus = CurrentMenu();
            ViewBag.Permissions = _permissionService.List(s => s.UserID == userID).ToList();
            ViewBag.PermissionPoints = typeof(AdminLTE.Enum.PermissionType).GetEnumListItem();
            return View(o_employee);
        }

        private List<MenuCacheModel> CurrentMenu()
        {
            var loginInfo = LoginInfoHelper.Current();
            List<MenuCacheModel> result = new List<MenuCacheModel>();
            var menus = _menuService.ListCache();
            if (loginInfo.IsAdmin) 
                return menus;
            foreach (var rootMenu in menus)
            {
                if (loginInfo.MenuIDs.Contains(rootMenu.ID))
                {
                    MenuCacheModel root = new MenuCacheModel()
                    {
                        Name = rootMenu.Name,
                        Url=rootMenu.Url,
                        ID = rootMenu.ID,
                        Children = new List<MenuCacheModel>()
                    };
                    foreach (var menu in rootMenu.Children)
                    {
                        if (loginInfo.MenuIDs.Contains(menu.ID))
                        {
                            MenuCacheModel child = new MenuCacheModel()
                            {
                                Name = menu.Name,
                                ID = menu.ID,
                                Url=menu.Url,
                                Permissions = loginInfo.Permissions[menu.ID]
                            };
                            root.Children.Add(child);
                        }
                    }
                    result.Add(root);
                }
            }
            return result;
        }

        /// <summary>
        /// 保存权限
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult SavePermission(string ID, string PermissionJson)
        {

            ReturnResult result = new ReturnResult();
            try
            {
                if (string.IsNullOrEmpty(PermissionJson)) throw new NullReferenceException("请勾选权限");
                var o_employee = _employeeService.SingleAndInit(ID);
                //TryUpdateModel(o_employee, null, Request.Form.AllKeys);
                List<KeyValuePair<string, int?>> permissions_ = PermissionJson.FromJson<List<KeyValuePair<string, int?>>>();
                Dictionary<string, int?> permissions = new Dictionary<string, int?>();
                permissions_.ForEach((item) => { permissions.Add(item.Key, item.Value); });

                //if (entity.Details == null) entity.Details = new List<SysRoleDetail>();
                var removeids = o_employee.Permissions.Where(s => !permissions.ContainsKey(s.MenuID)).Select(s => s.ID).ToList();
                foreach (var kv in permissions)
                {
                    if (!o_employee.Permissions.Any(s => s.MenuID == kv.Key))
                    {
                        Permission Permission = new Permission()
                        {
                            MenuID = kv.Key,
                            Permissions = kv.Value,
                            UserID=o_employee.ID
                        };
                        _permissionService.Add(Permission);
                        _dbTool.Commit();
                    }
                    else if (!o_employee.Permissions.Any(s => s.MenuID == kv.Key && s.Permissions == kv.Value))
                    {
                        var o_permission = o_employee.Permissions.FirstOrDefault(s => s.MenuID == kv.Key);
                        o_permission.Permissions = kv.Value;
                        _permissionService.Update(o_permission);
                        _dbTool.Commit();
                    }
                }
                _permissionService.Delete(s => removeids.Contains(s.ID));
                //_employeeService.Save(entity, entity.ID);
                _dbTool.Commit();
                //roleService.ClearCache();
                result.Status = 200;
                result.Message = "保存成功!";
            }
            catch (Exception ex)
            {
                result.Message = ex.ToString();
            }
            return Json(result);
        }

    }
}