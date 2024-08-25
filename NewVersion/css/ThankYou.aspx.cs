using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NewVersion.css
{
    public partial class ThankYou : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // 防止循环重定向
            if (Session["PaymentCompleted"] == null)
            {
                // 只在必要时重定向
                if (Request.UrlReferrer == null || !Request.UrlReferrer.AbsoluteUri.Contains("Checkout.aspx"))
                {
                    Response.Redirect("Checkout.aspx");
                }
            }
        }
    }
}