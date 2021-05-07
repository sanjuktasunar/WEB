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

            bundles.Add(new ScriptBundle("~/web/back-end/jquery-ui").Include(
                       "~/assets/vendor/jquery/jquery.min.js"));


            bundles.Add(new ScriptBundle("~/web/back-end/datatable").Include(
                       "~/assets/vendor/datatables/jquery.dataTables.min.js",
                       "~/assets/vendor/datatables/dataTables.bootstrap4.min.js"));

            bundles.Add(new ScriptBundle("~/web/back-end/message").Include(
                      "~/assets/custom-plugin/sweetalert.min.js",
                      "~/assets/custom-plugin/ShowMessage.js"));
        }
    }
}
