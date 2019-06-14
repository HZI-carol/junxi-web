using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace vgo.frontsite004.Models
{
    public class Tags
    {
        /// <summary>
        /// 获取或设置自增id
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// 获取或设置标签文本
        /// </summary>
        public string text { get; set; }

        /// <summary>
        /// 获取或设置标签的值
        /// </summary>
        public int value { get; set; }

        /// <summary>
        /// 获取或设置父级id
        /// </summary>
        public int parentid { get; set; }

        /// <summary>
        /// 获取子集的表情列表
        /// </summary>
        public IEnumerable<Tags> childtags { get; set; } = new List<Tags>();
    }
}