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


namespace NewVersion.css
{
    public partial class login : System.Web.UI.Page
    {
  
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
                    Response.Redirect("Home.aspx");
                }
            }

        }

        protected void btn_sigin_Click(object sender, EventArgs e)
        {
            if(txt_email.Text == "kelvinchong0457@gmail.com")
            {
                Response.Redirect("Home.aspx");
            }
            else
            {
                Response.Redirect("../admin/dashboard.aspx");
            }
        }

        protected void login_google_Click(object sender, EventArgs e)
        {
            GoogleConnect.Authorize("profile", "email");
       
        }

    }
    public class GoogleProfile
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Picture { get; set; }
        public string Email { get; set; }
        public string Verified_Email { get; set; }
    }
    }

