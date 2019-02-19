using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LU.Model.Share
{
    public class PostResult
    {
        /// <summary>
        /// 错误消息
        /// </summary>
        public string ErrMsg { get; set; }

        /// <summary>
        /// 执行是否成功
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// 跳转地址
        /// </summary>
        public string ToUrl { get; set; }
        /// <summary>
        /// 返回数据集
        /// </summary>
        public DataColl Data { get; set; }
    }

    public class DataColl
    {
        public object List { get; set; }

        /// <summary>
        /// 分页信息
        /// </summary>
        public PageInfo PageInfo { get; set; }


        public bool IsAdmin { get; set; }

        /// <summary>
        /// 是否加密狗登入
        /// </summary>
        public bool IsHaveJMG { get; set; }

        /// <summary>
        /// 10分钟没有操作
        /// </summary>
        public int NotOper { get; set; }

        public string Token { get; set; }

        public string HttpToken { get; set; }
    }

    /// <summary>
    /// 分页信息
    /// </summary>
    public class PageInfo
    {
        /// <summary>
        /// 当前页码
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// 页尺寸
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 总页数
        /// </summary>
        public int PageTotal { get; set; }

        /// <summary>
        /// 记录总数
        /// </summary>
        public int RecordTotal { get; set; }

    }
}
