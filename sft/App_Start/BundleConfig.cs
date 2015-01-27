using System.Web.Optimization;

namespace sft
{
  public class BundleConfig
  {
    // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862




    public static void RegisterBundles(BundleCollection bundles)
    {
#if DEBUG
      BundleTable.EnableOptimizations = false;
#endif

      bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                  "~/Scripts/jquery-{version}.js",
                  "~/Scripts/jquery.dropotron.min.js"));

      bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                  "~/Scripts/jquery.validate*"));

      bundles.Add(new ScriptBundle("~/bundles/skel").Include(
                  "~/Scripts/skel.min.js",
                  "~/Scripts/skel-layers.min.js",
                  "~/Scripts/init.js"
                    ));

      bundles.Add(new ScriptBundle("~/admin/bundles/metro-ui").Include(
          "~/Areas/Admin/Scripts/metro-ui/jquery.ui.widget.js",
          "~/Areas/Admin/Scripts/metro-ui/metro.min.js",
          "~/Areas/Admin/Scripts/DataTable.js",
          "~/Areas/Admin/Scripts/DataTables.colReorder.js"));

      // Use the development version of Modernizr to develop with and learn from. Then, when you're
      // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
      bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                  "~/Scripts/modernizr-*"));

      bundles.Add(new StyleBundle("~/admin/bundles/Content/css").Include(
             "~/Areas/Admin/Content/site.css"));

      bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/font-awesome.css"));

      bundles.Add(new StyleBundle("~/Content/css/desktop").Include(
                "~/Content/style-desktop.css"));

      bundles.Add(new StyleBundle("~/Content/css/1000").Include(
                "~/Content/style-1000px.css"));

      bundles.Add(new StyleBundle("~/Content/css/mobile").Include(
                "~/Content/style-mobile.css"));

      bundles.Add(new StyleBundle("~/admin/bundles/Content/fontswoff").Include(
    "~/Areas/Admin/Content/metro-ui/fonts/metroSysIcons.woff"
    ));
      bundles.Add(new StyleBundle("~/admin/bundles/Content/fontsttf").Include(
          "~/Areas/Admin/Content/metro-ui/fonts/metroSysIcons.ttf"
          ));
      bundles.Add(new StyleBundle("~/admin/bundles/Content/fontssvg").Include(
          "~/Areas/Admin/Content/metro-ui/fonts/metroSysIcons.svg"
          ));

      bundles.Add(new StyleBundle("~/admin/bundles/Content/fonts/iconFont.woff").Include(
"~/Areas/Admin/Content/metro-ui/fonts/inconFont.woff"
));
      bundles.Add(new StyleBundle("~/admin/bundles/Content/fonts/iconFont.ttf").Include(
          "~/Areas/Admin/Content/metro-ui/fonts/inconFont.ttf"
          ));
      bundles.Add(new StyleBundle("~/admin/bundles/Content/fonts/iconFont.svg").Include(
          "~/Areas/Admin/Content/metro-ui/fonts/inconFont.svg"
          ));

      // Set EnableOptimizations to false for debugging. For more information,
      // visit http://go.microsoft.com/fwlink/?LinkId=301862
      BundleTable.EnableOptimizations = true;
    }
  }
}
