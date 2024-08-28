using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NewVersion.css
{
    public partial class facebooklogin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var authResult = HttpContext.Current.GetOwinContext().Authentication.AuthenticateAsync("ExternalCookie").Result;
            if (authResult != null)
            {
                var claimsIdentity = authResult.Identity;

                // Iterate through claims to debug
                foreach (var claim in claimsIdentity.Claims)
                {
                    Debug.WriteLine($"Claim Type: {claim.Type}, Value: {claim.Value}");
                }

                // Retrieve claims
                var userName = claimsIdentity.FindFirst(ClaimTypes.Name)?.Value;
                var facebookId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                // Use these values as needed
                Response.Write($"Name: {userName}, Facebook ID: {facebookId}");
            }
        }
    }
}