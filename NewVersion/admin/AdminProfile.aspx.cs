using System;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Web.UI;

namespace NewVersion.admin
{
    public partial class AdminProfile : System.Web.UI.Page
    {
        // Flag to track if the user is in AdminUser or SuperAdminUser
        private string userTable = "AdminUser"; // Default table is AdminUser
        private string idColumn = "AdminID"; // Default ID column

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Load admin details on first load
                LoadAdminDetails();
            }
        }

        private void LoadAdminDetails()
        {
            string username = HttpContext.Current.User.Identity.Name;
            string connString = ConfigurationManager.ConnectionStrings["productConnectionString"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();

                // Check if user exists in AdminUser
                string query = "SELECT AdminID, Username, Position, Office, Email, DOB, Phone, Role, ProfilePicture FROM AdminUser WHERE Username = @Username";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Username", username);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            userTable = "AdminUser";
                            idColumn = "AdminID"; // Set the ID column for AdminUser
                            SetAdminDetails(reader);
                            return; // Exit if user is found in AdminUser
                        }
                    }
                }

                // Check if user exists in SuperAdminUser
                query = "SELECT SuperAdminID, Username, Position, Office, Email, DOB, Phone, Role, ProfilePicture FROM SuperAdminUser WHERE Username = @Username";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Username", username);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            userTable = "SuperAdminUser";
                            idColumn = "SuperAdminID"; // Set the ID column for SuperAdminUser
                            SetAdminDetails(reader);
                        }
                    }
                }
            }
        }

        // Helper method to set admin details to the textboxes
        private void SetAdminDetails(SqlDataReader reader)
        {
            // Use the dynamically determined ID column
            txt_adm_id.Text = reader[idColumn].ToString();
            txt_adm_username.Text = reader["Username"].ToString();
            txt_adm_position.Text = reader["Position"].ToString();
            txt_adm_office.Text = reader["Office"].ToString();
            txt_adm_email.Text = reader["Email"].ToString();
            txt_adm_birthday.Text = reader["DOB"].ToString();
            txt_adm_phone.Text = reader["Phone"].ToString();
            txt_adm_role.Text = reader["Role"].ToString();

            string profilePictureFilename = reader["ProfilePicture"].ToString();
            imgProfile.ImageUrl = !string.IsNullOrEmpty(profilePictureFilename) ? "assets/img/" + profilePictureFilename : "assets/img/default.jpg";
        }

        protected void btn_acc_svChanges_Click(object sender, EventArgs e)
        {
            string username = HttpContext.Current.User.Identity.Name;
            string newUsername = txt_adm_username.Text;
            string newPosition = txt_adm_position.Text;
            string newOffice = txt_adm_office.Text;
            string newEmail = txt_adm_email.Text;
            string newBirthday = txt_adm_birthday.Text;
            string newPhone = txt_adm_phone.Text;
            string profilePictureFilename = null;

            string currentPassword = txt_adm_crPassword.Text;
            string newPassword = txt_adm_newPassword.Text;
            string repeatNewPassword = txt_adm_rpNewPassword.Text;

            if (fileUpload.HasFile)
            {
                string uploadFolderPath = Server.MapPath("~/admin/assets/img/");
                if (!Directory.Exists(uploadFolderPath)) Directory.CreateDirectory(uploadFolderPath);

                string filename = Guid.NewGuid().ToString("N") + ".jpg";
                SimpleImage img = new SimpleImage(fileUpload.PostedFile.InputStream);
                img.Square();
                img.SaveAs(Path.Combine(uploadFolderPath, filename));
                profilePictureFilename = filename;
                imgProfile.ImageUrl = "assets/img/" + filename;
            }

            string connString = ConfigurationManager.ConnectionStrings["productConnectionString"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();

                if (!string.IsNullOrEmpty(currentPassword) || !string.IsNullOrEmpty(newPassword) || !string.IsNullOrEmpty(repeatNewPassword))
                {
                    if (string.IsNullOrEmpty(currentPassword) || string.IsNullOrEmpty(newPassword) || string.IsNullOrEmpty(repeatNewPassword))
                    {
                        LoadAdminDetails();
                        Response.Write("<script>alert('Please provide all password fields to change the password.');</script>");
                        return;
                    }

                    string hashedCurrentPassword = Security.HashPassword(currentPassword);
                    string checkPasswordQuery = $"SELECT PasswordHash FROM {userTable} WHERE Username = @Username";
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

                string query = $@"UPDATE {userTable} SET 
                         Username = @Username,
                         Position = @Position,
                         Office = @Office,
                         Email = @Email,
                         DOB = @Birthday,
                         Phone = @Phone";

                if (!string.IsNullOrEmpty(newPassword)) query += ", PasswordHash = @NewPassword";
                if (!string.IsNullOrEmpty(profilePictureFilename)) query += ", ProfilePicture = @ProfilePicture";
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

                    if (!string.IsNullOrEmpty(newPassword))
                    {
                        string hashedNewPassword = Security.HashPassword(newPassword);
                        cmd.Parameters.AddWithValue("@NewPassword", hashedNewPassword);
                    }

                    if (!string.IsNullOrEmpty(profilePictureFilename))
                    {
                        cmd.Parameters.AddWithValue("@ProfilePicture", profilePictureFilename);
                    }

                    cmd.ExecuteNonQuery();

                    Response.Write("<script>alert('Profile updated successfully.');</script>");
                    txt_adm_crPassword.Text = "";
                    txt_adm_newPassword.Text = "";
                    txt_adm_rpNewPassword.Text = "";
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
