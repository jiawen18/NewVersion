using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NewVersion.css
{
    public partial class UserProfile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Load user details
                LoadUserDetails();
            }
        }

        private void LoadUserDetails()
        {
            string username = HttpContext.Current.User.Identity.Name;

            // Connect to the database and retrieve the username and email from MemberUser
            string connString = ConfigurationManager.ConnectionStrings["productConnectionString"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                string query = "SELECT Username, Email FROM MemberUser WHERE Username = @Username";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Username", username);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            lbl_user_name.Text = reader["Username"].ToString();
                            lbl_user_email.Text = reader["Email"].ToString();
                        }
                        else
                        {
                            // Handle case where user is not found, e.g., clear labels or display a message
                            lbl_user_name.Text = "User not found";
                            lbl_user_email.Text = "";
                        }
                    }
                }
            }
        }


        // Method to get the profile picture URL for the current user
        protected string GetProfilePictureUrl()
        {
            string username = HttpContext.Current.User.Identity.Name;
            string profilePictureUrl = "/admin/assets/img/default.jpg"; // Default image path

            // Connect to the database and retrieve the profile picture URL
            string connString = ConfigurationManager.ConnectionStrings["productConnectionString"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();

                // Check in both AdminUser and SuperAdminUser tables
                string query = @"
                    SELECT ProfilePicture 
                    FROM AdminUser 
                    WHERE Username = @Username
                    UNION ALL
                    SELECT ProfilePicture 
                    FROM SuperAdminUser 
                    WHERE Username = @Username";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Username", username);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            profilePictureUrl = "/admin/assets/img/" + reader["ProfilePicture"].ToString(); // Append the profile picture filename
                        }
                    }
                }
            }
            return profilePictureUrl; // Return the URL
        }
    }
}