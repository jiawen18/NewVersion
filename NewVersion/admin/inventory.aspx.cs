using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using NewVersion.Models;

namespace NewVersion.admin
{
    public partial class inventory : System.Web.UI.Page
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
                var inventoryItems = context.Inventories.ToList();
                GridView1.DataSource = inventoryItems;
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
                var inventoryItems = context.Inventories.ToList();

                switch (sortColumn)
                {
                    case "inventoryName":
                        inventoryItems = inventoryItems.OrderBy(i => i.inventoryName).ToList();
                        break;
                    case "inventorySupplier":
                        inventoryItems = inventoryItems.OrderBy(i => i.inventorySupplier).ToList();
                        break;
                    case "inventoryQuantity":
                        inventoryItems = inventoryItems.OrderBy(i => i.inventoryQuantity).ToList();
                        break;
                }

                GridView1.DataSource = inventoryItems;
                GridView1.DataBind();
            }
        }

        protected void EditTaskButton_Click(object sender, EventArgs e)
        {
            Button editButton = sender as Button;
            if (editButton != null)
            {
                int inventoryID = Convert.ToInt32(editButton.CommandArgument);

                using (var context = new userEntities())
                {
                    var inventoryItem = context.Inventories.Find(inventoryID);
                    if (inventoryItem != null)
                    {
                        addInventoryName.Text = inventoryItem.inventoryName;
                        addInventorySupplier.Text = inventoryItem.inventorySupplier;
                        addInventoryQuantity.Text = inventoryItem.inventoryQuantity.ToString();

                        HiddenInventoryID.Value = inventoryID.ToString();

                        ScriptManager.RegisterStartupScript(this, GetType(), "showModal", "$('#addRowModal').modal('show');", true);
                    }
                }
            }
        }

        protected void RemoveInventoryButton_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender; // Cast sender to Button
            int inventoryId = Convert.ToInt32(btn.CommandArgument);

            using (var context = new userEntities())
            {
                var inventoryToRemove = context.Inventories.Find(inventoryId);
                if (inventoryToRemove != null)
                {
                    context.Inventories.Remove(inventoryToRemove);
                    context.SaveChanges();
                }
            }

            BindGrid();
            FeedbackLabel.Text = "Inventory item removed successfully!";
            FeedbackLabel.CssClass = "text-success";
        }

        protected void UpdateRowButton_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                using (var context = new userEntities())
                {
                    int inventoryID;
                    if (int.TryParse(HiddenInventoryID.Value, out inventoryID) && inventoryID > 0)
                    {
                        var inventoryItem = context.Inventories.Find(inventoryID);
                        if (inventoryItem != null)
                        {
                            inventoryItem.inventoryName = addInventoryName.Text;
                            inventoryItem.inventorySupplier = addInventorySupplier.Text;
                            inventoryItem.inventoryQuantity = int.Parse(addInventoryQuantity.Text);

                            context.SaveChanges();
                        }
                    }
                    else
                    {
                        FeedbackLabel.Text = "Invalid Inventory ID.";
                        FeedbackLabel.CssClass = "text-danger";
                        return;
                    }
                }

                ResetModalState();

                FeedbackLabel.Text = "Inventory item updated successfully!";
                FeedbackLabel.CssClass = "text-success";
            }
        }

        private void ResetModalState()
        {
            addInventoryName.Text = string.Empty;
            addInventorySupplier.Text = string.Empty;
            addInventoryQuantity.Text = string.Empty;
            HiddenInventoryID.Value = string.Empty;

            ScriptManager.RegisterStartupScript(this, GetType(), "closeModal", "$('#addRowModal').modal('hide');", true);
            BindGrid();
        }
    }
}
