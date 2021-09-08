using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;

namespace WebSpiderDocs
{
    public static class WebApiConfig {

        public static void Register(HttpConfiguration config)
        {

			// config.SuppressDefaultHostAuthentication();
			// config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));


			// Web API routes
			config.MapHttpAttributeRoutes();

            config.EnableCors();


            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

        }
	}
}
