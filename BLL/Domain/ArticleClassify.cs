using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using AdminLTE;
using MvcBase.Infrastructure;

namespace AdminLTE.Domain
{
    public partial class MainDbContext
    {
        public DbSet<ArticleClassify> ArticleClassify { get; set; }
    }
    /// <summary>
    /// 文章分类
    /// </summary>
    [Table("ARTICLECLASSIFY")]
    public class ArticleClassify
    {
        public ArticleClassify()
        {
            this.CreateTime=DateTime.Now;
            this.ID = Guid.NewGuid().ToString();
        }
        /// <summary>
        /// 分类ID
        /// </summary>
        [Key]
        [DisplayName("分类ID")]
        [Column("ID")]
        public String ID { get; set; }
        /// <summary>
        /// 分类名
        /// </summary>
        [DisplayName("分类名ID")]
        [Column("CLASSIFYNAME")]
        public String ClassifyName { get; set; }
        /// <summary>
        /// 父分类ID
        /// </summary>
        [DisplayName("父分类ID")]
        [Column("PARENTCLASSIFYID")]
        public String ParentClassifyID { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [DisplayName("创建时间")]
        [Column("CREATETIME")]
        public DateTime? CreateTime { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        [DisplayName("是否删除")]
        [Column("ISDELETE")]
        public Boolean? IsDelete { get; set; }
    }
    namespace Services
    {
        public interface IArticleClassifyService : IServiceBase<ArticleClassify>
        {
        }
        public class ArticleClassifyService : ServiceBase<ArticleClassify>, IArticleClassifyService
        {
            public ArticleClassifyService(IMainDbFactory factory) : base(factory) { }
        }
    }
}
