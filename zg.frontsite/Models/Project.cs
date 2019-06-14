using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace zg.frontsite
{
    /// <summary>
    /// 项目状态 0 在建项目  1 竣工项目  2 完工项目  3 停工项目 4 暂停  5 未开工项目
    /// </summary>
    public enum ProjectStatus : int
    {
        在建项目 = 0,
        竣工项目 = 1,
        完工项目 = 2,
        停工项目 = 3,
        暂停 = 4,
        未开工项目 = 5
    }

    public class Project
    {
        /// <summary>
        /// 获取或设置
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// 获取或设置用户id(也就是公司或企业id)
        /// </summary>
        public int user_id { get; set; }

        /// <summary>
        /// 获取或设置作品id
        /// </summary>
        public int fk_pano_id { get; set; }

        /// <summary>
        /// 获取或设置项目名称
        /// </summary>
        public string project_name { get; set; }

        /// <summary>
        /// 获取或设置联系人
        /// </summary>
        public string contact_name { get; set; }

        /// <summary>
        /// 获取或设置联系电话
        /// </summary>
        public string contact_phone { get; set; }

        /// <summary>
        /// 获取或设置作品分类
        /// </summary>
        public int cate_id { get; set; }

        /// <summary>
        /// 获取或设置是否公开
        /// </summary>
        public int ison { get; set; }

        /// <summary>
        /// 获取或设置作品标签
        /// </summary>
        public string tag_id { get; set; }

        /// <summary>
        /// 获取或设置作品行业
        /// </summary>
        public string trade { get; set; }

        /// <summary>
        /// 获取或设置是否开启项目logo 0 未开启 1 开启
        /// </summary>
        public bool islogo { get; set; }

        /// <summary>
        /// 获取或设置项目logo地址
        /// </summary>
        public string logo_url { get; set; }

        public string full_logo_url { get; set; }

        /// <summary>
        /// 获取或设置是是否开启项目封面 0 未开启 1 开启
        /// </summary>
        public bool isimage { get; set; }

        /// <summary>
        /// 获取或设置项目封面地址
        /// </summary>
        public string image_url { get; set; }

        public string full_image_url { get; set; }

        /// <summary>
        /// 获取或设置省
        /// </summary>
        public string province { get; set; }

        /// <summary>
        /// 获取或设置省编号id
        /// </summary>
        public string province_id { get; set; }

        /// <summary>
        /// 获取或设置市
        /// </summary>
        public string city { get; set; }

        /// <summary>
        /// 获取或设置市编号id
        /// </summary>
        public string city_id { get; set; }

        /// <summary>
        /// 获取或设置区/县级
        /// </summary>
        public string area { get; set; }

        /// <summary>
        /// 获取或设置区/县级编号id
        /// </summary>
        public string area_id { get; set; }

        /// <summary>
        /// 获取或设置详细地址
        /// </summary>
        public string address { get; set; }

        /// <summary>
        /// 获取或设置房间数量
        /// </summary>
        public int room_count { get; set; }

        /// <summary>
        /// 获取或设置几厅
        /// </summary>
        public int hall_count { get; set; }

        /// <summary>
        /// 获取或设置几卫
        /// </summary>
        public int bathroom_count { get; set; }

        /// <summary>
        /// 获取或设置建筑面积(m2)
        /// </summary>
        public double floorage { get; set; }

        /// <summary>
        /// 获取或设置装修总价
        /// </summary>
        public double total_price { get; set; }

        /// <summary>
        /// 获取或设置装修风格
        /// </summary>
        public int decoration_style_id { get; set; }

        /// <summary>
        /// 获取或设置场景分组id
        /// </summary>
        public int scene_group_id { get; set; }

        /// <summary>
        /// 获取或设置项目状态：0 在建项目  1 竣工项目  2 完工项目  3 停工项目 4 暂停  5 未开工项目
        /// </summary>
        public int status { get; set; }

        /// <summary>
        /// 获取或设置是否精选项目(推荐项目) 0 否，1 是
        /// </summary>
        public bool isrecommond { get; set; }

        /// <summary>
        /// 获取或设置备注
        /// </summary>
        public string remark { get; set; }

        /// <summary>
        /// 获取或设置是否删除
        /// </summary>
        public bool isdeleted { get; set; }

        /// <summary>
        /// 获取或设置创建时间
        /// </summary>
        public DateTime created { get; set; }

        /// <summary>
        /// 获取或设置更新时间
        /// </summary>
        public DateTime updated { get; set; }

        #region 扩展字段

        /// <summary>
        /// 获取或设置项目状态类型文本
        /// </summary>
        public string status_text => GetName<ProjectStatus>(status);

        /// <summary>
        /// 装修风格
        /// </summary>
        public string style_text { get; set; }

        /// <summary>
        /// 标识类型 1、项目logo 2、背景音乐 3、微信分享 4、加载动画 5、项目封面 6、作品加密 7、常用设置是否开启
        /// </summary>
        public int type { get; set; }

        /// <summary>
        /// 作品分类名称
        /// </summary>
        public string cate_name { get; set; }

        /// <summary>
        /// 浏览量
        /// </summary>
        public int seecount { get; set; }

        public string panoview_url { get; set; }

        public string pc_panoview_url { get; set; }

        /// <summary>
        /// 公司名称
        /// </summary>
        public string company_name { get; set; }

        /// <summary>
        /// 公司简介
        /// </summary>
        public string description { get; set; }

        /// <summary>
        /// 公司二维码
        /// </summary>
        public string full_qrcode_url { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        public string nickname { get; set; }

        #endregion

        /// <summary>
        /// 获取类型名称
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        static string GetName<T>(object value)
        {
            if (Enum.IsDefined(typeof(T), value))
            {
                return Enum.GetName(typeof(T), value);
            }

            return "";
        }

    }


}