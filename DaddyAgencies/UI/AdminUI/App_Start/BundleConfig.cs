using System.Web;
using System.Web.Optimization;

namespace AdminUI
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                       "~/Scripts/jquery-3.3.1.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new ScriptBundle("~/bundles/theme").Include(
                      "~/Scripts/js/main/jquery.min.js",
                      "~/Scripts/js/main/bootstrap.bundle.min.js",
                      "~/Scripts/js/app.js",
                      "~/Scripts/bootbox.min.js",
                      "~/Scripts/Site.js",
                      "~/Scripts/custom.js"
                      ));

            bundles.Add(new StyleBundle("~/Content/theme").Include(
                      "~/Content/css/animate.min.css",
                      "~/Content/css/bootstrap.min.css",
                      "~/Content/css/bootstrap_limitless.min.css",
                      "~/Content/css/colors.min.css",
                      "~/Content/css/components.min.css",
                      "~/Content/css/fontawesome_icons.min.css",
                      "~/Content/css/icomoon_icons.css",
                      "~/Content/css/layout.min.css",
                      "~/Content/css/material_icons.css",
                      "~/Content/alertify/alertify.min.css"));

            bundles.Add(new StyleBundle("~/Content/bootstrap").Include(
               "~/Content/Site.css"));

            BundleTable.EnableOptimizations = false;
        }
    }
}
