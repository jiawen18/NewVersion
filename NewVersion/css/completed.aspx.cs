using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NewVersion.css
{
    public partial class completed : System.Web.UI.Page
    {
        string cs = Global.CS;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindTransactionData();
            }

        }

        protected void btnTrack_Click(object sender, EventArgs e)
        {
            Response.Redirect("Delivery.aspx");
        }

        protected void btnReview_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string productId = btn.CommandArgument;

            Response.Redirect("Review.aspx?ProductID=" + productId);
        }

        private void BindTransactionData()
        {
            string query = "SELECT t.TransactionID, o.OrderID, od.ProductName, od.Quantity, od.Storage, od.Color, od.Price, od.ProductImage " +
                           "FROM [dbo].[Transaction] t " +
                           "INNER JOIN [dbo].[Order] o ON t.OrderID = o.OrderID " +
                           "INNER JOIN [dbo].[OrderDetails] od ON o.OrderID = od.OrderID " +
                           "WHERE t.TransactionStatus = 'Success'";

            // Replace with your database connection and command execution logic
            DataTable dt = GetData(query); // Assume GetData executes the query and returns a DataTable

            if (dt.Rows.Count > 0)
            {
                // Bind your data to a Repeater, GridView, or other appropriate control
                rptTransactions.DataSource = dt;
                rptTransactions.DataBind();
            }
        }

        private DataTable GetData(string query)
        {
            string connectionString = "productConnectionString"; 

            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        conn.Open();

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }

            return dt;
        }
    }
}