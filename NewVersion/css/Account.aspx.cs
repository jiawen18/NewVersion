using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NewVersion.css
{
    public partial class Account : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // get value and store in Session
            Session["UserName"] = txt_username.Text;  
            Session["Contact"] = txt_acc_phonr.Text;  
            Session["Email"] = txt_acc_email.Text;
        }

        protected void btn_acc_cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("Userprofile.aspx");
        }

        protected void btn_acc_svChanges_Click(object sender, EventArgs e)
        {
            // update Session when save
            Session["UserName"] = txt_username.Text;
            Session["Contact"] = txt_acc_phonr.Text;
            Session["Email"] = txt_acc_email.Text;
        }
    }
}