
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NewVersion
{
    public partial class Site1 : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void LogoutUser(object sender, EventArgs e)
        {

            // Log out of FormsAuthentication
            System.Web.Security.FormsAuthentication.SignOut();

            // Redirect to login page
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Successfully Log Out!'); window.location='Home2.aspx';", true);
        }

        protected void checkAuthAndRedirect()
        {

            if (!HttpContext.Current.User.Identity.IsAuthenticated)
            {
                // Register the alert and redirect script
                string script = "alert('You need to log in to access the cart.'); window.location.href='login.aspx';";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertRedirect", script, true);
            }
            else
            {
                // Redirect to the cart if authenticated
                Response.Redirect("cart.aspx");
            }
        }

        protected void btnCart_Click(object sender, EventArgs e)
        {
            checkAuthAndRedirect();
        }

    }
}