
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Facebook;

[assembly: OwinStartup(typeof(NewVersion.startup))]

namespace NewVersion
{
    public class startup
    {

        public void Configuration(IAppBuilder app)
        {
            // First, configure cookie authentication
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = "ApplicationCookie",
                LoginPath = new PathString("/login.aspx")
            });

            // Then configure external authentication providers like Facebook
            var facebookOptions = new FacebookAuthenticationOptions
            {
                AppId = "1006880521081390",
                AppSecret = "fd043b57a95f9134029db817a424989e",
                SignInAsAuthenticationType = "ApplicationCookie", // Make sure this matches the cookie authentication type
                CallbackPath = new PathString("/css/Home.aspx")
            };

            app.UseFacebookAuthentication(facebookOptions);
        }
    }

}
