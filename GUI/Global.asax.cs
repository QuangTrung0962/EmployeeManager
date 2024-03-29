﻿using GUI.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using WebGrease.Configuration;

namespace GUI
{
    public class MvcApplication : System.Web.HttpApplication
	{
		protected void Application_Start()
		{
            log4net.Config.XmlConfigurator.Configure();

            AreaRegistration.RegisterAllAreas();
            //Config DI
            UnityConfig.RegisterComponents();
            //Config API
            GlobalConfiguration.Configure(WebApiConfig.Register);

           FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			RouteConfig.RegisterRoutes(RouteTable.Routes);
			BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
	}
}
