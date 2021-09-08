using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using WebSpiderDocs.Providers;
using WebSpiderDocs.Repos;

[assembly: OwinStartup(typeof(WebSpiderDocs.Startup))]
namespace WebSpiderDocs
{
    public class Startup
    {

        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();

            ConfigureOAuth(app);

            WebApiConfig.Register(config);
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            app.UseWebApi(config);


		}

        public void ConfigureOAuth(IAppBuilder app)
        {
            OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                Provider = new AuthorizationServerProvider()
			};

			// Token Generation
			//app.UseOAuthBearerTokens(OAuthServerOptions);

			app.UseOAuthAuthorizationServer(OAuthServerOptions);
			app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
			//app.UseOAuthBearerTokens(OAuthServerOptions);


		}
    }
}