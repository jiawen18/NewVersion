using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NewVersion.css
{
    public partial class SuccessPage : System.Web.UI.Page
    {
       protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // data from session
                lblOrderId.Text = Request.QueryString["orderId"];
                lblTransactionId.Text = Request.QueryString["TransactionId"];
                lblInvoiceId.Text = Request.QueryString["InvoiceId"];
                
                if (Session["Amount"] != null)
                {
                    double amount = (double)Session["Amount"]; // read from session
                    lblAmount.Text = "RM " + amount.ToString("F2"); // show amount
                }

            }
        }

        protected void btnContinue_Click(object sender, EventArgs e)
        {
            Response.Redirect("Home.aspx");
        }
    }
}