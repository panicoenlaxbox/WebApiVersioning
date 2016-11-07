using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http.Routing;

namespace WebApiVersioning
{    
    class VersionConstraint : IHttpRouteConstraint
    {
        private readonly int _allowedVersion;
        private readonly int _defaultVersion;

        public VersionConstraint(int allowedVersion, int defaultVersion)
        {
            _allowedVersion = allowedVersion;
            _defaultVersion = defaultVersion;
        }

        public bool Match(HttpRequestMessage request, IHttpRoute route, string parameterName, IDictionary<string, object> values, HttpRouteDirection routeDirection)
        {
            if (routeDirection != HttpRouteDirection.UriResolution)
            {
                return false;
            }
            var version = GetVersionFromQueryString(request);
            if (version != null)
            {
                return version == _allowedVersion;
            }
            version = GetVersionFromHeaders(request);
            if (version != null)
            {
                return version == _allowedVersion;
            }
            version = GetVersionFromAcceptHeader(request);
            if (version != null)
            {
                return version == _allowedVersion;
            }
            version = GetVersionFromCustomAcceptHeader(request) ?? _defaultVersion;
            return version == _allowedVersion;
        }

        private static int? GetVersionFromCustomAcceptHeader(HttpRequestMessage request)
        {
            var accept = request.Headers.Accept
                .Where(p => p.MediaType.StartsWith("application/vnd.", StringComparison.OrdinalIgnoreCase))
                .SingleOrDefault(a => a.Parameters.Any(p => string.Equals(p.Name, "version", StringComparison.OrdinalIgnoreCase)));
            if (accept != null)
            {
                int version;
                if (int.TryParse(accept.Parameters.Single(p => string.Equals(p.Name, "version", StringComparison.OrdinalIgnoreCase)).Value, out version))
                {
                    return version;
                }
            }
            return null;
        }

        private static int? GetVersionFromAcceptHeader(HttpRequestMessage request)
        {
            var accept = request.Headers.Accept.SingleOrDefault(a => a.Parameters.Any(p => string.Equals(p.Name, "version", StringComparison.OrdinalIgnoreCase)));
            if (accept != null)
            {
                int version;
                if (int.TryParse(accept.Parameters.Single(p => string.Equals(p.Name, "version", StringComparison.OrdinalIgnoreCase)).Value, out version))
                {
                    return version;
                }
            }
            return null;
        }

        private static int? GetVersionFromHeaders(HttpRequestMessage request)
        {
            IEnumerable<string> headerValues;
            if (request.Headers.TryGetValues("x-version", out headerValues))
            {
                int version;
                if (int.TryParse(headerValues.First(), out version))
                {
                    return version;
                }
            }
            return null;
        }


        private static int? GetVersionFromQueryString(HttpRequestMessage request)
        {
            int version;
            if (int.TryParse(GetQueryStringValue(request, "version"), out version))
            {
                return version;
            }
            return null;
        }

        private static string GetQueryStringValue(HttpRequestMessage request, string key)
        {
            var values = request.GetQueryNameValuePairs();
            if (values.All(p => !string.Equals(p.Key, key, StringComparison.OrdinalIgnoreCase)))
            {
                return null;
            }
            return values.Single(p => string.Equals(p.Key, key, StringComparison.OrdinalIgnoreCase)).Value;
        }
    }
}