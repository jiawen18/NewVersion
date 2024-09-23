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
                string email = txt_emailsignup.Text.Trim();
                string username = txt_username.Text.Trim();
                string password = txt_passwordsingup.Text.Trim();
                string hashedPassword = Security.HashPassword(password);

                string role = DetermineUserRole(email);
             
                    if (role == "Admin")
                    {
                        AdminUser existingAdmin = ue.AdminUsers.SingleOrDefault(x => x.Username == username && x.Email == email);
                        if (existingAdmin != null)
                        {
                            // show error message
                            cvExisted.IsValid = false;
                            return;
                        }

                        AdminUser newAdmin = new AdminUser
                        {
                            Username = username,
                            Email = email,
                            PasswordHash = hashedPassword
                        };

                        ue.AdminUsers.Add(newAdmin);
                        ue.SaveChanges();
                        Response.Redirect("login.aspx");
                    }
                    else if (role == "Member")
                    {
                        MemberUser existingMember = ue.MemberUsers.SingleOrDefault(x => x.Username == username && x.Email == email);
                        if (existingMember != null)
                        {
                            // show error message
                            cvExisted.IsValid = false;
                            return;
                        }

                        MemberUser newMember = new MemberUser
                        {
                            Username = username,
                            Email = email,
                            PasswordHash = hashedPassword
                        };

                        ue.MemberUsers.Add(newMember);
                        ue.SaveChanges();
                        Response.Redirect("login.aspx");
                    }              
               
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