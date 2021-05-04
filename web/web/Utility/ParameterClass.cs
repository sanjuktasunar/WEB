using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web.Utility
{
    public class ParameterClass
    {
        public string Home { get; set; }
        public string AboutUs { get; set; }
        public string ContactUs { get; set; }
        public string Login { get; set; }

        public MenuLink MenuLink { get; set; }
    }

    public class MenuLink
    {
        public string HomeLink { get; set; }
        public string AboutUsLink { get; set; }
        public string ContactUsLink { get; set; }
        public string LoginLink { get; set; }
    }
}