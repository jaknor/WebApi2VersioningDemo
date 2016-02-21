using System;
using System.Collections.Generic;
using System.Web.Http.Routing;

namespace WebApi2VersioningDemo.Versioning
{
    internal class VersionedRoute : RouteFactoryAttribute
    {
        public VersionedRoute(string template, int allowedVersion, int startDate, string name = null)
            : base(template)
        {
            AllowedVersion = allowedVersion;
            StartDate = startDate;
            SetName(name);
        }

        public VersionedRoute(string template, int allowedVersion, int startDate, int endDate, string name = null)
    : base(template)
        {
            AllowedVersion = allowedVersion;
            StartDate = startDate;
            EndDate = endDate;
            SetName(name);
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

        private void SetName(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                Name = name + "V" + AllowedVersion;
            }
        }
    }
}
