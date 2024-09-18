using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NewVersion.css
{
    public partial class BackendPayment : System.Web.UI.Page
    {
        string cs = Global.CS;

        protected void Page_Load(object sender, EventArgs e)
        {
            string paymentStatus = Request.Form["Status"];
            string transactionID = Request.Form["TransID"];

            if (paymentStatus == "1")  //payment successfully
            {
                UpdateOrderStatus(transactionID, "Paid");
            }

            Response.StatusCode = 200;
            Response.End();
        }

        private void UpdateOrderStatus(string transactionID, string status)
        {
            string query = "Update Orders SET Status = @Status WHERE TransactionID = @TranactionID";

            SqlConnection con = new SqlConnection(cs);

            SqlCommand cmd = new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("@Status", status);
            cmd.Parameters.AddWithValue("@TransactionID", transactionID);

            try
            {
                con.Open();

                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    //update orders status
                    System.Diagnostics.Debug.WriteLine("Order Status update to 'Paid'");
                }

                else
                {
                    System.Diagnostics.Debug.WriteLine("Order not found, update 'Failed'");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error occured when updating order: " + ex.Message);
            }
        }
    }
}