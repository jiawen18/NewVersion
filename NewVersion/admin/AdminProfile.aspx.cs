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
           
            LoadAdminDetails();
           
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
                string query = "SELECT AdminID, Username, Position, Office, Email, DOB, Phone FROM AdminUser WHERE Username = @Username";
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
                        txt_adm_birthday.Text = Convert.ToDateTime(reader["DOB"]).ToString("yyyy-MM-dd");
                        txt_adm_phonr.Text = reader["Phone"].ToString();        
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
            DateTime newBirthday = DateTime.Parse(txt_adm_birthday.Text);
            string newPhone = txt_adm_phonr.Text;

            // Connect to the database and update the admin's details
            string connString = ConfigurationManager.ConnectionStrings["productConnectionString"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                string query = @"UPDATE AdminUser SET 
                                Username = @Username,
                                Position = @Position,
                                Office = @Office,
                                Email = @Email,
                                DOB = @Birthday,
                                Phone = @Phone,                             
                                WHERE Username = @OriginalUsername";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Username", newUsername);
                    cmd.Parameters.AddWithValue("@Position", newPosition);
                    cmd.Parameters.AddWithValue("@Office", newOffice);
                    cmd.Parameters.AddWithValue("@Email", newEmail);
                    cmd.Parameters.AddWithValue("@Birthday", newBirthday);
                    cmd.Parameters.AddWithValue("@Phone", newPhone);              
                    cmd.Parameters.AddWithValue("@OriginalUsername", username);

                    cmd.ExecuteNonQuery();
                }
            }
        
            Response.Redirect("dashboard.aspx");
        }

        protected void btn_acc_cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("dashboard.aspx");
        }
    }
}