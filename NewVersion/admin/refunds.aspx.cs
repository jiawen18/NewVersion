using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Util;
using NewVersion.Models;
using Razorpay.Api;

namespace NewVersion.admin
{
    public partial class refunds : System.Web.UI.Page
    {
        private userEntities db = new userEntities();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGrid();
            }
        }

        private void BindGrid()
        {
            using (var context = new userEntities())
            {
                var refundRequests = context.Refunds.AsQueryable();

                string selectedStatus = statusFilter.SelectedValue;
                if (!string.IsNullOrEmpty(selectedStatus))
                {
                    refundRequests = refundRequests.Where(r => r.RefundStatus == selectedStatus);
                }

                GridView1.DataSource = refundRequests.ToList();
                GridView1.DataBind();
            }
        }

        protected void StatusFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGrid();
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            BindGrid();
        }

        protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
        {
            var sortColumn = e.SortExpression;
            using (var context = new userEntities())
            {
                var refundRequests = context.Refunds.AsQueryable();

                string selectedStatus = statusFilter.SelectedValue;
                if (!string.IsNullOrEmpty(selectedStatus))
                {
                    refundRequests = refundRequests.Where(r => r.RefundStatus == selectedStatus);
                }

                switch (sortColumn)
                {
                    case "OrderID":
                        refundRequests = refundRequests.OrderBy(r => r.OrderID);
                        break;
                    case "RefundRequestDate":
                        refundRequests = refundRequests.OrderBy(r => r.RefundRequestDate);
                        break;
                    case "RefundStatus":
                        refundRequests = refundRequests.OrderBy(r => r.RefundStatus);
                        break;
                    case "refundClosureDate":
                        refundRequests = refundRequests.OrderBy(r => r.RefundClosureDate);
                        break;
                }

                GridView1.DataSource = refundRequests.ToList();
                GridView1.DataBind();
            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                string refundStatus = DataBinder.Eval(e.Row.DataItem, "RefundStatus").ToString();

                Button approveButton = (Button)e.Row.FindControl("ApproveRefundButton");
                Button rejectButton = (Button)e.Row.FindControl("RejectRefundButton");

                if (refundStatus == "Approved")
                {
                    approveButton.Enabled = false;
                    rejectButton.Enabled = true;
                }
                else if (refundStatus == "Rejected")
                {
                    approveButton.Enabled = true;
                    rejectButton.Enabled = false;
                }
                else // Pending
                {
                    approveButton.Enabled = true;
                    rejectButton.Enabled = true;
                }
            }
        }


        protected void ApproveRefundButton_Click(object sender, EventArgs e)
        {
            Button approveButton = sender as Button;
            if (approveButton != null)
            {
                int refundID = Convert.ToInt32(approveButton.CommandArgument);

                using (var context = new userEntities())
                {
                    var refundRequest = context.Refunds.Find(refundID);
                    if (refundRequest != null)
                    {
                        refundRequest.RefundStatus = "Approved";
                        refundRequest.RefundClosureDate = DateTime.Now;

                        context.SaveChanges();

                        var (orderId, orderTotalPrice) = RefundTransaction(refundRequest.OrderID);
                    }
                }

                //logic to update at customer's side here

                BindGrid();
                FeedbackLabel.Text = "Refund request approved successfully!";
                FeedbackLabel.CssClass = "text-success";

            }
        }

        public (string OrderID, decimal OrderTotalPrice) RefundTransaction(string orderId)
        {
            string cs = Global.CS;
            string _key = "rzp_test_7sBM0c2utoTQ59";
            string _secret = "OKDPvhfckfnU2BnhPs7dKERM";
            string paymentID = "";
            decimal orderTotalPrice = 0;

            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                string query = "SELECT TransactionID, OrderTotalPrice FROM Transactions WHERE OrderID = @OrderID";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@OrderID", orderId);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            paymentID = reader["TransactionID"].ToString();
                            orderTotalPrice = Convert.ToDecimal(reader["OrderTotalPrice"]);
                        }
                    }
                }
            }

            if (string.IsNullOrEmpty(paymentID))
            {
                throw new Exception("Transaction not found for the given OrderID.");
            }

            var client = new RazorpayClient(_key, _secret);

            var options = new Dictionary<string, object>
            {
                { "amount", orderTotalPrice * 100 }, // Amount should be in paise
            };

            try
            {
                // Assuming the Refund method takes a single parameter which is an options dictionary
                var refund = client.Payment.Refund(options);
                Console.WriteLine("Refund successful: " + refund);

                UpdateTransactionAndOrderStatus(paymentID, orderId);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Refund failed: " + ex.Message);
            }

            return (orderId, orderTotalPrice);
        }

        private void UpdateTransactionAndOrderStatus(string paymentID, string orderId)
        {
            string cs = Global.CS;

            using (SqlConnection con = new SqlConnection(cs)) {
                
                string updateTransactionQuery = "UPDATE Transactions SET TransactionStatus = @Status WHERE PaymentID = @PaymentID";
                using (SqlCommand cmd = new SqlCommand(updateTransactionQuery, con))
                {
                    cmd.Parameters.AddWithValue("@Status", "Refunded");
                    cmd.Parameters.AddWithValue("@PaymentID", paymentID);
                    cmd.ExecuteNonQuery();
                }

                
                string updateOrderQuery = "UPDATE Orders SET OrderStatus = @OrderStatus WHERE OrderID = @OrderID"; 
                using (SqlCommand cmd = new SqlCommand(updateOrderQuery, con))
                {
                    cmd.Parameters.AddWithValue("@OrderStatus", "Cancelled");
                    cmd.Parameters.AddWithValue("@OrderID", orderId);
                    cmd.ExecuteNonQuery();
                }
            }
        } 

        protected void RejectRefundButton_Click(object sender, EventArgs e)
        {
            Button rejectButton = sender as Button;
            if (rejectButton != null)
            {
                int refundID = Convert.ToInt32(rejectButton.CommandArgument);

                using (var context = new userEntities())
                {
                    var refundRequest = context.Refunds.Find(refundID);
                    if (refundRequest != null)
                    {
                        refundRequest.RefundStatus = "Rejected";
                        refundRequest.RefundClosureDate = DateTime.Now;

                        context.SaveChanges();
                    }
                }

                //logic to update at customer's side here

                BindGrid();
                FeedbackLabel.Text = "Refund request rejected!";
                FeedbackLabel.CssClass = "text-danger";

            }
        }
    }
}
