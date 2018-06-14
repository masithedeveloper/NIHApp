using System.Web.Optimization;

namespace NIHApp.RestApi
{
	public class BundleConfig
	{
		// For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
		public static void RegisterBundles(BundleCollection bundles)
		{
			bundles.Add(new ScriptBundle("~/bundles/jquery").Include("~/Views/Scripts/jquery-{version}.js"));
			bundles.Add(new ScriptBundle("~/bundles/modernizr").Include("~/Views/Scripts/modernizr-*"));
			bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include("~/Views/Scripts/bootstrap.js", "~/Views/Scripts/respond.js"));        
			bundles.Add(new StyleBundle("~/Content/css").Include("~/Areas/Content/bootstrap.css", "~/Content/site.css"));
		}
	}
}