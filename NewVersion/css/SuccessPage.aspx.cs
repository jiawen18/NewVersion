using NewVersion.admin;
using NewVersion.Models;
using Razorpay.Api;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.EnterpriseServices;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Util;

namespace NewVersion.css
{
    public partial class SuccessPage : System.Web.UI.Page
    {
        string cs = Global.CS;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string orderID = Request.QueryString["orderId"];
                lblOrderId.Text = orderID;

                string transactionID = Request.QueryString["TransactionId"];
                lblTransactionId.Text = transactionID;

                var (invoiceId, date) = GenerateRandomInvoiceId();
                lblInvoiceId.Text = invoiceId;
                lblInvoiceDate.Text = date;
                
                decimal totalPrice = 0.00m;

                if (Session["Amount"] != null)
                {
                    decimal amount = (decimal)Session["Amount"];
                    lblAmount.Text = "RM " + amount.ToString("F2");
                    totalPrice = amount;
                    
                }
                
                

                string transactionStatus = "Success";

                SaveTransactionDetails(transactionID, orderID, totalPrice, invoiceId, date, transactionStatus);
            }
        }


        public static (string InvoiceId, string Date) GenerateRandomInvoiceId()
        {
            const int length = 10; // Length of the random ID (excluding the prefix)
            const string chars = "0123456789";
            StringBuilder result = new StringBuilder();
            Random random = new Random(); // Create a random number generator

            for (int i = 0; i < length; i++)
            {
                int randomIndex = random.Next(chars.Length); // Generate a random index
                result.Append(chars[randomIndex]); // Randomly select a character and append it
            }

            // Get the current date in the specified format
            string date = DateTime.Now.ToString("M/d/yyyy hh:mm:ss tt"); ; // Format: DD Month YYYY

            return (result.ToString(), date); // Return the generated random invoice ID and date
        }

        protected void btnContinue_Click(object sender, EventArgs e)
        {
            Response.Redirect("Home2.aspx");
        }

        private void SaveTransactionDetails(string transactionID, string orderID, decimal totalPrice, string invoiceId, string date, string transactionStatus)
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


    }


}

