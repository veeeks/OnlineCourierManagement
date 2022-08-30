using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security.OAuth;
using System.Security.Claims;

namespace CourierManagement.API.Providers
{
    public class OAuthProvider : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            await Task.Run(() => context.Validated());
            // return base.ValidateClientAuthentication(context);
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            context.OwinContext.Response.Headers.Add("Allow-Control-Allow-Origin", new[] { "*" });
            using (AuthenticationRepository repo = new AuthenticationRepository())
            {
                var user = await repo.FindUser(context.UserName, context.Password);
                if (user == null)
                {
                    context.SetError("Invalid user", "Email or password incorrect");
                    return;
                }
                var identity = new ClaimsIdentity(context.Options.AuthenticationType);
                identity.AddClaim(new Claim("User", context.UserName));
                identity.AddClaim(new Claim("Role", "User"));
                context.Validated(identity);
            }
        }
    }
}