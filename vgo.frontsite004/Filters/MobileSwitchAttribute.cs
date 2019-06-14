using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace vgo.frontsite004.Filters
{
    /// <summary>
    /// 表示手机端或平板访问切换
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class MobileSwitchAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// 手机端页面地址
        /// </summary>
        public string Path { get; }

        /// <summary>
        /// 手机端页面参数
        /// 仅支持：page/:id格式
        /// </summary>
        public string ParamName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <param name="paramName">手机端页面参数（仅支持：page/:id格式）</param>
        public MobileSwitchAttribute(string path = "/m/", string paramName = "")
        {
            this.Path = path;
            this.ParamName = paramName;
            if (!Path.EndsWith("/"))
            {
                Path += "/";
            }
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (WebConfigs.IsMobileRequestAutoSwitch)
            {
                var request = filterContext.HttpContext.Request;
                if (Regex.IsMatch(request.UserAgent.ToLower(), "(android|ipad|iphone|mobile)"))
                {
                    //获取参数值
                    string paramValue = "";
                    if (!string.IsNullOrEmpty(ParamName))
                    {
                        //判断页面是否以html结尾
                        if (WebConfigs.IsHtmlSuffix)
                        {
                            paramValue = request[ParamName];
                        }
                        else
                        {
                            paramValue = filterContext.RouteData.Values[ParamName] + "";
                        }
                    }
                    string realPath = Path;
                    if (!string.IsNullOrEmpty(paramValue))
                    {
                        realPath += paramValue;
                    }
                    filterContext.Result = new RedirectResult(realPath);
                }
                else
                {
                    base.OnActionExecuting(filterContext);
                }
            }
        }
    }
}