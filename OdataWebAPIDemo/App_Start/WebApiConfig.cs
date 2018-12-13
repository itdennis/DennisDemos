using System.Web.Http;
using System.Web.OData.Builder;
using System.Web.OData.Extensions;

namespace OdataWebAPIDemo
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;
            ODataModelBuilder builder = new ODataConventionModelBuilder();
            config.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
        }
    }
}
