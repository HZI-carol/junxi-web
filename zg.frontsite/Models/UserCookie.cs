using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace zg.frontsite
{
    public class UserCookie
    {
        public string access_token { get; set; }

        public long expires_in { get; set; }

        public int id { get; set; }

        public string nickname { get; set; }

        public string avatar { get; set; }

        public int status { get; set; }

        public string isads { get; set; }

        public string isadz { get; set; }

        public string istour { get; set; }

        public string panonum { get; set; }

        public string integral { get; set; }

        public string packageid { get; set; }

        public string overtdt { get; set; }

        public string packagename { get; set; }
    }
}