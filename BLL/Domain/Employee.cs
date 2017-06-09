using MvcBase.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace AdminLTE.Domain
{
    public partial class MainDbContext
    {
        public DbSet<Employee> Employee { get; set; }
    }
    /// <summary>
    /// 系统管理员
    /// </summary>
    [Table("EMPLOYEE")]
    public class Employee
    {
        public Employee()
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
        /// 用户名（后台显示）
        /// </summary>
        [DisplayName("姓名")]
        [Column("USERNAME")]
        [Required(ErrorMessage = "请输入姓名")]
        public string UserName { get; set; }
        /// <summary>
        /// 登录名
        /// </summary>
        [DisplayName("登录名")]
        [Column("LOGINNAME")]
        public string LoginName { get; set; }
        /// <summary>
        /// 登录密码
        /// </summary>
        [DisplayName("登录密码")]
        [Column("LOGINPASSWORD")]
        public string LoginPassword { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [DisplayName("创建时间")]
        [Column("CREATETIME")]
        public DateTime? CreateTime { get; set; }
        /// <summary>
        /// 上次登录时间
        /// </summary>
        [DisplayName("上次登录时间")]
        [Column("LASTLOGINTIME")]
        public DateTime? LastLoginTime { get; set; }
        /// <summary>
        /// 已禁用
        /// </summary>
        [DisplayName("已禁用")]
        [Column("ISDISABLED")]
        public bool? IsDisabled { get; set; }
        /// <summary>
        /// 是超级管理员
        /// </summary>
        [DisplayName("是超级管理员")]
        [Column("ISADMIN")]
        public bool? IsAdmin { get; set; }
        ///// <summary>
        ///// 角色ID列表
        ///// <para>多个角色之间用英文逗号相隔</para>
        ///// </summary>
        //[DisplayName("角色")]
        //[Column("ROLEIDS")]
        //public string RoleIDs { get; set; }
        /// <summary>
        /// 头像
        /// </summary>
        [DisplayName("头像")]
        [Column("HEADURL")]
        public string HeadUrl { get; set; }
        //权限
        public virtual ICollection<Permission> Permissions { get; set; }
    }

    namespace Services
    {
        public interface IEmployeeService : IServiceBase<Employee>
        {
            List<Employee> ListAllEmployees();
        }
        public class EmployeeService : ServiceBase<Employee>, IEmployeeService
        {
            public EmployeeService(IMainDbFactory factory) : base(factory) { }

            public List<Employee> ListAllEmployees()
            {
                return List().ToList();
            }
        }
    }
}
