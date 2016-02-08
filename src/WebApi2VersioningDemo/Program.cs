namespace WebApi2VersioningDemo
{
    using Microsoft.Owin.Hosting;

    public class Program
    {
        public static void Main(string[] args)
        {
            var baseAddress = string.Format("http://localhost:{0}/", 9000);

            using (WebApp.Start<Startup>(url: baseAddress))
            {
                while (true)
                {

                }
            }
        }
    }
}
