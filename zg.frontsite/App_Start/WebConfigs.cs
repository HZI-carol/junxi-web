using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace zg.frontsite
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
                string cookie = Config.cookiedomain;
#if DEBUG
                cookie = "localhost";
#endif


                return cookie;
            }
        }

        /// <summary>
        /// 设置的cookie名称
        /// </summary>
        public static string CookieName => ConfigurationManager.AppSettings["CookieName"] ?? "UserAccount";

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
        static readonly string[] DefaultProjectHotKeyword = new string[] { "城市", "装饰花园", "展会" };

        /// <summary>
        /// 默认的装企业公司搜索关键字
        /// </summary>
        static readonly string[] DefaultCompanyHotKeyword = new string[] { "建材装企", "装饰公司", "德尚装企" };

        /// <summary>
        /// 获取全景项目热门搜索关键字
        /// </summary>
        public static string[] ProjectHotKeywords
        {
            get
            {
                string[] keywords = null;
                string str = ConfigurationManager.AppSettings["pages.project.hot.keywords"] ?? "";
                if (!string.IsNullOrEmpty(str))
                {
                    keywords = str.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                }
                if (keywords == null || !keywords.Any())
                {
                    return DefaultProjectHotKeyword;
                }

                return keywords;
            }
        }

        /// <summary>
        /// 获取全景热门搜索关键字
        /// </summary>
        public static string[] CompanyHotKeywords
        {
            get
            {
                string[] keywords = null;
                string str = ConfigurationManager.AppSettings["pages.company.hot.keywords"] ?? "";
                if (!string.IsNullOrEmpty(str))
                {
                    keywords = str.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                }
                if (keywords == null || !keywords.Any())
                {
                    return DefaultCompanyHotKeyword;
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
                return $"{ApiUrl}user/login?redirect_uri={HttpUtility.UrlEncode(WebConfigs.Config.url_qianduan)}";
            }
        }

        /// <summary>
        /// 注册地址
        /// </summary>
        public static string RegisterUrl
        {
            get
            {
                return $"{ApiUrl}user/register?redirect_uri={HttpUtility.UrlEncode(WebConfigs.Config.url_qianduan)}";
            }
        }

        /// <summary>
        /// 用户中心首页
        /// </summary>
        public static string UserCenterUrl
        {
            get
            {
                return Config.url_user + "#/home";
            }
        }

        /// <summary>
        /// 用户作品地址
        /// </summary>
        public static string UserPanoUrl
        {
            get
            {
                return Config.url_user + "#/project-manage";
            }
        }

        /// <summary>
        /// 全景查看
        /// </summary>
        public static string UserPanoViewUrl
        {
            get
            {
                return Config.url_new_view + "#/panoview/";
            }
        }

        /// <summary>
        /// 用户账户地址
        /// </summary>
        public static string UserAccountUrl
        {
            get
            {
                return Config.url_user + "#/user/company-info";
            }
        }

        /// <summary>
        /// 用户消息地址
        /// </summary>
        public static string UserMsgUrl
        {
            get
            {
                return Config.url_user + "#/message/personal";
            }
        }

        /// <summary>
        /// 用户作品市场我的订单地址
        /// </summary>
        public static string PMarketBuyOrderUrl
        {
            get
            {
                return Config.url_user + "#/smart-city/my-order";
            }
        }

        #endregion
    }
}