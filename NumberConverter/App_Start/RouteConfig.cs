using System.Web.Mvc;
using System.Web.Routing;

namespace NumberConverter
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "NumberToTextConversionRoute",
                url: "NumberToTextConversion",
                defaults: new { controller = "Conversion", action = "Conversion" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
