using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AdminLTE.Controllers
{
    public class PublicController : Controller
    {
        public FileResult ValidateCode(int? w, int? h)
        {
            var code = ValidateWhiteBlackImgCode.RandemCode();
            Session["ValidateCode"] = code;
            return File(ValidateWhiteBlackImgCode.Img(code, w ?? 200, h ?? 75), "image/png");
        }
    }
}