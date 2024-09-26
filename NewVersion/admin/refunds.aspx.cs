using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using NewVersion.Models;

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
                    }
                }

                //logic to update at customer's side here

                BindGrid();
                FeedbackLabel.Text = "Refund request approved successfully!";
                FeedbackLabel.CssClass = "text-success";

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
