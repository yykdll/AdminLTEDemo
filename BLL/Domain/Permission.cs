using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using MvcBase.Infrastructure;

namespace AdminLTE.Domain
{
    public partial class MainDbContext
    {
        public DbSet<Permission> Permission { get; set; }
    }
    [Table("PERMISSION")]
    public class Permission
    {
        public Permission()
        {
            this.ID = Guid.NewGuid().ToString();
        }
        /// <summary>
        /// 主键
        /// </summary>
        [DisplayName("主键")]
        [Key]
        [Column("ID")]
        public String ID { get; set; }
        /// <summary>
        /// 用户ID
        /// </summary>
        [DisplayName("用户ID")]
        [Column("USERID")]
        public String UserID { get; set; }
        /// <summary>
        /// 目录ID
        /// </summary>
        [DisplayName("目录ID")]
        [Column("MENUID")]
        public String MenuID { get; set; }
        /// <summary>
        /// 权限值
        /// </summary>
        [DisplayName("权限值")]
        [Column("PERMISSIONS")]
        public int? Permissions { get; set; }

        [ForeignKey("UserID")]
        public virtual Employee Employee { get; set; }

        [ForeignKey("MenuID")]
        public virtual Menu Menu { get; set; }
    }

    namespace Services
    {
        public interface IPermissionService : IServiceBase<Permission>
        {
        }

        public class PermissionService : ServiceBase<Permission>, IPermissionService
        {
            public PermissionService(IMainDbFactory factory)
                : base(factory)
            {
            }
        }
    }
}
