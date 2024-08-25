using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NewVersion.css
{
    public partial class Home : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {


            if (User.Identity.IsAuthenticated)
            {
                hlLogin.Visible = false;  // Hide the login link
                hlProfile.Visible = true; // Show the profile link
            }
        }

        protected void btnBuyNow_Click(object sender, EventArgs e)
        {
            Response.Redirect("ProductDetails.aspx");
        }
    }
}