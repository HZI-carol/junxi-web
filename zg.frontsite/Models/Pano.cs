using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace zg.frontsite
{
    public class Pano
    {
        /// <summary>
        ///作品表
        /// </summary>
        public int pano_id { get; set; }

        /// <summary>
        ///用户表id
        /// </summary>
        public int user_id { get; set; }

        /// <summary>
        /// 用户头像
        /// </summary>
        public string user_avatar { get; set; }

        /// <summary>
        /// 用户昵称
        /// </summary>
        public string user_nickname { get; set; }

        /// <summary>
        ///作品名称
        /// </summary>
        public string pano_name { get; set; }

        /// <summary>
        ///作品图片
        /// </summary>
        public string icon_src { get; set; }

        /// <summary>
        ///浏览次数
        /// </summary>
        public int seecount { get; set; }

        /// <summary>
        ///点赞数
        /// </summary>
        public int praisecount { get; set; }

        /// <summary>
        /// 标签
        /// </summary>
        public string tag_id { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime addtime { get; set; }

        /// <summary>
        /// 是否精选 
        /// </summary>
        public int ispicking { get; set; }

        /// <summary>
        ///  是否推荐
        /// </summary>
        public int isrecommend { get; set; }

        /// <summary>
        /// 作品预览地址
        /// </summary>
        public string panoview_url { get; set; }

        /// <summary>
        /// 作品排序值
        /// </summary>
        public int sort { get; set; }
    }
}