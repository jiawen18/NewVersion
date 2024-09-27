
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
                            txt_mb_id.Text = reader["MemberID"].ToString(); // Display the Member ID
                            txt_mb_id.ReadOnly = true; // Set the text box as read-only
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
            string memberId = txt_mb_id.Text; // Keep the Member ID

            string newUsername = txt_username.Text.Trim();
            string newEmail = txt_acc_email.Text.Trim();
            string newBirthday = txt_acc_birthday.Text.Trim();
            string newPhone = txt_acc_phonr.Text.Trim();
            string profilePictureFilename = null;

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
                imgProfile.ImageUrl = "~/admin/assets/img/" + filename; // Update the profile image
            }

            string connString = ConfigurationManager.ConnectionStrings["productConnectionString"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();

                // Construct the SQL query
                string query = "UPDATE MemberUser SET " +
                               "Username = @Username, " +
                               "Email = @Email, " +
                               "DOB = @Birthday, " +
                               "Phone = @Phone";

                if (!string.IsNullOrEmpty(txt_acc_newPassword.Text))
                {
                    query += ", PasswordHash = @NewPassword";
                }

                if (!string.IsNullOrEmpty(profilePictureFilename))
                {
                    query += ", ProfilePicture = @ProfilePicture";
                }

                query += " WHERE MemberID = @MemberID"; // Use MemberID for the WHERE clause

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    // Set the parameters for the query
                    cmd.Parameters.AddWithValue("@Username", newUsername);
                    cmd.Parameters.AddWithValue("@Email", newEmail);
                    cmd.Parameters.AddWithValue("@Birthday", newBirthday);
                    cmd.Parameters.AddWithValue("@Phone", newPhone);
                    cmd.Parameters.AddWithValue("@MemberID", memberId); // Use MemberID as a parameter

                    if (!string.IsNullOrEmpty(txt_acc_newPassword.Text))
                    {
                        string hashedNewPassword = Security.HashPassword(txt_acc_newPassword.Text);
                        cmd.Parameters.AddWithValue("@NewPassword", hashedNewPassword);
                    }

                    if (!string.IsNullOrEmpty(profilePictureFilename))
                    {
                        cmd.Parameters.AddWithValue("@ProfilePicture", profilePictureFilename);
                    }

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Profile updated successfully.'); window.location='UserProfile.aspx';", true);
                        // Redirect to the dashboard or another page after successful creation
                    }
                    else
                    {
                        Response.Write("<script>alert('No changes were made to the profile.');</script>");
                    }
                }
            }

            // Clear password fields after update
            txt_acc_crPassword.Text = "";
            txt_acc_newPassword.Text = "";
            txt_acc_rpNewPassword.Text = "";
            LoadAccountDetails(); // Reload the account details
        }



        protected void btn_acc_cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("UserProfile.aspx");
        }
    }
}
