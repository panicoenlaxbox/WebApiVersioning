using System.Collections.Generic;
using System.Web.Http.Routing;

namespace WebApiVersioning
{
    class VersionedRoute : RouteFactoryAttribute
    {
        private readonly int _allowedVersion;
        private const int DefaultVersion = 1;
        private string _template;

        public VersionedRoute(string template)
         : this(template, DefaultVersion)
        {
        }

        public VersionedRoute(string template, int allowedVersion)
            : base(template)
        {
            _template = template;
            _allowedVersion = allowedVersion;
        }

        public override IDictionary<string, object> Constraints => new HttpRouteValueDictionary
        {
            { "version", new VersionConstraint(_allowedVersion, DefaultVersion) }
        };
    }
}