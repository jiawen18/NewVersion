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
                LoadCompletedOrders();
            }

        }

        // Fetch completed orders from the database and bind them to a control
        private void LoadCompletedOrders()
        {
            DataTable dt = new DataTable();

            // SQL query to fetch completed orders with their details
            string query = @"
                SELECT 
                    o.OrderID,
                    od.ProductName,
                    od.Quantity,
                    od.Storage,
                    od.Color,
                    od.Price,
                    od.ProductImage,
                    t.TransactionStatus
                FROM 
                    [dbo].[Transaction] t
                INNER JOIN 
                    [dbo].[Order] o ON t.OrderID = o.OrderID
                INNER JOIN 
                    [dbo].[OrderDetails] od ON o.OrderID = od.OrderID
                WHERE 
                    t.TransactionStatus = 'Success'";

            // Use a SqlDataAdapter to fill the DataTable
            using (SqlConnection conn = new SqlConnection(cs))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }

            rptOrderDetails.DataSource = dt;
            rptOrderDetails.DataBind();
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


    }
}