using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace vgo.frontsite004.Models
{
    public class News
    {
        /// <summary>
        /// 获取或设置
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// 获取或设置类型id
        /// </summary>
        public int type_id { get; set; }

        /// <summary>
        /// 类型文本
        /// </summary>
        public string type_text { get; set; }

        /// <summary>
        /// 获取或设置标题
        /// </summary>
        public string title { get; set; }

        /// <summary>
        /// 获取或设置作者
        /// </summary>
        public string author { get; set; }

        /// <summary>
        /// 获取或设置封面图片
        /// </summary>
        public string cover_url { get; set; }

        public string full_cover_url { get; set; }

        /// <summary>
        /// 获取或设置内容摘要
        /// </summary>
        public string summary { get; set; }

        /// <summary>
        /// 获取或设置内容主体
        /// </summary>
        public string content { get; set; }

        /// <summary>
        /// 获取或设置排序值
        /// </summary>
        public int sort { get; set; }

        /// <summary>
        /// 获取或设置查看数
        /// </summary>
        public int seecount { get; set; }

        /// <summary>
        /// 获取或设置点赞数
        /// </summary>
        public int praisecount { get; set; }

        /// <summary>
        /// 获取或设置创建时间
        /// </summary>
        public DateTime created { get; set; }
    }
}