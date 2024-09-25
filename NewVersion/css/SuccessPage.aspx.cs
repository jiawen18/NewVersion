using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

                var (invoiceId, date) = GenerateRandomInvoiceId();

                lblInvoiceId.Text = invoiceId;
                lblInvoiceDate.Text = date;


                if (Session["Amount"] != null)
                {
                    double amount = (double)Session["Amount"]; // read from session
                    lblAmount.Text = "RM " + amount.ToString("F2"); // show amount
                }

            }
        }


        public static (string InvoiceId, string Date) GenerateRandomInvoiceId()
        {
            const int length = 10; // Length of the random ID (excluding the prefix)
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

            // Create the prefix with the updated call count
            string prefix = $"invoice_"; // Fixed prefix with incrementing number

            StringBuilder result = new StringBuilder(prefix); // Initialize the result with the prefix

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
    }
}
