using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;

namespace WebApiVersioning
{
    public class MyHttpControllerSelector : DefaultHttpControllerSelector
    {
        public MyHttpControllerSelector(HttpConfiguration configuration) : base(configuration)
        {
        }

        public override string GetControllerName(HttpRequestMessage request)
        {
            var controllerName = base.GetControllerName(request);
            return controllerName;
        }

        public override IDictionary<string, HttpControllerDescriptor> GetControllerMapping()
        {
            var controllerMapping = base.GetControllerMapping();
            return controllerMapping;
        }

        public override HttpControllerDescriptor SelectController(HttpRequestMessage request)
        {
            var controller = base.SelectController(request);
            return controller;
        }
    }
}