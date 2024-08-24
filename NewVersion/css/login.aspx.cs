using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Script.Serialization;
using ASPsnippets.GoogleAPI;
using System.IO;
using System.Net;
using System.Text;

namespace NewVersion.css
{
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            GoogleConnect.ClientId = "995711205443-95hlaqkilp75fhtolsd1079dql0haqip.apps.googleusercontent.com";
            GoogleConnect.ClientSecret = "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";
            GoogleConnect.RedirectUri = Request.Url.AbsoluteUri.Split('?')[0];

            if (!this.IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["code"]))
                {
                    string code = Request.QueryString["code"];
                    string json = GoogleConnect.Fetch("me", code);
                    GoogleProfile profile = new JavaScriptSerializer().Deserialize<GoogleProfile>(json);
                    lbl_email.Text = profile.Email;
                    lbl_name.Text = profile.Name;
                    ProfileImage.ImgUrl = profile.picture;
                    pnlProfile.Visible = true;
                    btn_sigin.Enabled = false;
                }
                if (Request.QueryString["error"] == "access_denied")
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "alert('Access denied.')", true);
                }
            }
        }

        protected void btn_sigin_Click(object sender, EventArgs e)
        {
            Response.Redirect("Home.aspx");
        }

        protected void login_google_Click(object sender, EventArgs e)
        {
            GoogleConnect.Authorize("profile", "email");
        }

        public class GoogleProfile
        {
            public string Email { get; set; }
            public string Name { get; set; }

        }


    }
}