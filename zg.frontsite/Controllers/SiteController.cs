using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;

namespace zg.frontsite
{
    /// <summary>
    /// 登录注册相关接口
    /// </summary>
    [RoutePrefix("")]
    [MobileSwitch]
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
    /// 表示手机端或平板访问切换
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class MobileSwitchAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var request = filterContext.HttpContext.Request;
            if (Regex.IsMatch(request.UserAgent.ToLower(), "(android|ipad|iphone|mobile)"))
            {
                filterContext.Result = new RedirectResult("/m");
            }
            else
            {
                base.OnActionExecuting(filterContext);
            }
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
        public async Task<ActionResult> Index()
        {
            //获取首页Banner列表
            var bannersList = (await ApiProvider.GetBannerListAsync(1, 6, 0));

            if (bannersList != null)
            {
                ViewBag.BannersList = bannersList.list;
            }

            //获取首页精选推荐列表
            var projectList = await ApiProvider.GetProjectPagedListAsync(1, 6, "", 1);
            if (projectList != null)
            {
                ViewBag.ProjectList = projectList.list;
            }

            //获取优秀装企列表
            var companyList = await ApiProvider.GetCompanyPagedListAsync(1, 4);
            if (companyList != null)
            {
                ViewBag.CompanyList = companyList.list;
            }

            //获取资讯信息
            var newsList = await ApiProvider.GetNewsPagedListAsync(1, 3);
            if (newsList != null)
            {
                ViewBag.NewsList = newsList.list;
            }

            //获取资讯推荐热眯信息
            var newsSeecountList = await ApiProvider.GetNewsPagedListAsync(1, 4, 0, "seecount_DESC");
            if (newsSeecountList != null)
            {
                ViewBag.NewsSeecountList = newsSeecountList.list;
            }

            //获取首页广告位
            var adbannersList = (await ApiProvider.GetBannerListAsync(1, 1, 1));

            if (adbannersList != null)
            {
                ViewBag.ADBannersList = adbannersList.list;
            }

            return View();
        }

        /// <summary>
        /// 全景案例
        /// </summary>
        /// <returns></returns>
        [Route("pano")]
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
        /// 新闻咨询
        /// </summary>
        /// <returns></returns>
        [Route("news")]
        public async Task<ActionResult> News()
        {
            //获取首页Banner列表
            var bannersList = (await ApiProvider.GetBannerListAsync(1, 6, 4));

            if (bannersList != null)
            {
                ViewBag.BannersList = bannersList.list;
            }

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

            return View();
        }

        /// <summary>
        /// 资讯详情
        /// </summary>
        /// <returns></returns>
        [Route("news/{id}")]
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
        public async Task<ActionResult> About()
        {
            //获取首页Banner列表
            var bannersList = (await ApiProvider.GetBannerListAsync(1, 6, 5));

            if (bannersList != null)
            {
                ViewBag.BannersList = bannersList.list;
            }

            return View();
        }

        /// <summary>
        /// 全景地图
        /// </summary>
        /// <returns></returns>
        [Route("panomap")]
        public ActionResult PanoMap()
        {
            return View();
        }
    }

    /// <summary>
    /// 宅构相关
    /// </summary>
    public partial class SiteController : Controller
    {
        #region 项目相关

        /// <summary>
        /// 项目页面
        /// </summary>
        /// <returns></returns>
        [Route("project")]
        public async Task<ActionResult> Project()
        {
            //获取首页Banner列表
            var bannersList = (await ApiProvider.GetBannerListAsync(1, 6, 2));
            if (bannersList != null)
            {
                ViewBag.BannersList = bannersList.list;
            }
            // 获取项目装修风格列表
            ViewBag.DecorationStyleList = (await ApiProvider.GetDecorationStyleListAsync());
            //获取初始页面数据
            var projectList = await ApiProvider.GetProjectPagedListAsync(1, 12, "", -1);
            if (projectList != null)
            {
                ViewBag.ProjectList = projectList;
            }

            return View();
        }

        /// <summary>
        /// 查看全景
        /// </summary>
        /// <returns></returns>
        [Route("panoview/{fk_pano_id}")]
        public async Task<ActionResult> PanoView(int fk_pano_id, int user_id)
        {
            var model = await ApiProvider.GetCompanyAndProjectAsync(fk_pano_id, user_id);
            if (model != null)
            {
                var panorightsList = await ApiProvider.GetProjectPagedListAsync(1, 10, model.tag_id, "");
                ViewBag.PanoRightList = panorightsList;

                var panobutonsList = await ApiProvider.GetProjectPagedListAsync(1, 3, model.tag_id, "seecount_DESC");

                ViewBag.PanoButonsList = panobutonsList.list;
            }
            else
            {
                return HttpNotFound();
            }

            return View(model);
        }


        #endregion

        #region 公司相关

        /// <summary>
        /// 装企公司页面
        /// </summary>
        /// <returns></returns>
        [Route("company")]
        public async Task<ActionResult> Company()
        {
            //获取首页Banner列表
            var bannersList = (await ApiProvider.GetBannerListAsync(1, 6, 3));
            if (bannersList != null)
            {
                ViewBag.BannersList = bannersList.list;
            }
            //获取初始页面数据
            var companyList = await ApiProvider.GetCompanyPagedListAsync(1, 8);
            if (companyList != null)
            {
                ViewBag.CompanyList = companyList;
            }

            return View();
        }

        /// <summary>
        /// 获取装企公司详情页
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("company/{id}")]
        public async Task<ActionResult> CompanyDetail(int id)
        {
            if (id <= 0)
            {
                return HttpNotFound();
            }
            ViewBag.Id = id;
            var data = await ApiProvider.GetCompanyAsync(id);

            return View(data);
        }

        #endregion
    }
}