using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;

namespace NewVersion.css
{
    public partial class forgotpw : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btn_resetpw_Click(object sender, EventArgs e)
        {
            string userEmail = txt_emailreset.Text;

            if (IsValidEmail(userEmail))
            {
                // Generate a new random password
                string newPassword = GenerateRandomPassword();

                string hashPassword = Security.HashPassword(newPassword);

                // Update the new password in the database
                UpdateUserPassword(userEmail, hashPassword);

                // Send the new password via email
                SendNewPasswordEmail(userEmail, newPassword);

                // Optionally, show a confirmation message
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('A new password has been sent to your email!'); window.location='login.aspx';", true);
            }
            else
            {
                // Optionally, show an error message
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Invalid email address.');", true);
            }
        }

        private bool IsValidEmail(string email)
        {
            string connString = ConfigurationManager.ConnectionStrings["productConnectionString"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                string query = "SELECT COUNT(*) FROM MemberUser WHERE Email = @Email";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Email", email);

                    int count = (int)cmd.ExecuteScalar(); // Get the number of rows with the provided email

                    return count > 0; // Email exists in the database
                }
            }
        }

        private string GenerateRandomPassword(int length = 8)
        {
            const string validChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            Random random = new Random();
            char[] newPassword = new char[length];

            for (int i = 0; i < length; i++)
            {
                newPassword[i] = validChars[random.Next(validChars.Length)];
            }

            return new string(newPassword);
        }

        private void UpdateUserPassword(string userEmail, string hashPassword)
        {
            string connString = ConfigurationManager.ConnectionStrings["productConnectionString"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                string query = "UPDATE MemberUser SET PasswordHash = @NewPassword WHERE Email = @Email"; // Assuming PasswordHash stores the password securely
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@NewPassword", hashPassword); // Save the new password
                    cmd.Parameters.AddWithValue("@Email", userEmail);
                    cmd.ExecuteNonQuery(); // Execute the update command
                }
            }
        }

        private void SendNewPasswordEmail(string userEmail, string newPassword)
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient smtpServer = new SmtpClient("smtp.gmail.com"); // Replace with your SMTP server
                mail.From = new MailAddress("kelvinchong0457@gmail.com"); // Sender email address
                mail.To.Add(userEmail);
                mail.Subject = "Your New Password";
                mail.Body = $"Your new password is: {newPassword}";

                smtpServer.Port = 587; // vary based on SMTP server
                smtpServer.Credentials = new NetworkCredential("kelvinchong0457@gmail.com", "fdxk vpul gxqv ivlu");
                smtpServer.EnableSsl = true; // Enable SSL if required by your email provider

                smtpServer.Send(mail);
            }
            catch (Exception ex)
            {
                // Log or handle error
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Error sending email: " + ex.Message + "');", true);
            }
        }
    }
}
