using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;
using System.Web.Http.Routing;

namespace WebApi2VersioningDemo.Versioning
{
    internal class VersionConstraint : IHttpRouteConstraint
    {
        public const string VersionName = "api-version";

        public const string VersionNameDate = "api-version-date";

        private const int DefaultVersion = 1;

        public VersionConstraint(int allowedVersion, int startDate, int? endDate)
        {
            AllowedVersion = allowedVersion;
            StartDate = startDate;
            EndDate = endDate;
        }

        public int AllowedVersion
        {
            get;
            private set;
        }

        public int StartDate
        {
            get;
            private set;
        }

        public int? EndDate
        {
            get;
            private set;
        }

        public bool Match(HttpRequestMessage request, IHttpRoute route, string parameterName, IDictionary<string, object> values, HttpRouteDirection routeDirection)
        {
            if (routeDirection == HttpRouteDirection.UriResolution)
            {
                var versionResult = GetVersionFromCustomContentType(request);

                if (versionResult.Version == null)
                {
                    versionResult = GetVersionFromCustomHeader(request);
                }

                if (versionResult.Version == null)
                {
                    versionResult = GetVersionFromQueryParameter(request);
                }

                if (versionResult.Version == null)
                {
                    versionResult = new VersionResult { Version = DefaultVersion };
                }

                if (versionResult.IsDateVersion)
                {
                    if (EndDate.HasValue)
                    {
                        return versionResult.Version >= StartDate && versionResult.Version < EndDate.Value;
                    }

                    return versionResult.Version >= StartDate;
                }
                else
                {
                    return (versionResult.Version == AllowedVersion);
                }
            }

            return true;
        }

        private VersionResult GetVersionFromQueryParameter(HttpRequestMessage request)
        {
            var queryKeyValuePairs = request.GetQueryNameValuePairs();

            var result = GetVersionFromQueryParameter(queryKeyValuePairs, VersionName);

            if (!result.Version.HasValue)
            {
                result = GetVersionFromQueryParameter(queryKeyValuePairs, VersionNameDate);
            }

            return result;
        }

        private VersionResult GetVersionFromQueryParameter(IEnumerable<KeyValuePair<string, string>> queryParameters, string parameterName)
        {
            var versionResult = new VersionResult
            {
                IsDateVersion = parameterName == VersionNameDate
            };

            var versionQueryParameter = queryParameters.FirstOrDefault(x => x.Key == parameterName);
            if (!versionQueryParameter.Equals(default(KeyValuePair<string, string>)))
            {
                int version;
                if (int.TryParse(versionQueryParameter.Value, out version))
                {
                    versionResult.Version = version;
                }
            }

            return versionResult;
        }

        private VersionResult GetVersionFromCustomHeader(HttpRequestMessage request)
        {
            var headers = request.Headers;

            var result = GetVersionFromCustomHeader(headers, VersionName);

            if (!result.Version.HasValue)
            {
                result = GetVersionFromCustomHeader(headers, VersionNameDate);
            }

            return result;
        }

        private VersionResult GetVersionFromCustomHeader(HttpRequestHeaders requestHeaders, string parameterName)
        {
            var versionResult = new VersionResult
            {
                IsDateVersion = parameterName == VersionNameDate
            };

            IEnumerable<string> headerValues;
            string versionAsString;

            if (requestHeaders.TryGetValues(parameterName, out headerValues) && headerValues.Count() == 1)
            {
                versionAsString = headerValues.First();
            }
            else
            {
                return versionResult;
            }

            int version;
            if (versionAsString != null && Int32.TryParse(versionAsString, out version))
            {
                versionResult.Version = version;
            }

            return versionResult;
        }

        private VersionResult GetVersionFromCustomContentType(HttpRequestMessage request)
        {
            var mediaTypes = request.Headers.Accept.Select(x => x.MediaType);

            var result = GetVersionFromCustomContentType(mediaTypes, VersionName);

            if (!result.Version.HasValue)
            {
                result = GetVersionFromCustomContentType(mediaTypes, VersionNameDate);
            }

            return result;
        }

        private VersionResult GetVersionFromCustomContentType(IEnumerable<string> mediaTypes, string parameterName)
        {
            var versionResult = new VersionResult
            {
                IsDateVersion = parameterName == VersionNameDate
            };

            string matchingMediaType = null;

            var regEx = new Regex(string.Format(@"application\/vnd\.moonpig\.{0}\.([\d]+)\+json", parameterName));

            foreach (var mediaType in mediaTypes)
            {
                if (regEx.IsMatch(mediaType))
                {
                    matchingMediaType = mediaType;
                    break;
                }
            }

            if (matchingMediaType == null)
            {
                return versionResult;
            }

            Match m = regEx.Match(matchingMediaType);
            string versionAsString = null;
            versionAsString = m.Groups[1].Value;

            int version;
            if (versionAsString != null && int.TryParse(versionAsString, out version))
            {
                versionResult.Version = version;
            }

            return versionResult;
        }
    }

    class VersionResult
    {
        public bool IsDateVersion { get; set; }
        public int? Version { get; set; }
    }
}
