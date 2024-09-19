using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NewVersion.css
{
    public partial class ResponsePayment : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string paymentStatus = Request.Form["Status"];

            if(paymentStatus == "1")  //assume "1" is successful
            {
                lblMessage.Text = "Payment Successful!";
            }

            else
            {
                lblMessage.Text = "Payment Failed!";
            }
        }
    }
}