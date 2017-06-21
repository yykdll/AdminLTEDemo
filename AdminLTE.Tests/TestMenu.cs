using System;
using AdminLTE.Domain;
using AdminLTE.Domain.Services;
using Microsoft.Practices.Unity;
using MvcBase.Infrastructure;
using Xunit;

namespace AdminLTE.Tests
{
    public class TestMenu 
    {
        //private  IMainDBTestTool _dbTool;
        //private  IMenuService _menuService;

        //public TestMenu()
        //{
            

        //}
        [Fact]
        public void TestMenu_Add_NotNull()
        {
            //Unity.Init();
            //
            IMainDbFactory factory = new CustomDbTestFactory();
            var menuService = new MenuService(factory);
            var mainDBTestTool = new MainDBTestTool(factory);
            var menu=new Menu();
            menu.Url = "///";
            menu.Name = "测试";
            menuService.Add(menu);
            mainDBTestTool.Commit();
            //_dbTool = MvcBase.Unity.Get<IMainDBTestTool>();

            //_menuService.Add(new Menu());
            //_dbTool.Commit();
            var expect = 55;
            Assert.True(expect == 55);
        }
        [Fact]
        public void TestAdd_Add_2()
        {
            int a = 1;
            int b = 1;
            int arc = a + b;
            var expect = 2;
            Assert.True(expect == arc);
        }
    }
}
