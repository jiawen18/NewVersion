using NewVersion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NewVersion.css
{
    public partial class Signup : System.Web.UI.Page
    {
        userEntities ue = new userEntities();
      

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_sigup_Click1(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                // Get user input from form fields
                string email = txt_emailsignup.Text.Trim();
                string username = txt_username.Text.Trim();
                string password = txt_passwordsingup.Text.Trim();

                string hashedPassword = Security.HashPassword(password);

             
                // Check in the Admin table
                AdminUser admin = ue.AdminUsers.SingleOrDefault(x => x.Username == username && x.Email == email);

                // Check in the Member table if no admin is found
                MemberUser member = ue.MemberUsers.SingleOrDefault(x => x.Username == username && x.Email == email);

                // Determine the role based on email
                string role = DetermineUserRole(email);

                // Save to respective table based on role
                if (role == "Admin")
                {
                    // Create a new admin object and save to Admin table
                    AdminUser newAdmin = new AdminUser
                    {
                        Username = username,
                        Email = email,
                        PasswordHash = hashedPassword
                    };

                    // Add new admin to the Admin table
                    ue.AdminUsers.Add(newAdmin);
                }
                else if (role == "Member")
                {
                    // Create a new member object and save to Member table
                    MemberUser newMember = new MemberUser
                    {
                        Username = username,
                        Email = email,
                        PasswordHash = hashedPassword                        
                    };

                    // Add new member to the Member table
                    ue.MemberUsers.Add(newMember);
                }

                // Save changes to the database
                ue.SaveChanges();

                Response.Redirect("login.aspx");
            }
        }

        public class UserDetails
        {
            public string Username { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
        }

        // This method determines the role based on the user's email
        private string DetermineUserRole(string email)
        {
            // Check the email domain and assign role accordingly
            if (email.EndsWith("@hansumg.com"))
            {
                return "Admin"; // Assign Admin role
            }
            else
            {
                return "Member"; // Assign Member role
            }
         
        }
    }
}