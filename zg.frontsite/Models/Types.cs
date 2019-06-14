using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace zg.frontsite
{
    public class Types
    {
        /// <summary>
        /// 获取或设置
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// 获取或设置分类名称
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 获取或设置英文名称
        /// </summary>
        public string english_name { get; set; }

        /// <summary>
        /// 获取或设置分类小图标
        /// </summary>
        public string icon_url { get; set; }

        /// <summary>
        /// 获取或设置排序
        /// </summary>
        public int sort { get; set; }

        /// <summary>
        /// 获取或设置创建时间
        /// </summary>
        public DateTime created { get; set; }
    }
}