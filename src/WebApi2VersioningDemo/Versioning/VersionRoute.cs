using System.Collections.Generic;
using System.Web.Http.Routing;

namespace WebApi2VersioningDemo.Versioning
{
    internal class VersionedRoute : RouteFactoryAttribute
    {
        public VersionedRoute(string template, int allowedVersion)
            : base(template)
        {
            AllowedVersion = allowedVersion;
        }

        public int AllowedVersion
        {
            get;
            private set;
        }

        public override IDictionary<string, object> Constraints
        {
            get
            {
                var constraints = new HttpRouteValueDictionary();
                constraints.Add("v", new VersionConstraint(AllowedVersion));
                return constraints;
            }
        }
    }
}
