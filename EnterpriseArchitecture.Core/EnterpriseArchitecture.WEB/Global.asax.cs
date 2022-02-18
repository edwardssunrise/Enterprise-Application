using EnterpriseArchitecture.IOC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace EnterpriseArchitecture.WEB
{
    /// <summary>
    /// When the project runs for the first time, the program is configured using the information in this field.
    /// </summary>
    public class MvcApplication : System.Web.HttpApplication
    {
        /// <summary>
        /// In classic ASP.NET there are a handful of special function names defined in the 
        /// "global.asa" file that will be run at specified points throughout the application lifecycle:
        /// 
        /// 1. Application_OnStart()
        /// 2. Application_OnEnd()
        /// 3. Session_OnStart()
        /// 4. Session_OnEnd()
        /// 
        /// 1. Application_OnStart()
        /// The application runs only once when it receives the first HTTP Request and just before any
        /// .ASP files are processed.
        ///
        /// 2. Application_OnEnd()
        /// Runs once during application shutdown after all requests are processed.
        ///
        /// 3. Session_OnStart()
        /// Runs at the start of each unique user session. If a user/client has cookies disabled, 
        /// this will work for every request, as ASP never detects the session cookie that identifies
        /// an existing session.
        ///
        /// 4. Session_OnEnd()
        /// Runs whenever a user session expires.
        ///
        /// that run these methods in response to runtime-generated matching events in ASP.NET
        /// AutoEventWireup is available. It is the Page_Load method that automatically invokes the 
        /// Page class in response to trigger the Load event during the Page lifecycle.
        /// 
        /// public class Global : System.Web.HttpApplication{
        ///     protected void Application_Start(){}
        ///     protected void Application_Start(object sender, EventArgs e){}
        /// }
        /// </summary>
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            /* The Unity package installed in the IOC layer must be reinstalled in this area. */
            UnityConfig.RegisterComponents();
        }

        /// <summary>
        /// The Application_BeginRequest() method is executed on all requests handled by the ASP.NET runtime.
        ///
        /// This method is used to disable the browser cache. This method prevents returning to the 
        /// "/Login/Index" page after successful login.
        /// </summary>
        protected void Application_BeginRequest()
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.UtcNow.AddHours(-1));
            Response.Cache.SetNoStore();
        }
    }
}
