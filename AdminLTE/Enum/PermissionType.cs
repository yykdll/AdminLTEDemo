using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace AdminLTE.Enum
{
    public enum PermissionType
    {

        /// <summary>
        /// 查看
        /// </summary>
        [Description("查看")]
        Index = 2,
        /// <summary>
        /// 新建
        /// </summary>
        [Description("新建")]
        Create = 4,
        /// <summary>
        /// 编辑
        /// </summary>
        [Description("编辑")]
        Edit = 8,
        /// <summary>
        /// 删除
        /// </summary>
        [Description("删除")]
        Delete = 16,
    }
}