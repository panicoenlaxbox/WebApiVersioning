using System.Collections.Generic;
using System.Linq;

namespace WebApiVersioning
{
    internal class FallbackRoute
    {
        public string RouteTemplate { get; set; }
        public int AllowedVersion { get; set; }
        public IEnumerable<int> FallbackVersions { get; set; }

        public bool HasFallbackVersion(int version)
        {
            return FallbackVersions.Contains(version);
        }
    }
}