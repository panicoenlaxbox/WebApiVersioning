using System.Collections.Generic;
using System.Web.Http.Routing;

namespace WebApiVersioning
{
    class VersionedRoute : RouteFactoryAttribute
    {
        private const int DefaultVersion = Versioning.CurrentVersion;

        public VersionedRoute(string template)
         : this(template, DefaultVersion)
        {
        }

        public VersionedRoute(string template, int allowedVersion)
            : base(template)
        {
            AllowedVersion = allowedVersion;
        }

        public int AllowedVersion { get; }

        public override IDictionary<string, object> Constraints => new HttpRouteValueDictionary
        {
            { "version", new VersionConstraint(AllowedVersion, DefaultVersion) }
        };
    }
}