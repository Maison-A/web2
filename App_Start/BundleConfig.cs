using System.Web.Optimization;

namespace web2 {
	public class BundleConfig {
		public static void RegisterBundles(BundleCollection bundles) {
			// allows you to make one line in layout page referenced by this line, to referenct as many files as you
			// want
			bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
						"~/Scripts/web2.js",
						"~/Scripts/modernizr-{version}.js",
						"~/Scripts/jquery.filedrop.js",
						"~/Scripts/jquery-{version}.js"));

			bundles.Add(new StyleBundle("~/Content/css").Include(
						"~/Content/site.css"));
		}
	}
}
