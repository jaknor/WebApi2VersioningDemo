namespace WebApi2VersioningDemo
{
    using Owin;
    using System.Web.Http;

    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();

            //var resolver = new DefaultInlineConstraintResolver();
            //resolver.ConstraintMap.Add("version", typeof(VersionConstraint)); resolver
            config.MapHttpAttributeRoutes();

            app.UseWebApi(config);
        }
    }
}
