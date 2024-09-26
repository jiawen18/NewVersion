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

          
               
             if (!IsUserSuperAdmin())
             {
                 // Hide sections only meant for superadmin
                 superAdminSection.Visible = false;
                 superAdminUpdateSection.Visible = false;
             }
                
            }
        }

        // Method to get the profile picture URL for the current user
        protected string GetProfilePictureUrl()
        {
            string username = HttpContext.Current.User.Identity.Name;
            string profilePictureUrl = "assets/img/default.jpg"; // Default image path

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

                // Check in the AdminUser table first
                string queryAdmin = "SELECT Username, Email FROM AdminUser WHERE Username = @Username";
                using (SqlCommand cmdAdmin = new SqlCommand(queryAdmin, conn))
                {
                    cmdAdmin.Parameters.AddWithValue("@Username", username);

                    using (SqlDataReader readerAdmin = cmdAdmin.ExecuteReader())
                    {
                        if (readerAdmin.Read())
                        {
                           
                            lbl_user_name.Text = readerAdmin["Username"].ToString();
                            lbl_user_email.Text = readerAdmin["Email"].ToString();
                            return; // Exit the method since user details are found
                        }
                    }
                }

                // If not found in AdminUser, check in the SuperAdmin table
                string querySuperAdmin = "SELECT Username, Email FROM SuperAdminUser WHERE Username = @Username";
                using (SqlCommand cmdSuperAdmin = new SqlCommand(querySuperAdmin, conn))
                {
                    cmdSuperAdmin.Parameters.AddWithValue("@Username", username);
                    using (SqlDataReader readerSuperAdmin = cmdSuperAdmin.ExecuteReader())
                    {
                        if (readerSuperAdmin.Read())
                        {                           
                            lbl_user_name.Text = readerSuperAdmin["Username"].ToString();
                            lbl_user_email.Text = readerSuperAdmin["Email"].ToString();
                        }
                    }
                }
            }


        }
        // Method to check if the user is a superadmin
        protected bool IsUserSuperAdmin()
        {
            string currentUser = HttpContext.Current.User.Identity.Name; // Get the currently logged-in user's username
            string connString = ConfigurationManager.ConnectionStrings["productConnectionString"].ConnectionString;
            bool isSuperAdmin = false;

            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();

                // Query to check if the user exists in the SuperAdmin table
                string querySuperAdmin = "SELECT COUNT(1) FROM SuperAdminUser WHERE Username = @Username";
                using (SqlCommand cmdSuperAdmin = new SqlCommand(querySuperAdmin, conn))
                {
                    cmdSuperAdmin.Parameters.AddWithValue("@Username", currentUser);

                    int count = (int)cmdSuperAdmin.ExecuteScalar();
                    if (count > 0)
                    {
                        isSuperAdmin = true; // If the user exists in SuperAdmin table, treat them as superadmin
                    }
                }
            }

            return isSuperAdmin;
        }
    }
}