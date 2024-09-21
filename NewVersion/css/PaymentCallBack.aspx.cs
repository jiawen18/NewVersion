using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NewVersion.css
{
    public partial class PaymentCallBack : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.HttpMethod == "POST")
            {
                string paymentStatus = Request.Form["status"]; // get status from payment gateway
                string orderId = Request.Form["order_id"];    // get orderId

                if (paymentStatus == "success")
                {
                    // update Status
                    UpdateOrderStatus(orderId, "Paid");

                    // payment succesfully
                    Response.Redirect("SuccessPage.aspx");
                }
                else
                {
                    // payment failed
                    Response.Redirect("FailurePage.aspx"); 
                }
            }
        }

        private void UpdateOrderStatus(string orderId, string status)
        {
            // 你可以在此处通过数据库操作更新订单的支付状态
            // 例如更新订单表中的订单状态为"Paid"
        }
    }
}