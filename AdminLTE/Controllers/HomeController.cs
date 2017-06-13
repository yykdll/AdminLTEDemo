﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AdminLTE.Domain;
using AdminLTE.Domain.Services;
using AdminLTE.Enum;
using AdminLTE.Filter;

namespace AdminLTE.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IMainDBTool _dbTool;
        private IEmployeeService _employeeService;

        public HomeController(IEmployeeService employeeService, IMainDBTool dbTool)
        {
            this._employeeService = employeeService;
            this._dbTool = dbTool;
        }
        public ActionResult Index()
        {
            return View();
        }
    }
}
