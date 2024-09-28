using NewVersion.Models;
using Razorpay.Api;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static Google.Apis.Requests.BatchRequest;

namespace NewVersion.css
{
    public partial class FailurePage : System.Web.UI.Page
    {
        string cs = Global.CS;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                string orderID ="";

                string transactionID = Request.QueryString["TransactionId"];

                string orderTotalPrice = "";

                if (Session["Amount"] != null)
                {
                    decimal amount = (decimal)Session["Amount"];
                    orderTotalPrice = "RM " + amount.ToString("N2");

                }

                string invoiceId = "";
                string date = "";


                string transactionStatus = "Failed";

                if (!string.IsNullOrEmpty(transactionID))
                {
                        SaveTransactionDetails(transactionID, orderID, orderTotalPrice, invoiceId, date, transactionStatus);
                   
                }

            }
        }

        
        protected void btnContinue_Click(object sender, EventArgs e)
        {
            Response.Redirect("Home2.aspx");
        }

        private void SaveTransactionDetails(string transactionID, string orderID, string totalPrice, string invoiceId, string date, string transactionStatus)
        {
            string transactionQuery = "INSERT INTO [Transaction] (TransactionID, OrderID, OrderTotalPrice, InvoiceID, InvoiceDate, TransactionStatus,TransactionDate) " +
                                   "VALUES (@TransactionID, @OrderID, @OrderTotalPrice, @InvoiceID, @InvoiceDate, @TransactionStatus,@TransactionDate)";

            using (SqlConnection conn = new SqlConnection(cs))
            {
                conn.Open();

                using (SqlCommand transactionCmd = new SqlCommand(transactionQuery, conn))
                {
                    transactionCmd.Parameters.AddWithValue("@TransactionID", transactionID);
                    transactionCmd.Parameters.AddWithValue("@OrderID", orderID);
                    transactionCmd.Parameters.AddWithValue("@OrderTotalPrice", totalPrice);
                    transactionCmd.Parameters.AddWithValue("@InvoiceID", invoiceId);
                    transactionCmd.Parameters.AddWithValue("@InvoiceDate", date);
                    transactionCmd.Parameters.AddWithValue("@TransactionStatus", transactionStatus);

                    transactionCmd.Parameters.AddWithValue("@TransactionDate", DateTime.Now);

                    transactionCmd.ExecuteNonQuery();
                }
            }
        

    }

protected void btnRetryPayment_Click(object sender, EventArgs e)
        {
            Response.Redirect("checkout.aspx");
        }

        protected void btnReturnHome_Click(object sender, EventArgs e)
        {
            Response.Redirect("Home.aspx");
        }
    }
}