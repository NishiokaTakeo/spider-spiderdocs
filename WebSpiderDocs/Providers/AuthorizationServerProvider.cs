using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using NLog;
using SpiderDocsModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using WebSpiderDocs.Repos;

namespace WebSpiderDocs.Providers
{
    public class AuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        static Logger logger = LogManager.GetCurrentClassLogger();

        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
			////string clientId = string.Empty, clientSecret = string.Empty;
			////context.TryGetBasicCredentials(out clientId, out clientSecret);

			//	// Resource owner password credentials does not provide a client ID.
			//if (context.ClientId == null)
			//{
			//	// Validate Authoorization.
			//	context.Validated();
			//}

			//// Return info.
			//await Task.Run(() => { });
			//return;
			context.OwinContext.Response.Headers.Remove("Access-Control-Allow-Origin");

            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });

            await Task.Run(() =>
            {
                string clientId = string.Empty, clientSecret = string.Empty;
                if (!context.TryGetBasicCredentials(out clientId, out clientSecret))
                //if (!context.TryGetFormCredentials(out clientId, out clientSecret))

                {
                    logger.Info("Login attempted but invalid {0}", clientId);
                    logger.Debug("Login attempted but invalid {0}:{1}", clientId, clientSecret);

                    context.SetError("invalid_client", "Client credentials could not be retrieved through the Authorization header.");
                    context.Rejected();
                    return;
                }

                try
                {
                    User user = UserController.LoginByMD5(clientId, clientSecret);

                    if (user.id > 0)
                    {
                        // Client has been verified.
                        context.OwinContext.Set<ApplicationUser>("oauth:client", new ApplicationUser
                        {
                            Id = user.login,
                            UserName = user.login,
                            ClientSecretHash = clientSecret
                        });

                        context.Validated(clientId);
                        //context.Parameters

                    }

                }
                catch (Exception ex)
                {

                    logger.Error(ex);
                    context.Rejected();
                }
            });

        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {

			//User user = UserController.LoginByMD5(context.UserName, context.Password);

			//if (user.id > 0)
			//{
			//	// // Client has been verified.
			//	context.OwinContext.Set<ApplicationUser>("oauth:client", new ApplicationUser
			//	{
			//		Id = user.login,
			//		UserName = user.login,
			//		ClientSecretHash = context.Password
			//	});

			//	//context.Validated(context.UserName);
			//	//context.Parameters


			//	//ApplicationUser client = context.OwinContext.Get<ApplicationUser>("oauth:client");
			//	ClaimsIdentity identity = new ClaimsIdentity(context.Options.AuthenticationType);
			//	identity.AddClaim(new Claim("sub", user.login));
			//	identity.AddClaim(new Claim("role", "user"));

			//	context.Validated(identity);

			//	//// Initialization.
			//	//var claims = new List<Claim>();
			//	//var userInfo = user;

			//	//// Setting
			//	//claims.Add(new Claim(ClaimTypes.Name, userInfo.login));

			//	//// Setting Claim Identities for OAUTH 2 protocol.
			//	//ClaimsIdentity oAuthClaimIdentity = new ClaimsIdentity(claims, OAuthDefaults.AuthenticationType);
			//	//ClaimsIdentity cookiesClaimIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationType);

			//	//// Setting user authentication.
			//	//AuthenticationProperties properties = CreateProperties(userInfo.login);
			//	//AuthenticationTicket ticket = new AuthenticationTicket(oAuthClaimIdentity, properties);

			//	//// Grant access to authorize user.
			//	//context.Validated(ticket);
			//	//context.Request.Context.Authentication.SignIn(cookiesClaimIdentity);

			//}
			//else
			//{

			//             if ( user == null || user.id == 0)
			//             {
			//                context.SetError("invalid_grant", "The user name or password is incorrect.");
			//                logger.Info("Login was attempted, but failed. : {0}", context.UserName);

			//                return;
			//             }
			//}

			//return;

			await Task.Run(() =>
			{

				context.OwinContext.Response.Headers.Remove("Access-Control-Allow-Origin");

				context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });

				ApplicationUser client = context.OwinContext.Get<ApplicationUser>("oauth:client");

				if (!string.IsNullOrEmpty(client.Id))
				{
					ClaimsIdentity identity = new ClaimsIdentity(context.Options.AuthenticationType);
					identity.AddClaim(new Claim("sub", client.UserName));
					identity.AddClaim(new Claim("role", "user"));

					context.Validated(identity);
				}
				else
				{
					context.Rejected();
				}

				return;
				// This is oribinal which worked
				//// Client flow matches the requested flow. Continue
				//User user = UserController.LoginByMD5(context.UserName, context.Password);
				//ApplicationUser client = context.OwinContext.Get<ApplicationUser>("oauth:client");

				//if ( user == null || user.id == 0)
				//{
				//    context.SetError("invalid_grant", "The user name or password is incorrect.");
				//    logger.Info("Login was attempted, but failed. : {0}", context.UserName);

				//    return;
				//}

				//if (client.Id == context.UserName && client.ClientSecretHash == context.Password)
				//{
				//    ClaimsIdentity identity = new ClaimsIdentity(context.Options.AuthenticationType);
				//    identity.AddClaim(new Claim("sub", context.UserName));
				//    identity.AddClaim(new Claim("role", "user"));

				//    context.Validated(identity);

				//}
				//else
				//{
				//    context.Rejected();

				//}
			});
		}

		public static AuthenticationProperties CreateProperties(string userName)
		{
			// Settings.
			IDictionary<string, string> data = new Dictionary<string, string>
											   {
												   { "login", userName }
											   };

			// Return info.
			return new AuthenticationProperties(data);
		}
	}
}