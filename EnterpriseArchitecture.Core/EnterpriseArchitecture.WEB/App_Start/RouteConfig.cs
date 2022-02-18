using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace EnterpriseArchitecture.WEB
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "PostMore",
                url: "site/PostMore",
                defaults: new { controller = "Site", action = "PostMore" }
            );

            routes.MapRoute(
                name: "Default3",
                url: "PostImageView/{id}/{w}/{h}",
                defaults: new { controller = "Site", action = "PostImageView", id = UrlParameter.Optional, w = UrlParameter.Optional, h = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default2",
                url: "ProfileImageView/{id}/{w}/{h}",
                defaults: new { controller = "Site", action = "ProfileImageView", id = UrlParameter.Optional, w = UrlParameter.Optional, h = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "PostList",
                url: "{categoryName}-{categoryID}",
                defaults: new { controller = "Site", action = "PostList" }
            );

            routes.MapRoute(
                name: "PostDetail",
                url: "{categoryName}/{slug}",
                defaults: new { controller = "Site", action = "PostDetail" }
            );

            routes.MapRoute(
                name: "Default",
                url: "admin/{controller}/{action}/{id}",
                defaults: new { controller = "Site", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
             name: "Default5",
             url: "{controller}/{action}/{id}",
             defaults: new { controller = "Site", action = "Index", id = UrlParameter.Optional }
             );
        }
    }
}