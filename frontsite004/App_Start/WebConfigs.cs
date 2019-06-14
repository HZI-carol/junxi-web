using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using vgo.frontsite004.Models;

namespace vgo.frontsite004
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
        /// 获取页面路径
        /// </summary>
        /// <param name="path">页面路径；如：/pano</param>
        /// <returns></returns>
        public static string GetPagePath(string path)
        {
            if (!string.IsNullOrEmpty(path) && IsHtmlSuffix)
            {
                return $"{path}.html";
            }

            return path;
        }

        /// <summary>
        /// 获取详细页面路径
        /// </summary>
        /// <param name="action">action名称；如：news</param>
        /// <param name="id">详情页id</param>
        /// <returns></returns>
        public static string GetDetailPagePath(string action, int id)
        {
            if (!string.IsNullOrEmpty(action))
            {
                return IsHtmlSuffix ? $"/{action}-detail.html?id={id}" : $"/{action}/{id}";
            }

            return action;
        }

        /// <summary>
        /// 接口地址
        /// </summary>
        public static string ApiUrl => ConfigurationManager.AppSettings["ApiUrl"] ?? "";

        /// <summary>
        /// 平台logo地址
        /// </summary>
        public static string LogoUrl => Config.logo_url ?? "/content/images/logo.png";

        /// <summary>
        /// 签名key
        /// </summary>
        public static string SharedKey => ConfigurationManager.AppSettings["SharedKey"] ?? "";

        /// <summary>
        /// cookiedomain
        /// </summary>
        public static string CookieDomain
        {
            get
            {
                string cookie = ConfigurationManager.AppSettings["CookieDomain"] ?? "";
#if DEBUG
                cookie = "localhost";
#endif
                if (UseNewUserbackstage)
                {
                    cookie = Config.cookiedomain;
                }

                return cookie;
            }
        }

        /// <summary>
        /// 设置的cookie名称
        /// </summary>
        public static string CookieName => ConfigurationManager.AppSettings["CookieName"] ?? "UserAccount";

        /// <summary>
        /// 是否使用新用户后台
        /// </summary>
        public static bool UseNewUserbackstage => ConfigurationManager.AppSettings["site.userbackstage.usenew"] == "1";

        /// <summary>
        /// 是否手机端自动跳转
        /// </summary>
        public static bool IsMobileRequestAutoSwitch => ConfigurationManager.AppSettings["site.mobile.request.autoswitch"] == "1";

        /// <summary>
        /// 是否静态页面后缀
        /// </summary>
        public static bool IsHtmlSuffix => ConfigurationManager.AppSettings["pages.suffix.html"] == "1";

        /// <summary>
        /// 默认SEO描述
        /// </summary>
        public static string DefaultSeoDescription => ConfigurationManager.AppSettings["pages.seo.description"] ?? "";

        /// <summary>
        /// 默认SEO关键字
        /// </summary>
        public static string DefaultSeoKeywords => ConfigurationManager.AppSettings["pages.seo.keywords"] ?? "";

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

                return GetPagePath("/login");
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
                    return Config.url_user + "#/user/personal-info";
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