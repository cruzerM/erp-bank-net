using System.Web;
using System.Web.Optimization;

namespace ERP_BANK.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            // Theme css files

            bundles.Add(new StyleBundle("~/Content/cssTheme").Include(
                       "~/Content/bootstrap.min.css",
                       "~/Content/animate.min.css",
                       "~/Content/icheck/flat/green.css",
                       "~/Content/floatexamples.css",
                       "~/Content/font-awesome.min.css",
                       "~/Content/custom.css"));

            // Theme scripts

            bundles.Add(new ScriptBundle("~/bundles/jsTheme").Include(
                      "~/Scripts/nprogress.js",
                      "~/Scripts/jquery.min.js",
                      "~/Scripts/bootstrap.min.js",
                      "~/Scripts/chartjs/chart.min.js",
                      "~/Scripts/progressbar/bootstrap-progressbar.min.js",
                      "~/Scripts/nicescroll/jquery.nicescroll.min.js",
                      "~/Scripts/icheck/icheck.min.js",
                      "~/Scripts/moment.min.js",
                      "~/Scripts/echart/echarts-all.js",
                      "~/Scripts/echart/green.js",
                      "~/Scripts/custom.js",
                      //"~/Scripts/moris/raphael-min.js",
                      //"~/Scripts/moris/morris.js",
                      //"~/Scripts/moris/example.js",
                      "~/Scripts/flot/jquery.flot.js",
                      "~/Scripts/flot/jquery.flot.pie.js",
                      "~/Scripts/flot/jquery.flot.orderBars.js",
                      "~/Scripts/flot/jquery.flot.time.min.js",
                      "~/Scripts/flot/date.js",
                      "~/Scripts/flot/jquery.flot.spline.js",
                      "~/Scripts/flot/jquery.flot.stack.js",
                      "~/Scripts/flot/curvedLines.js",
                      "~/Scripts/flot/jquery.flot.resize.js",
                      "~/Scripts/respond.js"));
        }
    }
}