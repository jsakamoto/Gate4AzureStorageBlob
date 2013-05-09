using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Toolbelt.Web;

namespace Gate4AzureStorageBlob
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            RegisterRoutes(RouteTable.Routes);

            AppOfflineModule.Filter.IsEnable = () =>
                HttpContext.Current.Request.IsSecureConnection == false;
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Content",
                url: "{action}/{account}/{container}/{name}",
                defaults: new { controller = "Gate4Blob" });
        }
    }
}