using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using PayPal;
using PayPal.PayPalAPIInterfaceService;

namespace eLargesse
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            // Get the config properties from PayPal.Api.ConfigManager           
            //Dictionary<string, string> config = PayPal.Manager.ConfigManager.Instance.GetProperties();

            // Create the Classic SDK service instance to use.
            //PayPalAPIInterfaceServiceService service = new PayPalAPIInterfaceServiceService(config);

        }
    }
}