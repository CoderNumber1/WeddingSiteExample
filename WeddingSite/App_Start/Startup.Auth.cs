using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Facebook;
using Microsoft.Owin.Security.Google;
using Owin;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WeddingSite
{
    public partial class Startup
    {
        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            // Enable the application to use a cookie to store information for the signed in user
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login")
            });
            // Use a cookie to temporarily store information about a user logging in with a third party login provider
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            // Uncomment the following lines to enable logging in with third party login providers
            //app.UseMicrosoftAccountAuthentication(
            //    clientId: "",
            //    clientSecret: "");

            //app.UseTwitterAuthentication(
            //   consumerKey: "",
            //   consumerSecret: "");

            //app.UseFacebookAuthentication(new Microsoft.Owin.Security.Facebook.FacebookAuthenticationOptions()
            //{
            //    AppId = "",
            //    AppSecret = "",

            //});

            var facebook = new FacebookAuthenticationOptions()
            {
                AppId = "",
                AppSecret = ""
                ,
                Provider = new FacebookAuthenticationProvider()
                {
                    OnAuthenticated = context =>
                    {
                        if (context.Email != null)
                        {
                            context.Identity.AddClaim(new System.Security.Claims.Claim(ClaimTypes.Email, context.Email));
                        }

                        context.Identity.AddClaim(new System.Security.Claims.Claim("FacebookAccessToken", context.AccessToken));
                        foreach (var claim in context.User)
                        {
                            var claimType = claim.Key;// string.Format("urn:facebook:{0}", claim.Key);
                            string claimValue = claim.Value.ToString();
                            if (!context.Identity.HasClaim(claimType, claimValue))
                                context.Identity.AddClaim(new System.Security.Claims.Claim(claimType, claimValue, "XmlSchemaString", "Facebook"));

                        }

                        return Task.FromResult(0);
                    }
                }
            };
            facebook.Scope.Add("email");

            app.UseFacebookAuthentication(facebook);

            var google = new GoogleOAuth2AuthenticationOptions()
            {
                ClientId = "",
                ClientSecret = "",
                Provider = new GoogleOAuth2AuthenticationProvider()
            };
            google.Scope.Add("email");

            app.UseGoogleAuthentication(google);
        }
    }
}