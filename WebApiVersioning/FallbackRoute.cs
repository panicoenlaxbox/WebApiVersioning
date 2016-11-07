using System.Collections.Generic;
using System.Linq;

namespace WebApiVersioning
{
    internal class FallbackRoute
    {
        public FallbackRoute(string routeTemplate, int allowedVersion)
        {
            RouteTemplate = routeTemplate;
            AllowedVersion = allowedVersion;
            FallbackVersions = new List<int>();
        }
        public string RouteTemplate { get; }
        public int AllowedVersion { get; }
        public IEnumerable<int> FallbackVersions { get; }

        public bool HasFallbackVersion(int version)
        {
            return FallbackVersions.Contains(version);
        }

        public void AddFallbackVersion(int version)
        {
            ((IList<int>)FallbackVersions).Add(version);
        }
    }
}