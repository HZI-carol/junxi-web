using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace zg.frontsite
{
    public class Banners
    {
        /// <summary>
        /// 获取或设置
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// 获取或设置标题
        /// </summary>
        public string title { get; set; }

        /// <summary>
        /// 获取或设置图片地址
        /// </summary>
        public string imgurl { get; set; }

        /// <summary>
        /// 获取或设置图片地址-前面加域名
        /// </summary>
        public string full_imgurl { get; set; }

        /// <summary>
        /// 获取或设置链接地址
        /// </summary>
        public string linkurl { get; set; }

        /// <summary>
        /// 获取或设置排序
        /// </summary>
        public int sort { get; set; }

        /// <summary>
        /// 获取或设置类型id 0 首页Banner 1 前端广告位
        /// </summary>
        public int type_id { get; set; }

        /// <summary>
        /// 获取或设置创建时间
        /// </summary>
        public DateTime created { get; set; }
    }
}