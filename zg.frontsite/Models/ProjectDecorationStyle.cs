using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace zg.frontsite
{
    public class ProjectDecorationStyle
    {
        /// <summary>
        /// 获取或设置
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// 获取或设置装修风格名称
        /// </summary>
        public string name { get; set; }

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