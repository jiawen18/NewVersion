using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
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
            // Load admin details from the database
            string connString = ConfigurationManager.ConnectionStrings["productConnectionString"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                string query = "SELECT AdminID, Username, Position, Office, Email, DOB, Phone, Role, ProfilePicture FROM AdminUser WHERE Username = @Username";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Username", HttpContext.Current.User.Identity.Name);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Set the values to the textboxes
                            txt_adm_id.Text = reader["AdminID"].ToString();
                            txt_adm_username.Text = reader["Username"].ToString();
                            txt_adm_position.Text = reader["Position"].ToString();
                            txt_adm_office.Text = reader["Office"].ToString();
                            txt_adm_email.Text = reader["Email"].ToString();
                            txt_adm_birthday.Text = reader["DOB"].ToString();
                            txt_adm_phone.Text = reader["Phone"].ToString();
                            txt_adm_role.Text = reader["Role"].ToString();


                            // Display the current profile picture
                            string profilePicturePath = reader["ProfilePicture"].ToString();
                            if (!string.IsNullOrEmpty(profilePicturePath))
                            {
                                imgProfile.ImageUrl = profilePicturePath;
                            }
                            else
                            {
                                imgProfile.ImageUrl = "assets/img/default.jpg"; // Use a default picture if no picture exists
                            }
                        }
                    }
                }
            }
        }

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
            string profilePicturePath = imgProfile.ImageUrl;  // Keep the current profile picture by default

            // Get password fields for password update
            string currentPassword = txt_adm_crPassword.Text;
            string newPassword = txt_adm_newPassword.Text;
            string repeatNewPassword = txt_adm_rpNewPassword.Text;

            // Handle file upload for profile picture
            if (fileUpload.HasFile)
            {
                // Define the path to save the uploaded file
                string uploadFolderPath = Server.MapPath("~/admin/assets/img/");

                // Create directory if it doesn't exist
                if (!Directory.Exists(uploadFolderPath))
                {
                    Directory.CreateDirectory(uploadFolderPath);
                }

                // Generate a new unique filename using GUID
                string filename = Guid.NewGuid().ToString("N") + ".jpg"; // Save as .jpg

                // Create a SimpleImage instance with the uploaded file
                SimpleImage img = new SimpleImage(fileUpload.PostedFile.InputStream);

                // Process the image: convert to square and resize
                img.Square();
                img.Resize(150);

                // Save the processed image to the defined path
                string filePath = Path.Combine(uploadFolderPath, filename);
                img.SaveAs(filePath);

                // Update the profile picture path
                profilePicturePath = "~/admin/assets/img/" + filename;

                // Update the ImageUrl of imgProfile to show the new uploaded picture
                imgProfile.ImageUrl = profilePicturePath;
            }


            // Connect to the database and update the admin's details
            string connString = ConfigurationManager.ConnectionStrings["productConnectionString"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();

                // Only check passwords if the user is trying to change it
                if (!string.IsNullOrEmpty(currentPassword) || !string.IsNullOrEmpty(newPassword) || !string.IsNullOrEmpty(repeatNewPassword))
                {
                    if (string.IsNullOrEmpty(currentPassword) || string.IsNullOrEmpty(newPassword) || string.IsNullOrEmpty(repeatNewPassword))
                    {
                        LoadAdminDetails();
                        Response.Write("<script>alert('Please provide all password fields to change the password.');</script>");
                        return;
                    }

                    string hashedCurrentPassword = Security.HashPassword(currentPassword);

                    string checkPasswordQuery = "SELECT PasswordHash FROM AdminUser WHERE Username = @Username";
                    using (SqlCommand checkPasswordCmd = new SqlCommand(checkPasswordQuery, conn))
                    {
                        checkPasswordCmd.Parameters.AddWithValue("@Username", username);
                        string storedPassword = (string)checkPasswordCmd.ExecuteScalar();

                        if (storedPassword != hashedCurrentPassword)
                        {
                            LoadAdminDetails();
                            Response.Write("<script>alert('Current password is incorrect.');</script>");
                            return;
                        }
                    }

                    if (newPassword != repeatNewPassword)
                    {
                        LoadAdminDetails();
                        Response.Write("<script>alert('New password and repeated new password do not match.');</script>");
                        return;
                    }
                }

                // Update user details, including the password if provided
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

                // If a new profile picture was uploaded, include it in the update
                if (!string.IsNullOrEmpty(profilePicturePath))
                {
                    query += ", ProfilePicture = @ProfilePicture";
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

                    if (!string.IsNullOrEmpty(profilePicturePath))
                    {
                        cmd.Parameters.AddWithValue("@ProfilePicture", profilePicturePath);
                    }

                    cmd.ExecuteNonQuery();

                    // Notify user of successful update
                    Response.Write("<script>alert('Profile updated successfully.');</script>");

                    // Clear password fields
                    txt_adm_crPassword.Text = "";
                    txt_adm_newPassword.Text = "";
                    txt_adm_rpNewPassword.Text = "";

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