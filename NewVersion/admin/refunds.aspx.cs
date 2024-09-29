using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Util;
using NewVersion.css;
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
            string cs = Global.CS;

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

                        string orderId = "";
                        using (SqlConnection con = new SqlConnection(cs))
                        {
                            con.Open();

                            string query = "SELECT OrderID FROM f.Refund WHERE RefundID = @ORefundID";

                            using (SqlCommand cmd = new SqlCommand(query, con))
                            {
                                using (SqlDataReader dr = cmd.ExecuteReader())
                                {

                                    if (dr.Read())
                                    {
                                        orderId = dr["OrderID"].ToString();
                                    }
                                }

                                string query1 = "UPDATE Order OrderStatus=@orderStatus WHERE OrderID = @OrderID ";

                                using (SqlCommand cmd1 = new SqlCommand(query1, con))
                                {
                                    using (SqlDataReader dr1 = cmd1.ExecuteReader())
                                    {
                                        cmd1.Parameters.AddWithValue("OrderStatus", "Cancelled");
                                        cmd1.Parameters.AddWithValue("OrderID", orderId);

                                        cmd.ExecuteNonQuery();
                                    }
                                }

                                string query2 = "UPDATE Transaction TransactionStatus = @TransactionStatus WHERE OrderID = @OrderID ";

                                using (SqlCommand cmd2 = new SqlCommand(query2, con))
                                {
                                    using (SqlDataReader dr2 = cmd2.ExecuteReader())
                                    {
                                        cmd2.Parameters.AddWithValue("OrderStatus", "Refunded");
                                        cmd2.Parameters.AddWithValue("OrderID", orderId);

                                        cmd.ExecuteNonQuery();
                                    }
                                }
                            }

                        }

                        //logic to update at customer's side here

                        BindGrid();
                        FeedbackLabel.Text = "Refund request approved successfully!";
                        FeedbackLabel.CssClass = "text-success";

                    }
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
