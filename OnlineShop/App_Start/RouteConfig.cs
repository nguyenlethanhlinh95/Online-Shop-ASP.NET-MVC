using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace OnlineShop
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //Dang nhap
            routes.MapRoute(
                 name: "Dang nhap",
                 url: "dang-nhap",
                 defaults: new
                 {
                     controller = "User",
                     action = "Login",
                     id =
                UrlParameter.Optional
                 },
                 namespaces: new[] { "OnlineShop.Controllers" }
             );

            routes.IgnoreRoute("{*botdetect}", new { botdetect = @"(.*)BotDetectCaptcha\.ashx" });
            //Register
            routes.MapRoute(
             name: "Dang ky thanh vien",
             url: "dang-ky",
             defaults: new
             {
                 controller = "User",
                 action = "Register",
                 id =
            UrlParameter.Optional
             },
             namespaces: new[] { "OnlineShop.Controllers" }
             );

            //About
            routes.MapRoute(
                 name: "Gioi thieu",
                 url: "gioi-thieu",
                 defaults: new
                 {
                     controller = "About",
                     action = "index",
                     id = UrlParameter.Optional
                 },
                 namespaces: new[] { "BanHangOnline.Controllers" }
             );

            //Contact
            routes.MapRoute(
                 name: "Lien he",
                 url: "lien-he",
                 defaults: new
                 {
                     controller = "Contact",
                     action = "index",
                     id = UrlParameter.Optional
                 },
                 namespaces: new[] { "BanHangOnline.Controllers" }
             );

            //Product detail
            routes.MapRoute(
                 name: "Product",
                 url: "chi-tiet/{metatitle}-{id}",
                 defaults: new
                 {
                     controller = "Product",
                     action = "Detail",
                     id = UrlParameter.Optional
                 },
                 namespaces: new[] { "BanHangOnline.Controllers" }
             );

            //Product category
            routes.MapRoute(
                name: "Category",
                url: "san-pham/{metatitle}-{id}",
                defaults: new
                {
                    controller = "Product",
                    action = "Index",
                    id = UrlParameter.Optional
                },
                namespaces: new[] { "BanHangOnline.Controllers" }
            );

            //Add cart
            routes.MapRoute(
                name: "Add Cart",
                url: "them-gio-hang",
                defaults: new
                {
                    controller = "Cart",
                    action = "AddItem",
                    id = UrlParameter.Optional
                },
                namespaces: new[] { "BanHangOnline.Controllers" }
            );

            //Add cart
            routes.MapRoute(
                name: "Cart",
                url: "gio-hang",
                defaults: new
                {
                    controller = "Cart",
                    action = "Index",
                    id = UrlParameter.Optional
                },
                namespaces: new[] { "BanHangOnline.Controllers" }
            );

            //Payment cart
            routes.MapRoute(
                name: "Payment",
                url: "thanh-toan",
                defaults: new
                {
                    controller = "Cart",
                    action = "Payment",
                    id = UrlParameter.Optional
                },
                namespaces: new[] { "BanHangOnline.Controllers" }
            );

            //Success cart
            routes.MapRoute(
                name: "Success",
                url: "hoan-thanh",
                defaults: new
                {
                    controller = "Cart",
                    action = "Success",
                    id = UrlParameter.Optional
                },
                namespaces: new[] { "BanHangOnline.Controllers" }
            );


            //Search
            routes.MapRoute(
                name: "Search",
                url: "tim-kiem",
                defaults: new
                {
                    controller = "Product",
                    action = "Search",
                    id = UrlParameter.Optional
                },
                namespaces: new[] { "BanHangOnline.Controllers" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional},
                namespaces: new[] { "OnlineShop.Controllers" }
            );

            
        }
    }
}
