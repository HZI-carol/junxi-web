using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace zg.frontsite
{
    public class User
    {
        public int user_id { get; set; }

        public string nickname { get; set; }

        public string avatar { get; set; }

        public int seecount { get; set; }

        public int praisecount { get; set; }

        public int panonum { get; set; }

        public string area { get; set; }

        public string areatext { get; set; }

        public string dec { get; set; }

        public int isrz { get; set; }
    }
}