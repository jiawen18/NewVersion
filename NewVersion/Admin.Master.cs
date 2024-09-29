﻿
using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web;
using System.Web.Security;
using System.Web.UI;

namespace NewVersion
{
    public partial class Admin : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsUserSuperAdmin() || !IsUserAdmin())
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('You Cannot Access to this page!'); window.location='../css/login.aspx';", true);
            }

            if (!IsPostBack)
            {
                // Set the SiteMapProvider
                AdminBreadcrumbDataSource.Provider = SiteMap.Providers["AdminSiteMapProvider"];

                // Load the user details
                LoadUserDetails();

                if (!IsUserSuperAdmin())
                {
                    // Hide sections only meant for superadmin
                    superAdminSection1.Visible = false;
                    superAdminSection2.Visible = false;
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
                            string profilePictureFilename = reader["ProfilePicture"].ToString();
                            if (!string.IsNullOrEmpty(profilePictureFilename))
                            {
                                profilePictureUrl = "assets/img/" + profilePictureFilename; // Append the profile picture filename

                            }
                            else
                            {
                                profilePictureUrl = "assets/img/default.jpg"; // Default image path
                            }
                        }

                       
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

                // Check in both AdminUser and SuperAdminUser tables
                string query = @"
                    SELECT 'Admin' AS UserType, Username, Email 
                    FROM AdminUser 
                    WHERE Username = @Username
                    UNION ALL
                    SELECT 'SuperAdmin' AS UserType, Username, Email 
                    FROM SuperAdminUser 
                    WHERE Username = @Username";

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
                    isSuperAdmin = count > 0; // If the user exists in SuperAdmin table, treat them as superadmin
                }
            }

            return isSuperAdmin;
        }

        protected bool IsUserAdmin()
        {
            string currentUser = HttpContext.Current.User.Identity.Name; // Get the currently logged-in user's username
            string connString = ConfigurationManager.ConnectionStrings["productConnectionString"].ConnectionString;
            bool isAdmin = false;

            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();

                // Query to check if the user exists in the SuperAdmin table
                string queryAdmin = "SELECT COUNT(1) FROM AdminUser WHERE Username = @Username";
                using (SqlCommand cmdAdmin = new SqlCommand(queryAdmin, conn))
                {
                    cmdAdmin.Parameters.AddWithValue("@Username", currentUser);

                    int count = (int)cmdAdmin.ExecuteScalar();
                    isAdmin = count > 0; 
                }
            }

            return isAdmin;
        }

        protected void LogoutUser(object sender, EventArgs e)
        {

            // Log out of FormsAuthentication
            System.Web.Security.FormsAuthentication.SignOut();

            // Redirect to login page
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Successfully Log Out!'); window.location='../css/Home2.aspx';", true);
        }

    }
}
