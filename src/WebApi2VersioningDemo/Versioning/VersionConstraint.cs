using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http.Routing;

namespace WebApi2VersioningDemo.Versioning
{
    internal class VersionConstraint : IHttpRouteConstraint
    {
        public const string VersionName = "api-version";

        private const int DefaultVersion = 1;

        public VersionConstraint(int allowedVersion)
        {
            AllowedVersion = allowedVersion;
        }

        public int AllowedVersion
        {
            get;
            private set;
        }

        public bool Match(HttpRequestMessage request, IHttpRoute route, string parameterName, IDictionary<string, object> values, HttpRouteDirection routeDirection)
        {
            if (routeDirection == HttpRouteDirection.UriResolution)
            {
                var version = GetVersionFromCustomHeader(request);

                if (!version.HasValue)
                {
                    version = DefaultVersion;
                }

                return (version == AllowedVersion);
            }

            return true;
        }

        private int? GetVersionFromCustomHeader(HttpRequestMessage request)
        {
            var headers = request.Headers;

            var result = GetVersionFromCustomHeader(headers, VersionName);

            return result;
        }

        private int? GetVersionFromCustomHeader(HttpRequestHeaders requestHeaders, string parameterName)
        {
            IEnumerable<string> headerValues;
            string versionAsString = null;

            if (requestHeaders.TryGetValues(parameterName, out headerValues) && headerValues.Count() == 1)
            {
                versionAsString = headerValues.First();
            }           

            int version;
            if (versionAsString != null && Int32.TryParse(versionAsString, out version))
            {
                return version;
            }

            return null;
        }
    }
}
