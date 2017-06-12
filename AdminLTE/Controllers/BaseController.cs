using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AdminLTE.Filter;

namespace AdminLTE.Controllers
{
    [AuthorityLogin]
    public class BaseController : Controller
    {
    }
}