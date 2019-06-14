using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;
using vgo.frontsite004.Filters;
using vgo.frontsite004.Models;

namespace vgo.frontsite004.Controllers
{
    /// <summary>
    /// 登录注册相关接口
    /// </summary>
    [RoutePrefix("")]
    public partial class SiteController : Controller
    {
        string m_Token = "";

        /// <summary>
        /// 页面认证
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnAuthentication(AuthenticationContext filterContext)
        {
            var request = filterContext.HttpContext.Request;
            var cookie = request.Cookies[WebConfigs.CookieName];
            if (cookie != null)
            {
                ViewBag.User = ApiProvider.GetHttpCookie(cookie);
                if (ViewBag.User != null)
                {
                    m_Token = ViewBag.User.access_token;
                    ViewBag.token = m_Token;
                }
            }
            base.OnAuthentication(filterContext);
        }

        /// <summary>
        /// 登录页面
        /// </summary>
        /// <returns></returns>
        [Route("login")]
        [Route("login.html")]
        public ActionResult Login()
        {
            if (ViewBag.User != null)
            {
                return Redirect("/");
            }
            // 判断是否使用新登录页面
            if (WebConfigs.UseNewUserbackstage)
            {
                return Redirect(WebConfigs.ApiUrl + "user/login?redirect_uri=" + HttpUtility.UrlEncode(WebConfigs.Config.url_qianduan));
            }

            return View();
        }

        /// <summary>
        /// 获取验证码
        /// </summary>
        /// <returns></returns>
        [Route("vcode")]
        public ActionResult VerifyCode()
        {
            string code = RandomStringGenerator.CreateRandomNumeric(5);
            byte[] buffer = ValidateCodeGenerator.CreateImage(code);
            Session["vcode"] = code;

            return File(buffer, "image/png");
        }

        /// <summary>
        /// 登录接口
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        [Route("api/login")]
        public async Task<ActionResult> LoginAsync(Login json)
        {
            // 判断是否使用新登录页面
            if (WebConfigs.UseNewUserbackstage)
            {
                return HttpNotFound();
            }

            var result = new JsonApiResult<string> { data = "" };

            try
            {
                if (json == null) throw new ArgumentException("请输入用户名");
                if (string.IsNullOrEmpty(json.username)) throw new ArgumentException("用户名不能为空");
                if (string.IsNullOrEmpty(json.password)) throw new ArgumentException("密码不能为空");
                if (string.IsNullOrEmpty(json.vcode) || !json.vcode.Equals(Session["vcode"])) throw new ArgumentException("验证码错误");

                //移除验证码
                Session.Remove("vcode");

                var cookie = await ApiProvider.LoginAsync(json.username, json.password);
                if (!cookie.success)
                {
                    result.code = cookie.code;
                    result.msg = cookie.msg;
                }
                else
                {
                    //写入cookie
                    Response.Cookies.Add(ApiProvider.GetWriteCookie(cookie.data));
                    result.code = cookie.code;
                }
            }
            catch (ArgumentException e)
            {
                result.code = -1;
                result.msg = e.Message;
            }

            return Json(result);
        }

        /// <summary>
        /// 注销登录
        /// </summary>
        /// <returns></returns>
        [Route("logout")]
        [Route("logout.html")]
        public ActionResult Logout()
        {
            ApiProvider.ClearCookie(Response);
            return Redirect("/");
        }
    }

    /// <summary>
    /// 页面相关
    /// </summary>
    public partial class SiteController : Controller
    {
        /// <summary>
        /// 首页
        /// </summary>
        /// <returns></returns>
        [Route("~/")]
        [MobileSwitch]
        public async Task<ActionResult> Index()
        {
            //获取首页精选作品列表
            ViewBag.List = await ApiProvider.GetIndexListAsync(8);
            //获取资讯信息
            var newsList = await ApiProvider.GetNewsPagedListAsync(1, 4);
            if (newsList != null)
            {
                ViewBag.NewsList = newsList.list;
            }

            return View();
        }

