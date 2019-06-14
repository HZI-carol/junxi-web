using QRCoder;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using vgo.partner.frontsite.Models;

namespace vgo.partner.frontsite.Controllers
{
    [RoutePrefix("")]
    public partial class HomeController : Controller
    {
        /// <summary>
        /// 获取站点配置
        /// </summary>
        /// <returns></returns>
        async Task GetSiteConfigAsync()
        {
            string domain = Request.Url.Host;
            string key = $"siteconfig:{domain}";
            if (!CacheProvider.TryGet(key, out SiteConfig model))
            {
                model = await ApiProvider.GetSiteConfigAsync(domain);
                if (model != null)
                {
                    CacheProvider.Set(key, model, TimeSpan.FromMinutes(5));
                }
            }
            //默认使用系统配置
            if (model == null)
            {
                model = new SiteConfig
                {
                    partner_id = 0,
                    site_name = WebConfigs.Config.platname,
                    copyright = WebConfigs.Config.common_page_footer,
                    qq = WebConfigs.Config.common_page_serviceqq,
                    contact_phone = WebConfigs.Config.common_page_hotline,
                    full_logo_url = WebConfigs.Config.logo_url,
                    full_wxqrcode_url = "/Content/images/qrcode.png"
                };
            }
            ViewBag.SiteConfig = model;
        }

        /// <summary>
        /// 首页
        /// </summary>
        /// <returns></returns>
        [Route("~/")]
        public async Task<ActionResult> Index()
        {
            //获取站点配置
            await GetSiteConfigAsync();

            return View();
        }

        /// <summary>
        /// 获取指定内容的二维码图片
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [Route("common/qrcode")]
        public ActionResult GetQrcode(string data)
        {
            if (!string.IsNullOrEmpty(data))
            {
                string hash = HashAlgorithmProvider.ComputeHash("MD5", data, true);
                string key = $"qrcode:{hash}";
                if (!CacheProvider.TryGet(key, out byte[] buffer))
                {
                    QRCodeGenerator qrGenerator = new QRCodeGenerator();
                    QRCodeData qrCodeData = qrGenerator.CreateQrCode(data, QRCodeGenerator.ECCLevel.Q);
                    Bitmap icon = null;
                    if (System.IO.File.Exists(Server.MapPath("~/favicon.ico")))
                    {
                        icon = new Bitmap(Server.MapPath("~/favicon.ico"));
                    }
                    QRCode qrCode = new QRCode(qrCodeData);
                    using (Bitmap qrCodeImage = qrCode.GetGraphic(10, Color.Black, Color.White, icon))
                    {
                        var stream = new MemoryStream();
                        qrCodeImage.Save(stream, ImageFormat.Png);
                        stream.Position = 0;
                        buffer = stream.ToArray();
                        stream.Dispose();
                    }
                    if (buffer != null)
                    {
                        CacheProvider.Set(key, buffer, TimeSpan.FromMinutes(10));
                    }
                }

                if (buffer != null)
                {
                    return File(buffer, "image/png");
                }
            }

            return HttpNotFound();
        }
    }

    public partial class HomeController : Controller
    {
        /// <summary>
        /// 全景案例
        /// </summary>
        /// <returns></returns>
        [Route("pano")]
        public async Task<ActionResult> Pano()
        {
            //获取站点配置
            await GetSiteConfigAsync();

            //获取标签数据
            ViewBag.Tags = (await ApiProvider.GetTagsListAsync());
            //获取初始页面数据
            ViewBag.PagedList = await ApiProvider.GetPanoPagedListAsync(1, 12, ViewBag.SiteConfig?.partner_id ?? 0);

            return View();
        }

        /// <summary>
        /// 摄影师
        /// </summary>
        /// <returns></returns>
        [Route("photographer")]
        public async Task<ActionResult> Photographer()
        {
            //获取站点配置
            await GetSiteConfigAsync();
            //获取初始页面数据
            ViewBag.PagedList = await ApiProvider.GetUserPagedListAsync(1, 12, "", ViewBag.SiteConfig?.partner_id ?? 0, "");

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
            //获取站点配置
            await GetSiteConfigAsync();
            //获取初始页面数据
            ViewBag.PagedList = await ApiProvider.GetPanoPagedListAsync(1, 12, user_id: id);

            return View(user);
        }

        /// <summary>
        /// 资讯
        /// </summary>
        /// <returns></returns>
        [Route("news")]
        public async Task<ActionResult> News()
        {
            //获取站点配置
            await GetSiteConfigAsync();
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
        public async Task<ActionResult> NewsDetail(int id)
        {
            if (id <= 0) return HttpNotFound();

            //获取站点配置
            await GetSiteConfigAsync();
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
            //获取站点配置
            await GetSiteConfigAsync();

            return View();
        }
    }
}