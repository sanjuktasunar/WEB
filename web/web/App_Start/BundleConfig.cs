using System.Web;
using System.Web.Optimization;

namespace web
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/web/front-end").Include(
                        "~/front-end/js/bootstrap.bundle.min.js",
                        "~/front-end/js/jquery.min.js",
                        "~/front-end/js/isotope.pkgd.js",
                        "~/front-end/js/templatemo.js",
                        "~/front-end/js/custom.js"));
        }
    }
}
