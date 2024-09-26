using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using NewVersion.Models;

namespace NewVersion.admin
{
    public partial class shippers : System.Web.UI.Page
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
                var shippers = context.Shippers.ToList();
                GridView1.DataSource = shippers;
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
            var sortColumn = e.SortExpression;
            using (var context = new userEntities())
            {
                var shippers = context.Shippers.ToList();

                switch (sortColumn)
                {
                    case "shipperID":
                        shippers = shippers.OrderBy(s => s.shipperID).ToList();
                        break;
                    case "shipperName":
                        shippers = shippers.OrderBy(s => s.shipperName).ToList();
                        break;
                    case "shipperEmail":
                        shippers = shippers.OrderBy(s => s.shipperEmail).ToList();
                        break;
                    case "shipperPhone":
                        shippers = shippers.OrderBy(s => s.shipperPhone).ToList();
                        break;
                }

                GridView1.DataSource = shippers;
                GridView1.DataBind();
            }
        }

        protected void EditTaskButton_Click(object sender, EventArgs e)
        {
            Button editButton = sender as Button;
            if (editButton != null)
            {
                int shipperID = Convert.ToInt32(editButton.CommandArgument);

                using (var context = new userEntities())
                {
                    var shipper = context.Shippers.Find(shipperID);
                    if (shipper != null)
                    {
                        addName.Text = shipper.shipperName;
                        addEmail.Text = shipper.shipperEmail;
                        addPhone.Text = shipper.shipperPhone;

                        HiddenShipperID.Value = shipperID.ToString();

                        ScriptManager.RegisterStartupScript(this, GetType(), "showModal", "$('#addRowModal').modal('show');", true);
                    }
                }
            }
        }

        protected void RemoveShipperButton_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender; // Cast sender to Button
            int shipperId = Convert.ToInt32(btn.CommandArgument);

            using (var context = new userEntities())
            {
                var shipperToRemove = context.Shippers.Find(shipperId);
                if (shipperToRemove != null)
                {
                    context.Shippers.Remove(shipperToRemove);
                    context.SaveChanges();
                }
            }

            BindGrid();
            FeedbackLabel.Text = "Shipper removed successfully!";
            FeedbackLabel.CssClass = "text-success";
        }

        protected void UpdateRowButton_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                using (var context = new userEntities())
                {
                    int shipperID;
                    if (int.TryParse(HiddenShipperID.Value, out shipperID) && shipperID > 0)
                    {
                        var shipper = context.Shippers.Find(shipperID);
                        if (shipper != null)
                        {
                            shipper.shipperName = addName.Text;
                            shipper.shipperEmail = addEmail.Text;
                            shipper.shipperPhone = addPhone.Text;

                            context.SaveChanges();
                        }
                    }
                    else
                    {
                        FeedbackLabel.Text = "Invalid Shipper ID.";
                        FeedbackLabel.CssClass = "text-danger";
                        return;
                    }
                }

                ResetModalState();

                FeedbackLabel.Text = "Shipper updated successfully!";
                FeedbackLabel.CssClass = "text-success";
            }
        }

        private void ResetModalState()
        {
            addName.Text = string.Empty;
            addEmail.Text = string.Empty;
            addPhone.Text = string.Empty;
            HiddenShipperID.Value = string.Empty;

            ScriptManager.RegisterStartupScript(this, GetType(), "closeModal", "$('#addRowModal').modal('hide');", true);
            BindGrid();
        }
    }
}
