using System.Collections.Generic;
using System.Web.Http.Routing;

namespace WebApi2VersioningDemo.Versioning
{
    internal class VersionedRoute : RouteFactoryAttribute
    {
        public VersionedRoute(string template, int allowedVersion, int startDate)
            : base(template)
        {
            AllowedVersion = allowedVersion;
            StartDate = startDate;
        }

        public VersionedRoute(string template, int allowedVersion, int startDate, int endDate)
    : base(template)
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

        public override IDictionary<string, object> Constraints
        {
            get
            {
                var constraints = new HttpRouteValueDictionary();
                constraints.Add("v", new VersionConstraint(AllowedVersion, StartDate, EndDate));
                return constraints;
            }
        }
    }
}
