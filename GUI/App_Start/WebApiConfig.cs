using System.Web.Http;
using System.Web.Mvc;

namespace GUI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "Api",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = UrlParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "test",
                routeTemplate: "api/{controller}/{searchString}",
                defaults: new { controller = "TestApi", searchString = RouteParameter.Optional }
            );

        }
    }
}
