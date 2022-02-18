using System.Web;
using System.Web.Optimization;

namespace EnterpriseArchitecture.WEB
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new Bundle("~/bundles/mybundle").Include(
                "~/Assets/External/jQuery/jquery-{version}.js",
                "~/Assets/External/Modernizr/modernizr-*",
                "~/Assets/External/Bootstrap/Scripts/bootstrap.js"
                ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Assets/External/Bootstrap/Styles/bootstrap.css",
                "~/Assets/External/FontAwesome/css/font-awesome.css"
                ));
        }
    }
}