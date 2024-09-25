using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NewVersion.css
{
    public partial class SuccessPage : System.Web.UI.Page
    {
        string cs = Global.CS;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // data from session
                string orderID = Request.QueryString["orderId"];
                lblOrderId.Text = orderID;
                string transactionID = Request.QueryString["TransactionId"];
                lblTransactionId.Text = transactionID;

                var (invoiceId, date) = GenerateRandomInvoiceId();

                string invoiceNumber = invoiceId;
                lblInvoiceId.Text = invoiceNumber;

                string invoiceDate = date;
                lblInvoiceDate.Text = invoiceDate;
                

                if (Session["Amount"] != null)
                {
                    decimal amount = (decimal)Session["Amount"]; // read from session
                    lblAmount.Text = "RM " + amount.ToString("F2"); // show amount
                }

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
            string date = DateTime.Now.ToString("dd MMMM yyyy"); // Format: DD Month YYYY

            return (result.ToString(), date); // Return the generated random invoice ID and date
        }


        protected void btnContinue_Click(object sender, EventArgs e)
        {
            Response.Redirect("Home.aspx");
        }

        private void SaveOrderToDatabase(string transactionID,string orderID, string invoiceNumber, DateTime invoiceDate)
        {
            SqlConnection con = new SqlConnection(cs);

            string query = "INSERT INTO Orders (TransactionID,OrderID, InvoiceNumber, InvoiceDate) VALUES (@TransactionID,@OrderID, @InvoiceNumber, @InvoiceDate)";
            
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@TransactionID",transactionID);
            cmd.Parameters.AddWithValue("@OrderID", orderID);
            cmd.Parameters.AddWithValue("@InvoiceNumber", invoiceNumber);
            cmd.Parameters.AddWithValue("@InvoiceDate", invoiceDate);

            con.Open();
            cmd.ExecuteNonQuery(); 
            
        }

    }
}