        /// <summary>
        /// 全景案例
        /// </summary>
        /// <returns></returns>
        [Route("pano")]
        [Route("pano.html")]
        [MobileSwitch("/m/#/pano")]
        public async Task<ActionResult> Pano()
        {
            //获取标签数据
            ViewBag.Tags = (await ApiProvider.GetTagsListAsync());
            //获取初始页面数据
            ViewBag.PagedList = await ApiProvider.GetPanoPagedListAsync(1, 12);

            return View();
        }

        /// <summary>
        /// 摄影师
        /// </summary>
        /// <returns></returns>
        [Route("photographer")]
        [Route("photographer.html")]
        [MobileSwitch("/m/#/author")]
        public async Task<ActionResult> Photographer()
        {
            //获取初始页面数据
            ViewBag.PagedList = await ApiProvider.GetUserPagedListAsync(1, 12, "", "");

            return View();
        }

        /// <summary>
        /// 获取摄影师详情页
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("photographer/{id}")]
        [Route("photographer-detail.html")]
        [MobileSwitch("/m/#/author", "id")]
        public async Task<ActionResult> PhotographerDetail(int id)
        {
            if (id <= 0) return HttpNotFound();
            var user = await ApiProvider.GetUserAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            //获取初始页面数据
            ViewBag.PagedList = await ApiProvider.GetPanoPagedListAsync(1, 12, id);

            return View(user);
        }

        /// <summary>
        /// 资讯
        /// </summary>
        /// <returns></returns>
        [Route("news")]
        [Route("news.html")]
        [MobileSwitch("/m/#/news")]
        public async Task<ActionResult> News()
        {
            //获取初始页面数据
            var typeList = await ApiProvider.GetNewsTypeListAsync();
            ViewBag.TypeList = typeList;
            int type_id = 0;
            if (typeList.Any())
            {
                type_id = typeList.First().id;
            }
            ViewBag.type_id = type_id;
            //获取初始页面数据
            ViewBag.PagedList = await ApiProvider.GetNewsPagedListAsync(1, 5, type_id);
            //获取最新新闻
            var hotList = await ApiProvider.GetNewsPagedListAsync(1, 5);
            if (hotList != null)
            {
                ViewBag.HotList = hotList.list;
            }

            return View();
        }

        /// <summary>
        /// 资讯详情
        /// </summary>
        /// <returns></returns>
        [Route("news/{id}")]
        [Route("news-detail.html")]
        [MobileSwitch("/m/#/news", "id")]
        public async Task<ActionResult> NewsDetail(int id)
        {
            if (id <= 0) return HttpNotFound();
            var model = await ApiProvider.GetNewsAsync(id);
            if (model == null)
            {
                return HttpNotFound();
            }

            return View(model);
        }

        /// <summary>
        /// 关于我们
        /// </summary>
        /// <returns></returns>
        [Route("about")]
        [Route("about.html")]
        [MobileSwitch("/m/#/support")]
        public ActionResult About()
        {
            return View();
        }

        /// <summary>
        /// 全景地图
        /// </summary>
        /// <returns></returns>
        [Route("panomap")]
        [Route("panomap.html")]
        public ActionResult PanoMap()
        {
            return View();
        }
    }

    /// <summary>
    /// 作品市场
    /// </summary>
    public partial class SiteController : Controller
    {
        /// <summary>
        /// 作品市场页面
        /// </summary>
        /// <returns></returns>
        [Route("panomarket")]
        [Route("panomarket.html")]
        public async Task<ActionResult> PanoMarket()
        {
            //作品分类列表
            ViewBag.TypeList = await ApiProvider.GetPanoMarketTypeListAsync();
            //作品列表
            ViewBag.PanoPagedList = await ApiProvider.GetMarketPanoListAsync(1, 12, 0, m_Token);

            return View();
        }

        /// <summary>
        /// 获取作品市场作品详情页
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("panomarket/{id}")]
        [Route("panomarket-detail.html")]
        public async Task<ActionResult> PanoMarketDetail(int id)
        {
            if (id <= 0)
            {
                return HttpNotFound();
            }
            ViewBag.Id = id;
            //作品分类列表
            var data = await ApiProvider.GetMarketPanoAsync(id, m_Token);

            return View(data);
        }
    }
}