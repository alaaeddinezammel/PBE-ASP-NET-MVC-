using System.Web;
using System.Web.Optimization;

namespace PBE
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

            bundles.Add(new ScriptBundle("~/bundles/jqwidgets").Include(
            "~/Scripts/jqxcore.js",
            "~/Scripts/jqxwindow.js",
            "~/Scripts/jqx-all.js",
            "~/Scripts/jqxdata.js",
            "~/Scripts/jqxgrid.js",
            "~/Scripts/jqxgrid.selection.js",
            "~/Scripts/jqxgrid.pager.js",
            "~/Scripts/jqxlistbox.js",
            "~/Scripts/jqxbuttons.js",
            "~/Scripts/jqxscrollbar.js",
            "~/Scripts/jqxdatatable.js",
            "~/Scripts/jqxtreegrid.js",
            "~/Scripts/jqxmenu.js",
            "~/Scripts/jqxcalendar.js",
            "~/Scripts/jqxgrid.sort.js",
            "~/Scripts/jqxgrid.filter.js",
            "~/Scripts/jqxdatetimeinput.js",
            "~/Scripts/jqxdropdownlist.js",
            "~/Scripts/jqxslider.js",
            "~/Scripts/jqxeditor.js",
            "~/Scripts/jqxinput.js",
            "~/Scripts/jqxdraw.js",
            "~/Scripts/jqxchart.core.js",
            "~/Scripts/jqxchart.rangeselector.js",
            "~/Scripts/jqxtree.js",
            "~/Scripts/globalization/globalize.js",
            "~/Scripts/jqxbulletchart.js",
            "~/Scripts/jqxcheckbox.js",
            "~/Scripts/jqxradiobutton.js",
            "~/Scripts/jqxvalidator.js",
            "~/Scripts/jqxpanel.js",
            "~/Scripts/jqxpasswordinput.js",
            "~/Scripts/jqxnumberinput.js",
            "~/Scripts/jqxcombobox.js"
            ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/jqx.base.css",
                "~/Content/jqx.darkblue.css" 
                ));
            

            bundles.Add(new StyleBundle("~/Content/css/css").Include(
                    "~/Content/css/AdminLTE.css",
                    "~/Content/css/bootstrap.min.css",
                    "~/Content/css/font-awesome.min.css",
                    "~/Content/css/calculatrice.css",
                    "~/Content/css/ionicons.min.css"));

            bundles.Add(new StyleBundle("~/Content/css/login").Include(
                "~/Content/style_login.css"));

            bundles.Add(new StyleBundle("~/Content/css/_login").Include(
                "~/Content/cssLogin/css/bootstrap-responsive.min.css",
                "~/Content/cssLogin/css/font-awesome.css",
                "~/Content/cssLogin/css/style.css"));

            bundles.Add(new ScriptBundle("~/bundles/template").Include(
                      "~/Scripts/js/AdminLTE/app.js",
                      "~/Scripts/js/AdminLTE/calculatrice.js"));

            bundles.Add(new ScriptBundle("~/bundles/js").Include(
                      "~/Scripts/js/bootstrap.min.js"));
           
        }
    }
}
