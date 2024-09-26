using NewVersion.Models;
using System;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web.UI;

namespace NewVersion.css
{
    public partial class Signup : System.Web.UI.Page
    {
        userEntities ue = new userEntities();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_sigup_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                string email = txt_emailsignup.Text.Trim();
                string username = txt_username.Text.Trim();
                string password = txt_passwordsingup.Text.Trim();
                string hashedPassword = Security.HashPassword(password);

                // Ensure that only Member accounts are created
                string role = "Member";

                // Check if the user already exists in the Member table
                MemberUser existingMember = ue.MemberUsers.SingleOrDefault(x => x.Username == username && x.Email == email);
                if (existingMember != null)
                {
                    // Show error message if the user already exists
                    cvExisted.IsValid = false;
                    return;
                }

                // Create a new Member account
                MemberUser newMember = new MemberUser
                {
                    Username = username,
                    Email = email,
                    PasswordHash = hashedPassword,
                    Role = role
                };

                // Add new member to the database
                ue.MemberUsers.Add(newMember);

                try
                {
                    ue.SaveChanges();
                    // Redirect to login page after successful signup
                    Response.Redirect("login.aspx");
                }
                catch (DbUpdateException ex)
                {
                    // Get more details from the inner exception
                    var innerException = ex.InnerException?.InnerException;
                    if (innerException != null)
                    {
                        Console.WriteLine(innerException.Message); // Log or display the inner exception
                    }
                }
            }
        }

      
    }
}
