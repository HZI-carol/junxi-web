using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace vgo.partner.frontsite.Models
{
    public class SiteConfig
    {
        public int partner_id { get; set; }

        public string site_name { get; set; }

        public string site_domain { get; set; }

        public string logo_url { get; set; }

        public string full_logo_url { get; set; }

        public string company_name { get; set; }

        public string contact_name { get; set; }

        public string contact_phone { get; set; }

        public string qq { get; set; }

        public string wxqrcode_url { get; set; }

        public string full_wxqrcode_url { get; set; }

        public string about_us { get; set; }

        public string copyright { get; set; }

        public IEnumerable<PartnerSiteBanner> banner_list = new List<PartnerSiteBanner>();

        public IEnumerable<SitePano> pano_list = new List<SitePano>();
    }

    public class PartnerSiteBanner
    {
        public int id { get; set; }

        public string full_banner_url { get; set; }

        public string link_url { get; set; }

        public int sort { get; set; }
    }

    public class SitePano
    {
        public string pano_id { get; set; }

        public string pano_name { get; set; }

        public int seecount { get; set; }

        public int praisecount { get; set; }

        public string icon_src { get; set; }

        public string full_icon_src { get; set; }

        public string user_nickname { get; set; }

        public string user_avatar { get; set; }

        public string full_avatar { get; set; }

        public string panoview_url { get; set; }
    }
}