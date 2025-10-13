using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.UI;

namespace TNGLuong
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkID=303951
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/js").Include(
                            "~/vendor/jquery/jquery-3.2.1.min.js",
                            "~/vendor/bootstrap/js/popper.js",
                            "~/vendor/bootstrap/js/bootstrap.min.js",
                            "~/vendor/select2/select2.min.js",
                            "~/vendor/tilt/tilt.jquery.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/main").Include("~/js/main.js"));
            //bundles.Add(new ScriptBundle("~/bundles/menumain").Include("~/js/menu_main.js"));
            // Order is very important for these files to work, they have explicit dependencies
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                           "~/js/modernizr-*"));

            bundles.Add(new ScriptBundle("~/Adminbundles/js2").Include(
                            "~/WebAdmin/vendors/jquery/dist/jquery.min.js",
                            "~/WebAdmin/vendors/bootstrap/dist/js/bootstrap.bundle.min.js",
                            "~/WebAdmin/vendors/fastclick/lib/fastclick.js",
                            "~/WebAdmin/vendors/nprogress/nprogress.js",
                            "~/WebAdmin/vendors/malihu-custom-scrollbar-plugin/jquery.mCustomScrollbar.concat.min.js",
                            "~/WebAdmin/build/js/custom.min.js"));
        }
    }
}