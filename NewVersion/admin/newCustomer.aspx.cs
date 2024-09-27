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
    public partial class newCustomer : System.Web.UI.Page
    {
        // Create an instance of your entity framework data context
        userEntities ue = new userEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadCustomerData();
            }
        }


        // Method to load admin data and bind it to the Repeater control
        private void LoadCustomerData()
        {
            string connString = ConfigurationManager.ConnectionStrings["productConnectionString"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                string query = "SELECT MemberID, Email, Username FROM MemberUser";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    RepeaterCustomerList.DataSource = reader;
                    RepeaterCustomerList.DataBind();
                }
            }
        }

        protected void btn_add_customer_Click(object sender, EventArgs e)
        {
            // Ensure all the required data is valid
            if (Page.IsValid)
            {
                // Retrieve form data
                string username = txt_cus_username.Text.Trim();
                string email = txt_cus_email.Text.Trim();
                string password = txt_cus_password.Text.Trim();
                string hashedPassword = Security.HashPassword(password);


                // Check if the username or email already exists in the database
                var existingMember = ue.MemberUsers.SingleOrDefault(a => a.Username == username || a.Email == email);
                if (existingMember != null)
                {
                    // If an admin with the same username or email exists, show an error message
                    cvExisted.IsValid = false;
                    cvExisted.ErrorMessage = "Username or Email already exists!";
                    return;
                }

                // Create a new AdminUser object
                var newMemberUser = new MemberUser
                {
                    Username = username,
                    Email = email,
                    PasswordHash = hashedPassword, // Store the hashed password               
                    Role = "Member" // Set the role to member   
                };

                // Add the new admin to the database
                ue.MemberUsers.Add(newMemberUser);

                try
                {
                    // Save changes to the database
                    ue.SaveChanges();
                    // Show a message box indicating success
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Customer account successfully created!'); window.location='customerlist.aspx';", true);
                    // Redirect to the dashboard or another page after successful creation
                }
                catch (Exception ex)
                {
                    // Handle any errors that occur during the save process
                    Console.WriteLine("Error: " + ex.Message);

                }
            }
        }
    }
}