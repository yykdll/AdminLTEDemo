using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting;
using System.Web.Mvc;
using MvcBase.Infrastructure;

namespace AdminLTE.Domain
{
    public partial class MainDbContext
    {
        public DbSet<Menu> Menu { get; set; }
    }



    /// <summary>
    /// 后台管理目录路径
    /// </summary>
    [Table("MENU")]
    public class Menu
    {
        public Menu()
        {
            this.ID = Guid.NewGuid().ToString();
            this.CreateTime = DateTime.Now;
        }
        /// <summary>
        /// 主键
        /// </summary>
        [DisplayName("主键")]
        [Key]
        [Column("ID")]
        public String ID { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        [DisplayName("名称")]
        [Column("NAME")]
        [Required(ErrorMessage = "名称不能为空")]
        [MaxLength(50,ErrorMessage = "名称不能超过50个字")]
        public string Name { get; set; }
        /// <summary>
        /// 链接地址
        /// </summary>
        [DisplayName("链接地址")]
        [Column("URL")]
        [MaxLength(100, ErrorMessage = "链接地址不能超过100个字")]
        [Remote("IsURLNullorEmpty", "MenuManage")]
        public string Url { get; set; }
        /// <summary>
        /// 允许的权限
        /// </summary>
        [DisplayName("允许的权限")]
        [Column("ALLOWPERMISSIONS")]
        public int? AllowPermissions { get; set; }
        /// <summary>
        /// 父级ID
        /// </summary>
        [DisplayName("父级ID")]
        [Column("PARENTID")]
        public String ParentID { get; set; }
        /// <summary>
        /// 已启用
        /// </summary>
        [DisplayName("已启用")]
        [Column("ISENABLE")]
        public bool? IsEnable { get; set; }
        ///// <summary>
        ///// 是控制器
        ///// </summary>
        //[DisplayName("是控制器")]
        //public bool? IsController { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [DisplayName("创建时间")]
        [Column("CREATETIME")]
        public DateTime? CreateTime { get; set; }
        /// <summary>
        /// 排序编号
        /// </summary>
        [DisplayName("排序编号")]
        [Column("ORDERID")]
        public int? OrderID { get; set; }

        [ForeignKey("ParentID")]
        public virtual Menu Parent { get; set; }
        public virtual ICollection<Menu> Children { get; set; }


    }


    public class MenuCacheModel
    {
        public String ID { get; set; }
        public string Name { get; set; }
        public int? Permissions { get; set; }
        public string Url { get; set; }
        public List<MenuCacheModel> Children { get; set; }
    }

    namespace Services
    {
        public interface IMenuService : IServiceBase<Menu>
        {
            List<MenuCacheModel> ListCache();
            void ClearCache();
        }
        public class MenuService : ServiceBase<Menu>, IMenuService
        {
            string CacheName = "Menu";
            public MenuService(IMainDbFactory factory) : base(factory) { }
            public List<MenuCacheModel> ListCache()
            {
                if (!MvcBase.Helper.CacheExtensions.CheckCache(CacheName))
                {
                    var all = List(s => s.IsEnable == true).ToList();
                    var roots = all
                        .Where(s => s.ParentID == null || s.ParentID == "0")
                        .OrderBy(s => s.OrderID)
                        .Select(s => new MenuCacheModel()
                        {
                            ID = s.ID,
                            Name = s.Name,
                            //Permissions = s.AllowPermissions,
                            Url=s.Url,
                            Children = new List<MenuCacheModel>()
                        }).ToList();
                    foreach (var root in roots)
                    {
                        root.Children.AddRange(all.Where(s => s.ParentID == root.ID).OrderBy(s => s.OrderID).Select(s => new MenuCacheModel()
                        {
                            ID = s.ID,
                            Name = s.Name,
                            Permissions = s.AllowPermissions,
                            Url = s.Url,
                        }));
                    }
                    MvcBase.Helper.CacheExtensions.SetCache(CacheName, roots, MvcBase.Enum.CacheTimeType.ByHours, 2);
                }
                return MvcBase.Helper.CacheExtensions.GetCache<List<MenuCacheModel>>(CacheName);
            }
            public void ClearCache()
            {
                MvcBase.Helper.CacheExtensions.ClearCache(CacheName);
            }
        }
    }
}
