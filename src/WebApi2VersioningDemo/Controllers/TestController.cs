namespace WebApi2VersioningDemo.Controllers
{
    using System.Web.Http;

    [RoutePrefix("api")]
    public class TestController : ApiController
    {
        [HttpGet]
        [Route("Test")]
        public string Test()
        {
            return "The api is working";
        }
    }
}
