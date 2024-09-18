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

      

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_sigup_Click1(object sender, EventArgs e)
        {
            // Get user input from form fields
            string email = txt_emailsignup.Text.Trim();
            string username = txt_username.Text.Trim();
            string password = txt_passwordsingup.Text.Trim();

            string hashedPassword = Security.HashPassword(password);


            Response.Redirect("login.aspx");
        }

        public class UserDetails
        {
            public string Username { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
        }
    }
}