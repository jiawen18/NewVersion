using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NewVersion
{
    public partial class Admin : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Set the SiteMapProvider
                AdminBreadcrumbDataSource.Provider = SiteMap.Providers["AdminSiteMapProvider"];

                // Load the user details
                LoadUserDetails();
            }
        }

        // Method to get the profile picture URL for the current user
        protected string GetProfilePictureUrl()
        {
            string username = HttpContext.Current.User.Identity.Name;
            string profilePictureUrl = "~/admin/assets/img/default.jpg"; // Default image path

            // Connect to the database and retrieve the profile picture URL
            string connString = ConfigurationManager.ConnectionStrings["productConnectionString"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                string query = "SELECT ProfilePicture FROM AdminUser WHERE Username = @Username";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Username", username);
                    object result = cmd.ExecuteScalar();

                    if (result != null)
                    {
                        profilePictureUrl = "assets/img/" + result.ToString(); // Append the profile picture filename
                    }
                }
            }
            return profilePictureUrl; // Return the URL
        }

        // Method to load the current user's details (username and email)
        private void LoadUserDetails()
        {
            string username = HttpContext.Current.User.Identity.Name;

            // Connect to the database and retrieve the username and email
            string connString = ConfigurationManager.ConnectionStrings["productConnectionString"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                string query = "SELECT Username, Email FROM AdminUser WHERE Username = @Username";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Username", username);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Populate the textboxes with the username and email
                            //lbl_user_name.Text = reader["Username"].ToString();
                            //lbl_user_email.Text = reader["Email"].ToString();
                        }
                    }
                }
            }
        }
    }
}