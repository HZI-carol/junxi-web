using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace zg.frontsite
{
    public class Companys
    {
        /// <summary>
        /// 获取或设置用户id
        /// </summary>
        public int user_id { get; set; }

        /// <summary>
        /// 获取或设置公司名称
        /// </summary>
        public string company_name { get; set; }

        /// <summary>
        /// 获取或设置类型id
        /// </summary>
        public int type_id { get; set; }

        /// <summary>
        /// 获取或设置联系人
        /// </summary>
        public string contact_name { get; set; }

        /// <summary>
        /// 获取或设置联系电话
        /// </summary>
        public string contact_phone { get; set; }

        /// <summary>
        /// 获取或设置省
        /// </summary>
        public string province { get; set; }

        /// <summary>
        /// 获取或设置省份id
        /// </summary>
        public string province_id { get; set; }

        /// <summary>
        /// 获取或设置市
        /// </summary>
        public string city { get; set; }

        /// <summary>
        /// 获取或设置城市id
        /// </summary>
        public string city_id { get; set; }

        /// <summary>
        /// 获取或设置区/县级
        /// </summary>
        public string area { get; set; }

        /// <summary>
        /// 获取或设置区/县级id
        /// </summary>
        public string area_id { get; set; }

        /// <summary>
        /// 获取或设置详细地址
        /// </summary>
        public string address { get; set; }

        /// <summary>
        /// 获取或设置官网地址
        /// </summary>
        public string site_url { get; set; }

        /// <summary>
        /// 获取或设置公司logo
        /// </summary>
        public string logo { get; set; }

        public string full_logo { get; set; }

        /// <summary>
        /// 获取或设置公司公众号二维码
        /// </summary>
        public string qrcode_url { get; set; }

        /// <summary>
        /// 获取或设置公司资质
        /// </summary>
        public string license_image_url { get; set; }

        /// <summary>
        /// 获取或设置公司简介
        /// </summary>
        public string description { get; set; }

        /// <summary>
        /// 获取或设置创建时间
        /// </summary>
        public DateTime created { get; set; }

        /// <summary>
        /// 获取或设置更新时间
        /// </summary>
        public DateTime? updated { get; set; }

        #region 扩展

        public IEnumerable<Project> project_list = new List<Project>();

        /// <summary>
        /// 公司对应项目下的项目总条数
        /// </summary>
        public int total { get; set; }

        /// <summary>
        /// 类型名称
        /// </summary>
        public string type_name { get; set; }

        /// <summary>
        /// 作品id
        /// </summary>
        public int fk_pano_id { get; set; }

        /// <summary>
        /// 浏览总量
        /// </summary>
        public int seecount { get; set; }

        /// <summary>
        /// 是否认证状态 4、认证
        /// </summary>
        public int isrzcert { get; set; }

        #endregion
    }
}