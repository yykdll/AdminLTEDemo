using System.Web.Mvc;
using AdminLTE;
using AdminLTE.Domain.Services;
using MvcBase;
using Microsoft.Practices.Unity;

namespace TestUnity.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            Init();
            var _menuService = MvcBase.Unity.Get<IMenuService>();
            return View();
        }
        public static void Init()
        {
            MvcBase.UnityHelper.Init(RegisterType);
        }
        /// <summary>
        /// 注册 IoC 映射
        /// </summary>
        /// <param name="container"></param>
        private static void RegisterType(IUnityContainer container)
        {
            /*
             * 
             * 这里注册的接口与实现类，要取出来有两种方式
             * 1.继承有 Controller 或者 ApiController 的控制器、接口，在构造函数上加上要取出的接口类型的参数，构造函数的参数类型、数量、顺序不限制。
             * 2.通过 MvcCore.Unity.Get<T>() 取出对应的接口，这里的泛型 T 既为需要取出的接口类型。
             * 
             * 注意:以上两种方式取出的接口必须是下面绑定的接口！
             * 
             */

            container

            #region 基础绑定

                //.Bind<IMainDbTestFactory, CustomDbTestFactory>()

//            #endregion

//            #region 数据层绑定

//.LoadAssemblyAndBind("AdminLTE.Tests", "AdminLTE.Tests.Services")
.Bind<IMainDbFactory, CustomDbFactory>()

            #endregion

            #region 数据层绑定

.LoadAssemblyAndBind("BLL", "AdminLTE.Domain.Services")

            #endregion


;

        }
    }
}
