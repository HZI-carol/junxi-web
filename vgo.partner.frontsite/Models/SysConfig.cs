using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace vgo.partner.frontsite.Models
{
    /// <summary>
    /// 系统配置类
    /// </summary>
    public class SysConfig
    {
        public string platname { get; set; }

        public string logo_url { get; set; }

        public string cookiedomain { get; set; }

        public string companyname { get; set; }

        public string common_page_footer { get; set; }

        public string common_page_hotline { get; set; }

        public string common_page_serviceqq { get; set; }

        public string url_qianduan { get; set; }

        public string url_user { get; set; }
    }
}