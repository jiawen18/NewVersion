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

            // TODO: Validate that email exists in your database and generate a token or link.
            if (IsValidEmail(userEmail))
            {
                // Generate password reset link (could be a token that you store in your database)
                string resetToken = Guid.NewGuid().ToString();
                string resetLink = "https://localhost:44344/css/forgotpw.aspx?token=" + resetToken;

                // Send the email
                SendResetPasswordEmail(userEmail, resetLink);

                // Optionally, you can show a confirmation message
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Password reset instructions have been sent to your email.');", true);
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

                    if (count > 0)
                    {
                        // Email exists in the database
                        return true;
                    }

                    else
                    {                       
                        // Email does not exist, return true
                        return false;
                    }
                }
            }

        
        }

        private void SendResetPasswordEmail(string userEmail, string resetLink)
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient smtpServer = new SmtpClient("smtp.gmail.com"); // Replace with your SMTP server
                mail.From = new MailAddress("kelvinchong0457@gmail.com"); // Sender email address
                mail.To.Add(userEmail);
                mail.Subject = "Password Reset Request";
                mail.Body = $"Click the link below to reset your password:\n{resetLink}";

                smtpServer.Port = 587; //  vary based on SMTP server
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