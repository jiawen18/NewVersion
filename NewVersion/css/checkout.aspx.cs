using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NewVersion.css
{
    public partial class checkout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
        }

        protected void ddlBanks_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedValue = ddlPaymentMethod.SelectedItem.Text;

            if (selectedValue == "Public Bank" || selectedValue == "Touch n Go")
            {
                
                lblText.Text = "";
            }
            else
            {
                
                lblText.Text = "Please select a valid payment method.";
            }

            
            lblTransfer.Text = selectedValue;
        }

        protected void btnOrder_Click(object sender, EventArgs e)
        {
            string amount = lblAmount.Text;
            string paymentMethod = lblTransfer.Text;

            if (paymentMethod == "Public Bank")
            {
                Session["PaymentCompleted"] = true;

                Response.Redirect("https://www2.pbebank.com/myIBK/apppbb/servlet/BxxxServlet?RDOName=BxxxAuth&MethodName=login");
            }
            else if (paymentMethod == "Touch n Go")
            {
                Session["PaymentCompleted"] = true;

                Response.Redirect("https://tngportal.touchngo.com.my/;#login");
            }
            else
            {
                lblText.Text = "Please select a valid payment method.";
            }

            //connect with the URL and payment gateway (ipay88)
            string gatewayUrl = "https://sandbox.ipay88.com.my/epayment/entry.asp";
            string merchantCode = "YourMerchantCode"; //get from ipay88
            string paymentID = "1";

            //create a form, direct to the payment gateway
            string responseUrl = "https://localhost:44344/css/ResponsePayment.aspx";
            string backendUrl = "https://localhost:44344/css/BackendPayment.aspx";

            //request the form
            string formHtml = $@"
                <html>
                <body onload = 'document.form[0].submit();
                    <form action = '{gatewayUrl}' method='POST'>
                        <input type='hidden' name='MerchantCode' value='{merchantCode}'/>
                        <input type='hidden' name='MerchantCode' value='{amount}'/>
                        <input type='hidden' name='MerchantCode' value='{paymentID}'/>
                        <input type='hidden' name='MerchantCode' value='{responseUrl}'/>
                        <input type='hidden' name='MerchantCode' value='{backendUrl}'/>
                        <noscript><input type='hidden' name='MerchantCode' value='Continue'></noscript>
                    </form>
                </body>
                </html>";

            Response.Clear();
            Response.Write(formHtml);
            Response.End();
        }
        



        protected void btnCloseDialog_Click(object sender, EventArgs e)
        {
            Response.Redirect("checkout.aspx");
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            PlaceHolder1.Controls.Clear();

            Label lbl1 = new Label();
            lbl1.ID = "lbl1";
            lbl1.Text = "First Name: " + c_diff_fname.Text;

            Label lbl2 = new Label();
            lbl2.ID = "lbl2";
            lbl2.Text = "Phone Number: " + c_diff_phone.Text;

            Label lbl3 = new Label();
            lbl3.ID = "lbl3";
            lbl3.Text = "Address: " + c_diff_address.Text;

            PlaceHolder1.Controls.Add(lbl1);
            PlaceHolder1.Controls.Add(new LiteralControl("<br />"));  
            PlaceHolder1.Controls.Add(lbl2);
            PlaceHolder1.Controls.Add(new LiteralControl("<br />"));  
            PlaceHolder1.Controls.Add(lbl3);

            Response.Redirect("checkout.aspx");
        }


    }
}