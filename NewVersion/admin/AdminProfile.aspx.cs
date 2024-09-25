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
    public partial class AdminProfile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                // Populate the textboxes only on the first load, not on postbacks
                LoadAdminDetails();
            }

        }

        // Load the currently logged-in admin's details
        private void LoadAdminDetails()
        {
            // Get the currently logged-in admin's username 
            string username = HttpContext.Current.User.Identity.Name;

            // Connect to the database and retrieve the admin details
            string connString = ConfigurationManager.ConnectionStrings["productConnectionString"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                string query = "SELECT AdminID, Username, Position, Office, Email, DOB, Phone, Role FROM AdminUser WHERE Username = @Username";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Username", username);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        // Populate the form fields with the admin's details
                        txt_adm_id.Text = reader["AdminID"].ToString();
                        txt_adm_username.Text = reader["Username"].ToString();
                        txt_adm_position.Text = reader["Position"].ToString();
                        txt_adm_office.Text = reader["Office"].ToString();
                        txt_adm_email.Text = reader["Email"].ToString();
                        txt_adm_birthday.Text = reader["DOB"].ToString();
                        txt_adm_phone.Text = reader["Phone"].ToString();
                        txt_adm_role.Text = reader["Role"].ToString() ;
                    }
                }
            }
        }

        // Save the updated admin details
        protected void btn_acc_svChanges_Click(object sender, EventArgs e)
        {
            string username = HttpContext.Current.User.Identity.Name;

            // Get the new values from the textboxes
            string newUsername = txt_adm_username.Text;
            string newPosition = txt_adm_position.Text;
            string newOffice = txt_adm_office.Text;
            string newEmail = txt_adm_email.Text;
            string newBirthday = txt_adm_birthday.Text;
            string newPhone = txt_adm_phone.Text;

            // Get password fields for password update
            string currentPassword = txt_adm_crPassword.Text;
            string newPassword = txt_adm_newPassword.Text;
            string repeatNewPassword = txt_adm_rpNewPassword.Text;

            // Hash the current entered password for comparison
            string hashedCurrentPassword = Security.HashPassword(currentPassword);

            // Connect to the database and update the admin's details
            string connString = ConfigurationManager.ConnectionStrings["productConnectionString"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();

                // check if the current password matches
                string checkPasswordQuery = "SELECT PasswordHash FROM AdminUser WHERE Username = @Username";
                using (SqlCommand checkPasswordCmd = new SqlCommand(checkPasswordQuery, conn))
                {
                    checkPasswordCmd.Parameters.AddWithValue("@Username", username);
                    string storedPassword = (string)checkPasswordCmd.ExecuteScalar();

          

                    // Check if the current password is correct
                    if (storedPassword != hashedCurrentPassword)
                    {
                        // If the current password does not match, stop the process
                        Response.Write("<script>alert('Current password is incorrect.');</script>");
                        return;
                        
                    }
                }

                // Check if new password and repeat password match
                if (!string.IsNullOrEmpty(newPassword) && newPassword != repeatNewPassword)
                {
                    // If new passwords don't match, stop the process
                    Response.Write("<script>alert('New password and repeated new password do not match.');</script>");
                    return;
                   
                }

                // Proceed to update user details, including the password if provided
                string query = @"UPDATE AdminUser SET 
                                Username = @Username,
                                Position = @Position,
                                Office = @Office,
                                Email = @Email,
                                DOB = @Birthday,
                                Phone = @Phone";

                // If the user entered a new password, hash it and include it in the update
                if (!string.IsNullOrEmpty(newPassword))
                {
                    query += ", PasswordHash = @NewPassword";
                }

                query += " WHERE Username = @OriginalUsername";



                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Username", newUsername);
                    cmd.Parameters.AddWithValue("@Position", newPosition);
                    cmd.Parameters.AddWithValue("@Office", newOffice);
                    cmd.Parameters.AddWithValue("@Email", newEmail);
                    cmd.Parameters.AddWithValue("@Birthday", newBirthday);
                    cmd.Parameters.AddWithValue("@Phone", newPhone);
                    cmd.Parameters.AddWithValue("@OriginalUsername", username);

                    // Only add the hashed new password parameter if it's not empty
                    if (!string.IsNullOrEmpty(newPassword))
                    {
                        string hashedNewPassword = Security.HashPassword(newPassword);
                        cmd.Parameters.AddWithValue("@NewPassword", hashedNewPassword);
                    }

                    System.Diagnostics.Debug.WriteLine("New Username: " + txt_adm_username.Text);
                    System.Diagnostics.Debug.WriteLine("New Position: " + txt_adm_position.Text);
                    System.Diagnostics.Debug.WriteLine("New password " + newPassword);
                    System.Diagnostics.Debug.WriteLine("New office: " + txt_adm_office.Text);

                    cmd.ExecuteNonQuery();
                    // Notify user of successful update
                    Response.Write("<script>alert('Profile updated successfully.');</script>");
                    // Reload the admin details to show updated values
                    LoadAdminDetails();

                }

                
            }

            
        }

        protected void btn_acc_cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("dashboard.aspx");
        }
    }
}