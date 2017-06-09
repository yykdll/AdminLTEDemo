// Decompiled with JetBrains decompiler
// Type: MvcCore.IoC.UnityControllerFactory
// Assembly: MvcCore, Version=1.0.0.30, Culture=neutral, PublicKeyToken=null
// MVID: 2E513CDA-5999-4C86-8B0D-323ED256A8F6
// Assembly location: F:\反混淆\MvcCore-cleaned.dll

using Microsoft.Practices.Unity;
using System;
using System.ComponentModel;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MvcBase.IoC
{
    public class UnityControllerFactory : DefaultControllerFactory
    {
        private IUnityContainer iunityContainer_0;


        public UnityControllerFactory(IUnityContainer container)
        {
            this.iunityContainer_0 = container;
        }

        protected override IController GetControllerInstance(RequestContext reqContext, Type controllerType)
        {
            if (controllerType == (Type)null)
                throw new HttpException(404, string.Format("当前控制器 '{0}' 不存在或没有导入控制器", (object)reqContext.HttpContext.Request.Path));
            try
            {
                return this.iunityContainer_0.Resolve(controllerType, new ResolverOverride[0]) as IController;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(string.Format("解析 {0} 控制器失败", (object)controllerType.Name), ex);
            }
        }
    }
}
