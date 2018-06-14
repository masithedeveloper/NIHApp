using System;
using System.Net;
using System.Net.Http.Formatting;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using NHibernate.Burrow;
using StructureMap;

namespace NIHApp.RestApi
{
	public class WebApiApplication : HttpApplication
	{
		private static void ConfigureApi(HttpConfiguration config)
		{
			config.DependencyResolver = new StructureMapResolver(ObjectFactory.Container);
		}

		protected void Application_Start()
		{
			/*
			AreaRegistration.RegisterAllAreas();
			GlobalConfiguration.Configure(WebApiConfig.Register);
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			RouteConfig.RegisterRoutes(RouteTable.Routes);
			BundleConfig.RegisterBundles(BundleTable.Bundles);
			*/
			AreaRegistration.RegisterAllAreas();
			ServicePointManager.UseNagleAlgorithm = false;
			ServicePointManager.DefaultConnectionLimit = 1000;
			GlobalConfiguration.Configuration.Formatters.Clear();
			GlobalConfiguration.Configuration.Formatters.Add(new JsonMediaTypeFormatter());
			GlobalConfiguration.Configure(WebApiConfig.Register);
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			RouteConfig.RegisterRoutes(RouteTable.Routes);
			BundleConfig.RegisterBundles(BundleTable.Bundles);
			//log4net.Config.XmlConfigurator.Configure();
			StructureMapBootStrapper.Bootstrap();
			ConfigureApi(GlobalConfiguration.Configuration);
			ControllerBuilder.Current.SetControllerFactory(new StructureMapControllerFactory());


			/*

			 AreaRegistration.RegisterAllAreas();
			ServicePointManager.UseNagleAlgorithm = false;
			ServicePointManager.DefaultConnectionLimit = 1000;
			GlobalConfiguration.Configuration.Formatters.Clear();
			GlobalConfiguration.Configuration.Formatters.Add(new JsonMediaTypeFormatter());
			GlobalConfiguration.Configure(WebApiConfig.Register);
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			RouteConfig.RegisterRoutes(RouteTable.Routes);
			BundleConfig.RegisterBundles(BundleTable.Bundles);
			log4net.Config.XmlConfigurator.Configure();
			StructureMapBootStrapper.Bootstrap();
			ConfigureApi(GlobalConfiguration.Configuration); 



			AreaRegistration.RegisterAllAreas();
			WebApiConfig.Register(GlobalConfiguration.Configuration);
			RouteConfig.RegisterRoutes(RouteTable.Routes);
		  */
		}

		protected void Application_BeginRequest(object sender, EventArgs e)
		{
			//new BurrowFramework().InitWorkSpace();
		}

		protected void Application_EndRequest(object sender, EventArgs e)
		{
			ObjectFactory.ReleaseAndDisposeAllHttpScopedObjects();
		}
	}
}