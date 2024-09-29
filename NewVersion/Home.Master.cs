
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
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
            string currentPage = Request.Url.AbsolutePath.ToLower();

            // Check if the current page is not Home2.aspx or the login page
            if (!IsUserMember() && !currentPage.Contains("home2.aspx") && !currentPage.Contains("login.aspx"))
            {
                // Redirect to the login page
                string loginPageUrl = "../css/login.aspx"; // Adjust the path as necessary

                // Display alert and redirect to the login page
                string script = $@"
            alert('You Cannot Access this page!'); 
            window.location='{loginPageUrl}';
        ";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", script, true);
            }

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

        protected bool IsUserMember()
        {
            string currentUser = HttpContext.Current.User.Identity.Name; // Get the currently logged-in user's username
            string connString = ConfigurationManager.ConnectionStrings["productConnectionString"].ConnectionString;
            bool isMember = false;

            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();

                // Query to check if the user exists in the SuperAdmin table
                string queryMember = "SELECT COUNT(1) FROM MemberUser WHERE Username = @Username";
                using (SqlCommand cmdMember = new SqlCommand(queryMember, conn))
                {
                    cmdMember.Parameters.AddWithValue("@Username", currentUser);

                    int count = (int)cmdMember.ExecuteScalar();
                    isMember = count > 0; // If the user exists in SuperAdmin table, treat them as superadmin
                }
            }

            return isMember;
        }

    }
}