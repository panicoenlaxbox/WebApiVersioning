using System;
using System.Net.Http.Headers;
using System.Web.Http;
using Owin;
using WebApiVersioning.Models;

namespace WebApiVersioning
{
    class Startup
    {
        
        public void Configuration(IAppBuilder app)
        {
            app.UseWelcomePage("/welcome");

            var config = new HttpConfiguration();
            //config.MapHttpAttributeRoutes();
            //config.Routes.MapHttpRoute(
            //    name: "DefaultApi",
            //    routeTemplate: "api/{namespace}/{controller}/{id}",
            //    defaults: new { id = RouteParameter.Optional }
            //);
            //config.Services.Replace(typeof(IHttpControllerSelector), new NamespaceHttpControllerSelector(config));


            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            config.Formatters.Insert(0,
               new TypedXmlMediaTypeFormatter(typeof(Customer),
               new MediaTypeHeaderValue("application/vnd.example.com+xml")));
            config.Formatters.Insert(0,
                new TypedJsonMediaTypeFormatter(typeof(Customer),
                new MediaTypeHeaderValue("application/vnd.example.com+json")));
            config.Formatters.Insert(0,
               new TypedXmlMediaTypeFormatter(typeof(Customer2),
               new MediaTypeHeaderValue("application/vnd.example.com+xml")));
            config.Formatters.Insert(0,
                new TypedJsonMediaTypeFormatter(typeof(Customer2),
                new MediaTypeHeaderValue("application/vnd.example.com+json")));
            //config.Formatters.Add(
            //    new TypedXmlMediaTypeFormatter(typeof(Customer), 
            //    new MediaTypeHeaderValue("application/vnd.example.com.customer+xml")));
            //config.Formatters.Add(
            //    new TypedXmlMediaTypeFormatter(typeof(Customer),
            //    new MediaTypeHeaderValue("application/vnd.example.com.customer+json")));
            app.UseWebApi(config);
        }
    }
}