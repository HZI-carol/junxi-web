using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using vgo.partner.frontsite.Models;

namespace vgo.partner.frontsite
{
    public static class WebConfigs
    {
        /// <summary>
        /// 系统配置对象
        /// </summary>
        public static SysConfig Config { private set; get; }

        /// <summary>
        /// 初始化系统配置
        /// </summary>
        /// <returns></returns>
        public async static Task InitSysConfigAsync()
        {
            Config = await ApiProvider.GetSysConfigAsync();
        }

        /// <summary>
        /// 接口地址
        /// </summary>
        public static string ApiUrl => ConfigurationManager.AppSettings["ApiUrl"] ?? "";

        /// <summary>
        /// 默认的全景热门搜索关键字
        /// </summary>
        static readonly string[] DefaultPanoHotKeyword = new string[] { "城市", " 房产", "高校" };

        /// <summary>
        /// 获取全景热门搜索关键字
        /// </summary>
        public static string[] PanoHotKeywords
        {
            get
            {
                string[] keywords = null;
                string str = ConfigurationManager.AppSettings["pages.pano.hot.keywords"] ?? "";
                if (!string.IsNullOrEmpty(str))
                {
                    keywords = str.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                }
                if (keywords == null || !keywords.Any())
                {
                    return DefaultPanoHotKeyword;
                }

                return keywords;
            }
        }


        /// <summary>
        /// 是否使用新用户后台
        /// </summary>
        public static bool UseNewUserbackstage => ConfigurationManager.AppSettings["site.userbackstage.usenew"] == "1";


        #region 登录、注册、用户后台相关地址

        /// <summary>
        /// 登录地址
        /// </summary>
        public static string LoginUrl
        {
            get
            {
                if (UseNewUserbackstage)
                {
                    return $"{ApiUrl}user/login?redirect_uri={HttpUtility.UrlEncode(WebConfigs.Config.url_qianduan)}";
                }

                return Config.url_user + "login.aspx";
            }
        }

        /// <summary>
        /// 注册地址
        /// </summary>
        public static string RegisterUrl
        {
            get
            {
                if (UseNewUserbackstage)
                {
                    return $"{ApiUrl}user/register?redirect_uri={HttpUtility.UrlEncode(WebConfigs.Config.url_qianduan)}";
                }

                return Config.url_user + "register.aspx";
            }
        }

        /// <summary>
        /// 用户中心首页
        /// </summary>
        public static string UserCenterUrl
        {
            get
            {
                if (UseNewUserbackstage)
                {
                    return Config.url_user + "#/home";
                }

                return Config.url_user + "user/index.aspx";
            }
        }

        /// <summary>
        /// 用户作品地址
        /// </summary>
        public static string UserPanoUrl
        {
            get
            {
                if (UseNewUserbackstage)
                {
                    return Config.url_user + "#/panorama-manage";
                }

                return Config.url_user + "user/pano-list.aspx";
            }
        }

        /// <summary>
        /// 用户账户地址
        /// </summary>
        public static string UserAccountUrl
        {
            get
            {
                if (UseNewUserbackstage)
                {
                    return Config.url_user + "#/personal-info";
                }

                return Config.url_user + "AccountDetail/Index.aspx";
            }
        }

        /// <summary>
        /// 用户消息地址
        /// </summary>
        public static string UserMsgUrl
        {
            get
            {
                if (UseNewUserbackstage)
                {
                    return Config.url_user + "#/message/personal";
                }

                return Config.url_user + "SystemNotice/Index.aspx";
            }
        }

        /// <summary>
        /// 用户作品市场我的订单地址
        /// </summary>
        public static string PMarketBuyOrderUrl
        {
            get
            {
                if (UseNewUserbackstage)
                {
                    return Config.url_user + "#/smart-city/my-order";
                }

                return Config.url_user + "Pmarket/BuyerOrder.aspx";
            }
        }

        #endregion
    }
}