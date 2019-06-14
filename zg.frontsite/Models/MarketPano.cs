using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace zg.frontsite
{
    public class MarketPano
    {
        /// <summary>
        /// 获取或设置
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// 获取或设置作品分类id
        /// </summary>
        public int fk_pano_id { get; set; }

        /// <summary>
        /// 获取或设置作品所属用户id
        /// </summary>
        public int fk_user_id { get; set; }

        /// <summary>
        /// 获取或设置作品名称
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 获取或设置作品分类id
        /// </summary>
        public int pano_type { get; set; }

        public string pano_type_text { get; set; }

        /// <summary>
        /// 获取或设置原图价格
        /// </summary>
        public int price { get; set; }

        /// <summary>
        /// 获取或设置拍摄设备设备id
        /// </summary>
        public int shoot_type { get; set; }

        public string shoot_type_text { get; set; }

        /// <summary>
        /// 获取或设置省
        /// </summary>
        public string pro { get; set; }

        /// <summary>
        /// 获取或设置市
        /// </summary>
        public string city { get; set; }

        /// <summary>
        /// 获取或设置区
        /// </summary>
        public string area { get; set; }

        /// <summary>
        /// 获取或设置作品图片地址
        /// </summary>
        public string icon_src { get; set; }

        public string full_icon_src { get; set; }

        public string nickname { get; set; }

        public bool has_buy { get; set; }

        public string panoview_url { get; set; }
    }
}