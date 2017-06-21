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
        public DbSet<Article> Article { get; set; }
    }

    /// <summary>
    /// 文章
    /// </summary>
    [Table("ARTICLE")]
    public class Article
    {
        /// <summary>
        /// 文章ID
        /// </summary>
        [Key]
        [DisplayName("文章ID")]
        [Column("ID")]
        public String ID { get; set; }
        /// <summary>
        /// 文章标题
        /// </summary>
        [DisplayName("文章标题")]
        [Column("TITLE")]
        public String Title { get; set; }
        /// <summary>
        /// 文章描述
        /// </summary>
        [DisplayName("文章描述")]
        [Column("DESCRIPTION")]
        public String Description { get; set; }
        /// <summary>
        /// 文章内容
        /// </summary>
        [DisplayName("文章内容")]
        [Column("CONTENT")]
        public String Content { get; set; }
        /// <summary>
        /// 关键字
        /// </summary>
        [DisplayName("关键字")]
        [Column("KEYWORDS")]
        public String Keywords { get; set; }
        /// <summary>
        /// 创建人ID
        /// </summary>
        [DisplayName("创建人ID")]
        [Column("CREATORID")]
        public String CreatorID { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [DisplayName("创建时间")]
        [Column("CREATETIME")]
        public DateTime? CreateTime { get; set; }
        /// <summary>
        /// 创建人姓名
        /// </summary>
        [DisplayName("创建人姓名")]
        [Column("CREATORNAME")]
        public String CreatorName { get; set; }
        /// <summary>
        /// 阅读量
        /// </summary>
        [DisplayName("阅读量")]
        [Column("VIEWCOUNT")]
        public Int32? ViewCount { get; set; }
        /// <summary>
        /// 分类ID
        /// </summary>
        [DisplayName("分类ID")]
        [Column("CLASSIFYID")]
        public String ClassifyID { get; set; }
        /// <summary>
        /// 父分类ID
        /// </summary>
        [DisplayName("父分类ID")]
        [Column("PARENTLASSIFYID")]
        public String ParentlassifyID { get; set; }
        /// <summary>
        /// 是否发布
        /// </summary>
        [DisplayName("是否发布")]
        [Column("ISPUBLISH")]
        public Boolean? IsPublish { get; set; }
        /// <summary>
        /// 发布人ID
        /// </summary>
        [DisplayName("发布人ID")]
        [Column("PUBLISHERID")]
        public String PublisherID { get; set; }
        /// <summary>
        /// 发布时间
        /// </summary>
        [DisplayName("发布时间")]
        [Column("PUBLISHTIME")]
        public DateTime? PublishTime { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        [DisplayName("是否删除")]
        [Column("ISDELETE")]
        public Boolean? IsDelete { get; set; }
        /// <summary>
        /// 删除时间
        /// </summary>
        [DisplayName("删除时间")]
        [Column("DELETETIME")]
        public DateTime? DeleteTime { get; set; }
        /// <summary>
        /// 删除者
        /// </summary>
        [DisplayName("删除者")]
        [Column("DELETORID")]
        public String DeletorID { get; set; }

        /// <summary>
        /// 最后编辑时间
        /// </summary>
        [DisplayName("最后编辑时间")]
        [Column("LASTEDITTIME")]
        public DateTime? LastEditTime { get; set; }
}

    namespace services
    {
        public interface IArticleService : IServiceBase<Article>{}

        public class ArticleService : ServiceBase<Article>, IArticleService
        {
            public ArticleService(IMainDbFactory factory) : base(factory) { }
        }
    }
}
