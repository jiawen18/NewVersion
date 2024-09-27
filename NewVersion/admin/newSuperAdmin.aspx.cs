﻿
using NewVersion.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NewVersion.admin
{
    public partial class newSuperAdmin : System.Web.UI.Page
    { // Create an instance of your entity framework data context
        userEntities ue = new userEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadSuperAdminData();
            }
        }


        // Method to load admin data and bind it to the Repeater control
        private void LoadSuperAdminData()
        {
            string connString = ConfigurationManager.ConnectionStrings["productConnectionString"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                string query = "SELECT SuperAdminID, Email, Username, Position, Office FROM SuperAdminUser";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    RepeaterSuperAdminList.DataSource = reader;
                    RepeaterSuperAdminList.DataBind();
                }
            }
        }

       

        protected void btn_add_sp_admin_Click(object sender, EventArgs e)
        {
            // Ensure all the required data is valid
            if (Page.IsValid)
            {
                // Retrieve form data
                string username = txt_sp_adm_username.Text.Trim();
                string email = txt_sp_adm_email.Text.Trim();
                string password = txt_sp_adm_password.Text.Trim();
                string hashedPassword = Security.HashPassword(password);
                string position = txt_sp_adm_position.Text.Trim();
                string office = txt_sp_adm_office.Text.Trim();


                // Check if the username or email already exists in the database
                var existingSuperAdmin = ue.SuperAdminUsers.SingleOrDefault(a => a.Username == username || a.Email == email);
                if (existingSuperAdmin != null)
                {
                    // If an admin with the same username or email exists, show an error message
                    cvExisted.IsValid = false;
                    cvExisted.ErrorMessage = "Username or Email already exists!";
                    return;
                }

                // Create a new AdminUser object
                var newSuperAdmin = new SuperAdminUser
                {
                    Username = username,
                    Email = email,
                    PasswordHash = hashedPassword, // Store the hashed password
                    Position = position,
                    Office  = office,
                    Role = "Super Admin" // Set the role to Admin (or use role-based logic)     
                };

                // Add the new admin to the database
                ue.SuperAdminUsers.Add(newSuperAdmin);

                try
                {
                    // Save changes to the database
                    ue.SaveChanges();
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Super Admin account successfully created!'); window.location='newSuperAdmin.aspx';", true);
                    // Redirect to the dashboard or another page after successful creation
                }
                catch (Exception ex)
                {
                    // Handle any errors that occur during the save process
                    Console.WriteLine("Error: " + ex.Message);
                    // Optionally, show a message to the user or log the error
                }
            }
        }
    }
}