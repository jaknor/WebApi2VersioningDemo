namespace WebApi2VersioningDemo.Versioning
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Net.Http;
    using System.Web.Http.Routing;

    public static class UrlHelperExtension
    {
        public static string Link(HttpRequestMessage httpRequest, string routeName, int version, object routeValues)
        {
            var helper = new UrlHelper(httpRequest);

            routeName = routeName + "V" + version;

            if(routeValues == null)
            {
                routeValues = new object { };
            }
                        
            routeValues = routeValues.AddProperty("api-version", version);

            return helper.Link(routeName, routeValues);
        }
    }

    public static class ObjectExtensions
    {
        public static IDictionary<string, object> AddProperty(this object obj, string name, object value)
        {
            var dictionary = obj.ToDictionary();
            dictionary.Add(name, value);
            return dictionary;
        }

        // helper
        public static IDictionary<string, object> ToDictionary(this object obj)
        {
            IDictionary<string, object> result = new Dictionary<string, object>();
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(obj);
            foreach (PropertyDescriptor property in properties)
            {
                result.Add(property.Name, property.GetValue(obj));
            }
            return result;
        }
    }
}
