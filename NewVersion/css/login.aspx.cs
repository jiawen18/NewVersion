    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using System.Web.Script.Serialization;
    using ASPSnippets.GoogleAPI;       
    using System.EnterpriseServices;
    using System.Web.Security;
    using Microsoft.Owin.Security;
    using NewVersion.Models;


    namespace NewVersion.css
    {
        public partial class login : System.Web.UI.Page
        {
            userEntities ue = new userEntities();
            protected void Page_Load(object sender, EventArgs e)
            {
                /* From google cloud platform */
                GoogleConnect.ClientId = "995711205443-95hlaqkilp75fhtolsd1079dql0haqip.apps.googleusercontent.com";
                GoogleConnect.ClientSecret = "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";
                GoogleConnect.RedirectUri = Request.Url.AbsoluteUri.Split('?')[0];

                if (!this.IsPostBack)
                {
                    string code = Request.QueryString["code"];
                    if (!string.IsNullOrEmpty(code))
                    {
                        GoogleConnect connect = new GoogleConnect();
                        string json = connect.Fetch("me", code);
                        GoogleProfile profile = new JavaScriptSerializer().Deserialize<GoogleProfile>(json);    
                        txt_email.Text = profile.Email;

                        /* Redirect User to home page after successfully login */
                        Response.Redirect("AboutUs.aspx");
                    }
                }

            }

        protected void btn_signin_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                string emailOrUsername = txt_email.Text.Trim();
                string password = txt_password.Text.Trim();
                bool rememberMe = ckb_remember.Checked;

                // Check if user exists in Admin or Member table
                var AdminUser = ue.AdminUsers.SingleOrDefault(a => a.Email == emailOrUsername || a.Username == emailOrUsername);
                var MemberUser = ue.MemberUsers.SingleOrDefault(m => m.Email == emailOrUsername || m.Username == emailOrUsername);

                // Handle login for admin users
                if (AdminUser != null)
                {
                    string inputPasswordHash = Security.HashPassword(password);
                    if (AdminUser.PasswordHash == inputPasswordHash)
                    {
                        // Log in the user as Admin
                        Security.LoginUser(AdminUser.Username, "Admin", rememberMe);                   
                    }
                    else
                    {
                        // Invalid credentials
                        cvNotMatched.IsValid = false;
                    }
                }
                // Handle login for member users 
                else if (MemberUser != null)
                {
                    string inputPasswordHash = Security.HashPassword(password);
                    if (MemberUser.PasswordHash == inputPasswordHash)
                    {
                        // Log in the user as Member
                        Security.LoginUser(MemberUser.Username, "Member", rememberMe);                  
                    }
                    else
                    {
                        // Invalid credentials
                        cvNotMatched.IsValid = false;
                    }
                }
                // If no user is found
                else
                {
                    cvNotMatched.IsValid = false; // Display error for non-existent user
                }
            }
        }

        protected void login_google_Click(object sender, EventArgs e)
            {
                GoogleConnect.Authorize("profile", "email");
       
            }

            public class GoogleProfile
            {
                public string Id { get; set; }
                public string Name { get; set; }
                public string Picture { get; set; }
                public string Email { get; set; }
                public string Verified_Email { get; set; }
            }
            protected void login_facebook_Click(object sender, EventArgs e)
            {

                var properties = new AuthenticationProperties
                {
                    RedirectUri = "AboutUs.aspx"
                };
                HttpContext.Current.GetOwinContext().Authentication.Challenge(properties, "Facebook");
            }

        }
        }
   
    

