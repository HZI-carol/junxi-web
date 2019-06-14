using QRCoder;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace zg.frontsite
{
    [RoutePrefix("common")]
    public class CommonController : Controller
    {
        /// <summary>
        /// 获取指定内容的二维码图片
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [Route("qrcode")]
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

        /// <summary>
        /// 通用全局配置
        /// </summary>
        /// <returns></returns>
        [Route("js/config")]
        public JavaScriptResult GetGlobalConfig()
        {
            return JavaScript("document.write('<script src=\"" + WebConfigs.ApiUrl + "common/js/config\"></script>');");
        }
    }
}