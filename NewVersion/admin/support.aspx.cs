using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using NewVersion.Models;

namespace NewVersion.admin
{
    public partial class support : System.Web.UI.Page
    {
        private userEntities db = new userEntities();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGrid();
            }
        }

        private void BindGrid(string sortExpression = null)
        {
            using (var context = new userEntities())
            {
                var supportRequests = context.Supports.AsQueryable();

                if (!string.IsNullOrEmpty(sortExpression))
                {
                    switch (sortExpression)
                    {
                        case "FirstName":
                            supportRequests = supportRequests.OrderBy(s => s.FirstName);
                            break;
                        case "LastName":
                            supportRequests = supportRequests.OrderBy(s => s.LastName);
                            break;
                        case "Email":
                            supportRequests = supportRequests.OrderBy(s => s.Email);
                            break;
                        case "Status":
                            supportRequests = supportRequests.OrderBy(s => s.Status);
                            break;
                        case "CreatedAt":
                            supportRequests = supportRequests.OrderBy(s => s.CreatedAt);
                            break;
                    }
                }

                GridView1.DataSource = supportRequests.ToList();
                GridView1.DataBind();
            }
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            BindGrid();
        }

        protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
        {
            string sortExpression = e.SortExpression;
            BindGrid(sortExpression);
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string status = DataBinder.Eval(e.Row.DataItem, "Status").ToString();

                Button markAsReadButton = (Button)e.Row.FindControl("MarkAsReadButton");
                Button replyButton = (Button)e.Row.FindControl("ReplyButton");

                if (status == "Seen" || status == "Replied")
                {
                    markAsReadButton.Enabled = false;
                    replyButton.Enabled = false;
                }
            }
        }

        protected void MarkAsReadButton_Click(object sender, EventArgs e)
        {
            Button markAsReadButton = sender as Button;
            if (markAsReadButton != null)
            {
                int supportID = Convert.ToInt32(markAsReadButton.CommandArgument);

                using (var context = new userEntities())
                {
                    var supportRequest = context.Supports.Find(supportID);
                    if (supportRequest != null)
                    {
                        supportRequest.Status = "Seen";
                        context.SaveChanges();
                    }
                }

                BindGrid();
                FeedbackLabel.Text = "Support request marked as seen.";
                FeedbackLabel.CssClass = "text-info";
            }
        }

        protected void ReplyButton_Click(object sender, EventArgs e)
        {
            Button replyButton = sender as Button;
            if (replyButton != null)
            {
                int supportID = Convert.ToInt32(replyButton.CommandArgument);

                using (var context = new userEntities())
                {
                    var supportRequest = context.Supports.Find(supportID);
                    if (supportRequest != null)
                    {
                        supportRequest.Status = "Replied";
                        context.SaveChanges();
                    }
                }

                BindGrid();
                FeedbackLabel.Text = "Support request marked as replied.";
                FeedbackLabel.CssClass = "text-info";
            }
        }

        protected void RemoveSpamButton_Click(object sender, EventArgs e)
        {
            Button removeSpamButton = sender as Button;
            if (removeSpamButton != null)
            {
                int supportID = Convert.ToInt32(removeSpamButton.CommandArgument);

                using (var context = new userEntities())
                {
                    var supportRequest = context.Supports.Find(supportID);
                    if (supportRequest != null)
                    {
                        context.Supports.Remove(supportRequest);
                        context.SaveChanges();
                    }
                }

                BindGrid();
                FeedbackLabel.Text = "Support request removed.";
                FeedbackLabel.CssClass = "text-info";
            }
        }


    }
}
