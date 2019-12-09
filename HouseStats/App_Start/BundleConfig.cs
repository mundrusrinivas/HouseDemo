using System.Web;
using System.Web.Optimization;

namespace HouseStats
{
    public class BundleConfig
    {
         public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/css").Include(
                        "~/Content/site.css"));

            bundles.Add(new ScriptBundle("~/bundles/HouseStats")
            .IncludeDirectory("~/Scripts/Controllers", "*.js")
            .Include("~/Scripts/HouseStats.js"));

           // BundleTable.EnableOptimizations = true;
        }
    }
}
