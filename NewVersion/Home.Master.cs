
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
            Response.Redirect("login.aspx");
        }
    }
}