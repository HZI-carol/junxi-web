using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace vgo.frontsite004.Models
{
    public class MarketPanoType
    {
        /// <summary>
        /// 获取或设置
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// 获取或设置作品分类名称
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 获取或设置排序
        /// </summary>
        public int sort { get; set; }

        /// <summary>
        /// 获取或设置创建作品分类时间
        /// </summary>
        public DateTime created { get; set; }
    }
}