using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace WebApiVersioning
{
    internal class Versioning
    {
        public const int Current = 3;
        public static Lazy<IEnumerable<FallbackRoute>> FallbackRoutes;

        static Versioning()
        {
            FallbackRoutes = new Lazy<IEnumerable<FallbackRoute>>(GetFallbackRoutes);
        }

        private static IEnumerable<FallbackRoute> GetFallbackRoutes()
        {
            var fallbackRoutes = Assembly.GetExecutingAssembly().GetTypes()
                .SelectMany(t => t.GetMethods())
                .Where(m => m.GetCustomAttributes(typeof(VersionedRoute), false).Length > 0)
                .Select(m =>
                {
                    var route = m.GetCustomAttribute<VersionedRoute>();
                    return new FallbackRoute
                    {
                        RouteTemplate = route.Template,
                        AllowedVersion = route.AllowedVersion,
                        FallbackVersions = new List<int>()
                    };
                }).ToList();
            foreach (var routeTemplate in fallbackRoutes.Select(p => p.RouteTemplate).Distinct())
            {
                var lastFallbackRouteIndexFound = 0;
                for (var version = Current; version > 0; version--)
                {
                    if (fallbackRoutes.Any(MatchFallbackRoute(routeTemplate, version)))
                    {
                        lastFallbackRouteIndexFound = version;
                        continue;
                    }
                    ((IList<int>)fallbackRoutes.Single(MatchFallbackRoute(routeTemplate, lastFallbackRouteIndexFound)).FallbackVersions).Add(
                        version);
                }
            }
            return fallbackRoutes;
        }

        private static Func<FallbackRoute, bool> MatchFallbackRoute(string routeTemplate, int allowedVersion)
        {
            return f => f.RouteTemplate == routeTemplate && f.AllowedVersion == allowedVersion;
        }

        public static FallbackRoute GetFallbackRoute(string routeTemplate, int allowedVersion)
        {
            return FallbackRoutes.Value.SingleOrDefault(f => f.RouteTemplate == routeTemplate && f.AllowedVersion == allowedVersion);
        }
    }
}