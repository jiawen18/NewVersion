using System;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Web.UI;

namespace NewVersion.css
{
    public partial class Account : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadAccountDetails();
            }
        }

        private void LoadAccountDetails()
        {
            string username = HttpContext.Current.User.Identity.Name;
            string connString = ConfigurationManager.ConnectionStrings["productConnectionString"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                string query = "SELECT MemberID, Username, Email, DOB, Phone, ProfilePicture FROM MemberUser WHERE Username = @Username";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Username", username);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            txt_mb_id.Text = reader["MemberID"].ToString();
                            txt_username.Text = reader["Username"].ToString();
                            txt_acc_email.Text = reader["Email"].ToString();
                            txt_acc_birthday.Text = reader["DOB"].ToString();
                            txt_acc_phonr.Text = reader["Phone"].ToString();

                            string profilePictureFilename = reader["ProfilePicture"].ToString();
                            imgProfile.ImageUrl = !string.IsNullOrEmpty(profilePictureFilename) ? "/admin/assets/img/" + profilePictureFilename : "/admin/assets/img/default.jpg";
                        }
                    }
                }
            }
        }

        protected void btn_acc_svChanges_Click(object sender, EventArgs e)
        {
            string username = HttpContext.Current.User.Identity.Name;
            string newUsername = txt_username.Text;
            string newEmail = txt_acc_email.Text;
            string newBirthday = txt_acc_birthday.Text;
            string newPhone = txt_acc_phonr.Text;
            string profilePictureFilename = null;

            string currentPassword = txt_acc_crPassword.Text;
            string newPassword = txt_acc_newPassword.Text;
            string repeatNewPassword = txt_acc_rpNewPassword.Text;

            // Handle file upload
            if (fileUpload.HasFile)
            {
                string uploadFolderPath = Server.MapPath("~/admin/assets/img/");
                if (!Directory.Exists(uploadFolderPath)) Directory.CreateDirectory(uploadFolderPath);

                string filename = Guid.NewGuid().ToString("N") + ".jpg";
                SimpleImage img = new SimpleImage(fileUpload.PostedFile.InputStream);
                img.Square();
                img.SaveAs(Path.Combine(uploadFolderPath, filename));
                profilePictureFilename = filename;
                imgProfile.ImageUrl = "/admin/assets/img/" + filename;
            }

            string connString = ConfigurationManager.ConnectionStrings["productConnectionString"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();

                // Password validation and hashing logic (if necessary)
                if (!string.IsNullOrEmpty(currentPassword) || !string.IsNullOrEmpty(newPassword) || !string.IsNullOrEmpty(repeatNewPassword))
                {
                    if (string.IsNullOrEmpty(currentPassword) || string.IsNullOrEmpty(newPassword) || string.IsNullOrEmpty(repeatNewPassword))
                    {
                        Response.Write("<script>alert('Please provide all password fields to change the password.');</script>");
                        LoadAccountDetails();
                        return;
                    }

                    // Validate current password
                    string hashedCurrentPassword = Security.HashPassword(currentPassword);
                    string checkPasswordQuery = "SELECT PasswordHash FROM MemberUser WHERE Username = @Username";
                    using (SqlCommand checkPasswordCmd = new SqlCommand(checkPasswordQuery, conn))
                    {
                        checkPasswordCmd.Parameters.AddWithValue("@Username", username);
                        string storedPassword = (string)checkPasswordCmd.ExecuteScalar();

                        if (storedPassword != hashedCurrentPassword)
                        {
                            LoadAccountDetails();
                            Response.Write("<script>alert('Current password is incorrect.');</script>");                        
                            return;
                        }
                    }

                    // Ensure the new passwords match
                    if (newPassword != repeatNewPassword)
                    {
                        LoadAccountDetails();
                        Response.Write("<script>alert('New password and repeated password do not match.');</script>");                 
                        return;
                    }
                }

                // Construct the SQL query
                string query = "UPDATE MemberUser SET " +
                               "Username = @Username, " +
                               "Email = @Email, " +
                               "DOB = @Birthday, " +
                               "Phone = @Phone";

                if (!string.IsNullOrEmpty(newPassword))
                {
                    query += ", PasswordHash = @NewPassword";
                }

                if (!string.IsNullOrEmpty(profilePictureFilename))
                {
                    query += ", ProfilePicture = @ProfilePicture";
                }

                query += " WHERE Username = @OriginalUsername";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    // Set the parameters for the query
                    cmd.Parameters.AddWithValue("@Username", newUsername);
                    cmd.Parameters.AddWithValue("@Email", newEmail);
                    cmd.Parameters.AddWithValue("@Birthday", newBirthday);
                    cmd.Parameters.AddWithValue("@Phone", newPhone);
                    cmd.Parameters.AddWithValue("@OriginalUsername", username);

                    if (!string.IsNullOrEmpty(newPassword))
                    {
                        string hashedNewPassword = Security.HashPassword(newPassword);
                        cmd.Parameters.AddWithValue("@NewPassword", hashedNewPassword);
                    }

                    if (!string.IsNullOrEmpty(profilePictureFilename))
                    {
                        cmd.Parameters.AddWithValue("@ProfilePicture", profilePictureFilename);
                    }

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        Response.Write("<script>alert('Profile updated successfully.');</script>");
                    }
                    else
                    {
                        Response.Write("<script>alert('No changes were made to the profile.');</script>");
                    }
                }
            }

            Response.Write("<script>alert('Profile updated successfully.');</script>");
            // Clear password fields after update
            txt_acc_crPassword.Text = "";
            txt_acc_newPassword.Text = "";
            txt_acc_rpNewPassword.Text = "";      
            LoadAccountDetails();
        }


        protected void btn_acc_cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("UserProfile.aspx");
        }
    }
}
