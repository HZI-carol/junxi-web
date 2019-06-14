using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace vgo.partner.frontsite.Models
{
    public class NewsType
    {
        /// <summary>
        /// 获取或设置
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// 获取或设置类型名称
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 获取或设置排序值
        /// </summary>
        public int sort { get; set; }
    }
}