using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NewVersion.admin
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        string cs = Global.CS; // Your connection string

        private string SortDirection
        {
            get
            {
                return ViewState["SortDirection"] as string ?? "ASC";
            }
            set
            {
                ViewState["SortDirection"] = value;
            }
        }

        private string SortExpression
        {
            get
            {
                return ViewState["SortExpression"] as string ?? "Name";
            }
            set
            {
                ViewState["SortExpression"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindRepeater(SortExpression, SortDirection);
            }
        }

        private void BindRepeater(string sortBy, string sortDirection)
        {
            string query = $"SELECT Name, Price, Quantity FROM Product ORDER BY {sortBy} {sortDirection}";

            using (SqlConnection conn = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);

                Repeater1.DataSource = ds;
                Repeater1.DataBind();

                conn.Close();
            }
        }

        protected void SortButton_Click(object sender, EventArgs e)
        {
            var button = (System.Web.UI.WebControls.LinkButton)sender;
            string sortBy = button.CommandArgument;
            string sortDirection = SortDirection == "ASC" ? "DESC" : "ASC";

            // Update sort direction and expression
            SortDirection = sortDirection;
            SortExpression = sortBy;

            // Rebind the repeater
            BindRepeater(sortBy, sortDirection);

            // Update button icons
            UpdateSortIcons();
        }

        private void UpdateSortIcons()
        {
            // Access the header template and find the sorting buttons
            RepeaterItem headerItem = Repeater1.Controls[0] as RepeaterItem;

            if (headerItem != null)
            {
                // Find the sorting buttons by their IDs
                System.Web.UI.WebControls.LinkButton sortByName = headerItem.FindControl("SortByName") as System.Web.UI.WebControls.LinkButton;
                System.Web.UI.WebControls.LinkButton sortByPrice = headerItem.FindControl("SortByPrice") as System.Web.UI.WebControls.LinkButton;
                System.Web.UI.WebControls.LinkButton sortByQuantity = headerItem.FindControl("SortByQuantity") as System.Web.UI.WebControls.LinkButton;

                // Update icons based on the current sort expression and direction
                if (sortByName != null)
                {
                    sortByName.Text = SortExpression == "Name" ? (SortDirection == "ASC" ? "<i class='fa fa-sort-up'></i>" : "<i class='fa fa-sort-down'></i>") : "<i class='fa fa-sort'></i>";
                }

                if (sortByPrice != null)
                {
                    sortByPrice.Text = SortExpression == "Price" ? (SortDirection == "ASC" ? "<i class='fa fa-sort-up'></i>" : "<i class='fa fa-sort-down'></i>") : "<i class='fa fa-sort'></i>";
                }

                if (sortByQuantity != null)
                {
                    sortByQuantity.Text = SortExpression == "Quantity" ? (SortDirection == "ASC" ? "<i class='fa fa-sort-up'></i>" : "<i class='fa fa-sort-down'></i>") : "<i class='fa fa-sort'></i>";
                }
            }
        }


        protected void AddRowButton_Click(object sender, EventArgs e)
        {
            string script = "alert('Add button clicked!');";
            ClientScript.RegisterStartupScript(this.GetType(), "AlertScript", script, true);
        }
        protected void CopyItemButton_Click(object sender, EventArgs e)
        {
            string script = "alert('Button clicked!');";
            ClientScript.RegisterStartupScript(this.GetType(), "AlertScript", script, true);
        }

        protected void EditTaskButton_Click(object sender, EventArgs e)
        {
            string script = "alert('Button clicked!');";
            ClientScript.RegisterStartupScript(this.GetType(), "AlertScript", script, true);
        }

        protected void RemoveItemButton_Click(object sender, EventArgs e)
        {
            string script = "alert('Button clicked!');";
            ClientScript.RegisterStartupScript(this.GetType(), "AlertScript", script, true);
        }
    }
}